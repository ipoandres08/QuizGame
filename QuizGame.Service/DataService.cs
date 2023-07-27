using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using QuizGame.Models;

namespace QuizGame.Service
{
    public class DataService
    {
        public Collection[] ReadData()
        {
            Collection[] data = new Collection[3];
            Dictionary<int, List<Quiz>> quizzes = ReadQuizzes();
            data[0] = new Collection("General quizzes", "General general", quizzes[1]);
            data[1] = new Collection("School quizzes", "I dont know, is something that you can say now", quizzes[2]);
            data[2] = new Collection("Entertaiment quizzes", "It's only a game, they tell me but now this is my life", quizzes[3]);
            return data;
        }

        public Dictionary<int, List<Quiz>> ReadQuizzes()
        {
            Dictionary<int, List<Quiz>> quizzesCollection = new Dictionary<int, List<Quiz>>();
            List<Quiz> quizzes = new List<Quiz>();
            List<Categorie> categories = new List<Categorie>();
            Dictionary<int, List<Question>> questions = ReadQuestions();

            categories.Add(new Categorie("General"));
            quizzes.Add(new Quiz("General quizz", "General questions for general people", categories, questions[1]));
            quizzesCollection.Add(1, quizzes);
            quizzes.Clear();

            categories.Add(new Categorie("History"));
            quizzes.Add(new Quiz("History quizz", "How much do you know about history?", categories, questions[2]));
            quizzesCollection.Add(2, quizzes);
            quizzes.Clear();

            categories.Clear();
            categories.Add(new Categorie("Entertaiment"));
            categories.Add(new Categorie("Movies"));
            quizzes.Add(new Quiz("Movies quizz", "Movies, can your remember all of them?", categories, questions[3]));
            quizzesCollection.Add(3, quizzes);

            return quizzesCollection;
        }

        public Dictionary<int, List<Question>> ReadQuestions()
        {
            Dictionary<int, List<Question>> questions = new Dictionary<int, List<Question>>();
            Dictionary<int, List<string>> choices = ReadChoices();

            List<Question> questionsList = new List<Question>();
            questionsList.Add(new FillBlankQuestion("_____ wrote The Odyssey", "Homer", 10, 0));
            questionsList.Add(new FillBlankQuestion("The olympic games originated in ______", "greece", 10, 5));
            questionsList.Add(new FillBlankQuestion("The whale is a ______", "mammal", 10, 4));
            questionsList.Add(new MultipleChoiceQuestion("What is the name of the longest river in the world?", "b", 10, choices[1]));
            questionsList.Add(new MultipleChoiceQuestion("What is the largest ocean in the world?", "a", 10, choices[2]));
            questions.Add(1, questionsList);

            questionsList = new List<Question>();
            questionsList.Add(new FillBlankQuestion("_______ ruled ancient egypt", "pharaoh", 10, 0));
            questionsList.Add(new FillBlankQuestion("According to legend, _______ and Remus founded Rome.", "romulus", 10, 3));
            questionsList.Add(new FillBlankQuestion("Alexander the Great was a student of the Greek philosopher ________.", "aristotle", 10, 10));
            questionsList.Add(new MultipleChoiceQuestion("In what year was America discovered?", "a", 10, choices[3]));
            questionsList.Add(new MultipleChoiceQuestion("What were the first settlers who moved in search of food called?", "", 10, choices[4]));
            questions.Add(2, questionsList);

            questionsList = new List<Question>();
            questionsList.Add(new FillBlankQuestion("Anastasia is a __________ movie.", "dreamworks", 10, 3));
            questionsList.Add(new FillBlankQuestion("Daffy Duck and Donald appear in the movie Who Framed _____ Rabbit", "roger", 10, 10));
            questionsList.Add(new FillBlankQuestion("The Chronicles of Narnia: The Lion, the Witch and the Wardrobe is a ______ movie", "disney", 10, 13));
            questions.Add(3, questionsList);

            return questions;
        }

        private Dictionary<int, List<string>> ReadChoices()
        {
            Dictionary<int, List<string>> choices = new Dictionary<int, List<string>>();
            List<string> choicesList = new List<string>();
            choicesList.Add("a. Nile River");
            choicesList.Add("b. Amazon River");
            choicesList.Add("c. Danube River");
            choices.Add(1, choicesList);

            choicesList = new List<string>();
            choicesList.Add("a. Pacific Ocean");
            choicesList.Add("b. Indian Ocean");
            choicesList.Add("c. Atlantic Ocean");
            choices.Add(2, choicesList);

            choicesList = new List<string>();
            choicesList.Add("a. 1492");
            choicesList.Add("b. 1592");
            choicesList.Add("c. 1590");
            choicesList.Add("d. 1482");
            choices.Add(3, choicesList);

            choicesList = new List<string>();
            choicesList.Add("a. Nomads");
            choicesList.Add("b. Homosapiens");
            choicesList.Add("c. Egyptians");
            choicesList.Add("d. Spanish people");
            choices.Add(4, choicesList);

            return choices;
        }
    }
}
