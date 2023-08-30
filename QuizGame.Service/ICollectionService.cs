using OneOf;
using QuizGamePerssistence.Models;
using QuizGamePerssistence.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> RetrieveCollections(
        CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> RetrieveCollection(
            Guid id, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> CreateCollection(
            CollectionForUpsert newCollection, bool addNewQuizzes, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> UpdateCollection(
            Guid id, CollectionForUpsert collection, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> DeleteCollection(
            Guid id, CancellationToken cancellationToken);
    }
}
