namespace BookLibrarySite.Models
{
    public partial class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int TotalCopies { get; set; }

        public int CopiesInUse { get; set; }

        public string? Type { get; set; }

        public string? Isbn { get; set; }

        public string? Category { get; set; }
    }

}
