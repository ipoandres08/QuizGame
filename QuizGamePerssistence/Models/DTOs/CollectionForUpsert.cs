
namespace QuizGamePerssistence.Models.DTOs
{
    public class CollectionForUpsert
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        //public ICollection<QuizForUpsert> Quizzes { get; init; } = new List<QuizForUpsert> ();
    }
}
