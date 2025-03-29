namespace webapp.data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int No_Of_Pages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int languageId { get; set; }

        public int? AuthorId { get; set; }
        public Langauge? language { get; set; }

        public Author? author { get; set; }

    }
}
