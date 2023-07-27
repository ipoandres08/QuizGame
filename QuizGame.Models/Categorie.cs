using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Categorie
    {
        public string Name { get; set; }

        public Categorie(string name) 
        {
            Name = name;
        }
    }
}
