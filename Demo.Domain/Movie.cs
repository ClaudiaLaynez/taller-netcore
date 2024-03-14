namespace Demo.Domain
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public GenreType Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
