namespace webapp.data
{
    public class Langauge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
