using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Categorie
    {
        public int QuizId { get; set; }
        public int CategorieId { get; set; } = new Random().Next();
        public string Name { get; set; }

        public Categorie(int quizId, string name) 
        {
            this.QuizId = quizId;
            Name = name;
        }
    }
}
