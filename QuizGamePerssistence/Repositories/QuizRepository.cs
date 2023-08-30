using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static QuizGamePerssistence.Constants.Constants;

namespace QuizGamePerssistence.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizGameContext _context;
        //private readonly IValidator<Collection> _validator;

        public QuizRepository(QuizGameContext context)
        {
            _context = context;
            //_validator = validator;
        }

        public async Task<OneOf<Quiz, RequestError>> AddQuiz(Quiz quiz, CancellationToken cancellationToken)
        {
            await _context.Quizzes
                .AddAsync(quiz, cancellationToken);
            var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return quiz;
        }

        public async Task<OneOf<Quiz, RequestError>> DeleteQuiz(Guid id, CancellationToken cancellationToken)
        {
            var foundedQuiz = await GetQuizByID(id, cancellationToken);
            if (foundedQuiz.IsT1)
            {
                return foundedQuiz;
            }

            _context.Quizzes.Remove(foundedQuiz.AsT0);
            //var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return foundedQuiz;
        }

        public async Task<OneOf<Quiz, RequestError>> GetQuizByID(Guid id, CancellationToken cancellationToken)
        {
            var quiz = await _context.Quizzes
                .Where(collection => collection.QuizId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (quiz is null)
            {
                return new RequestError(HttpStatusCode.NotFound, RequestErrorMessages.CategoryNotFound);
            }

            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzes(CancellationToken cancellationToken)
        {
            return await _context.Quizzes.ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Quiz> existingQuizzes, IEnumerable<Guid> newQuizzes)> GetQuizzesByID(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var existQuizzes = await _context.Quizzes.Where(q => ids.Contains(q.QuizId)).ToListAsync(cancellationToken);
            var matchingQuizzesIds = existQuizzes
                .Select(q => q.QuizId).ToArray();

            var nonMatchingQuizzes = ids.Where(id => !matchingQuizzesIds.Contains(id)).ToList();
            
            return (existQuizzes, nonMatchingQuizzes);
        }
        public async Task<OneOf<IEnumerable<Quiz>, RequestError>> AddQuizzes(IEnumerable<Quiz> newQuizzes, CancellationToken cancellationToken)
        {
            var quizzesResult = new List<Quiz>();
      
            foreach (var quiz in newQuizzes)
            {
                var result = await this.AddQuiz(quiz, cancellationToken);
                if (result.IsT1)
                {
                    return result.AsT1;
                }
                quizzesResult.Add(result.AsT0);
            }
            return quizzesResult;
        }
        public async Task<OneOf<Quiz, RequestError>> UpdateQuiz(Guid id, Quiz quiz, CancellationToken cancellationToken)
        {
            var foundedQuiz = await _context.Quizzes
           .Where(x => x.QuizId == id)
           .FirstOrDefaultAsync(cancellationToken);
            if (foundedQuiz is null)
            {
                return new RequestError(
                    HttpStatusCode.NotFound, RequestErrorMessages.CollectionNotFound);
            }
            /*
            var validationResult = await _validator
                .ValidateAsync(collection);
            if (!validationResult.IsValid)
            {
                return new RequestError(
                    HttpStatusCode.UnprocessableEntity, validationResult.ToString());
            }*/

            foundedQuiz.Name = quiz.Name;
            foundedQuiz.Description = quiz.Description;
            foundedQuiz.Categories = quiz.Categories;
            //var debugView = _context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
            }

            return quiz;
        }
    }
}
