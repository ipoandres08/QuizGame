using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class UIService
    {
        private DataService dataService;
        public UIService() 
        { 
            dataService = new DataService();
        }

        public void Show()
        {
            Collection[] collections = dataService.ReadData();
            Console.WriteLine("Welcome to the quizz console Application:\n");
            for (int i = 0; i < collections.Length; i++)
            {
                Console.WriteLine($"{i+1} {collections[i].Name}");
            }

            int choose = SelectAChoose(collections, "collection");

            ShowCollection(collections[choose-1]);
        }

        private void ShowCollection(Collection collection) 
        {
            Console.Clear();
            Console.WriteLine($"Quizzes in {collection.Name}\n");
            Quiz[] quizzes = collection.Quizzes.ToArray();
            for (int i = 0; i < quizzes.Length; i++)
            {
                Console.WriteLine($"{i + 1} {quizzes[i].Name}");
            }
            int choose = SelectAChoose(quizzes, "quiz");

            ShowQuestions(quizzes[choose - 1]);
        }

        private int SelectAChoose(Object[] objects, string name)
        {
            int choose = -1;
            bool success = false;

            while (choose < 1 || choose > objects.Length && !success)
            {
                Console.Write($"\nSelect a {name}: ");
                success = int.TryParse(Console.ReadLine(), out choose);
            }
            return choose;
        }

        private void ShowQuestions(Quiz quiz)
        {
            string answer = "";
            Console.Clear();
            Console.WriteLine($"Selected quiz: {quiz.Name}\nQuiz description: {quiz.Description}\n");
            int count = 1;
            int totalScore = 0;
            int playerScore = 0;
            foreach(Question question in quiz.Questions) 
            {
                Console.WriteLine($"Question{count}\nquestion.ToString()");
                answer = Console.ReadLine().Replace(" ", "").ToLower();
                if(answer == question.CorrectAnswer.Replace(" ", "").ToLower())
                {
                    Console.WriteLine($"Correct answer! you earned {question.Score} points.");
                    playerScore += question.Score;
                } else
                {
                    Console.WriteLine($"Wrong answer, you lost {question.Score} points.");
                }
                totalScore += question.Score;
                count++;
            }
            Console.WriteLine($"Quiz completed!\nYour score: {playerScore} out of {totalScore}");
            if (playerScore == totalScore) Console.WriteLine("Good job!");

        }

    }
}
