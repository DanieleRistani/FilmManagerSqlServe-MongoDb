namespace FilmManager.SqlServe.EntitySqlServe
{
    public class FilmDto
    {
        public long FilmId { get; set; }

        public string FilmName { get; set; } = null!;

        public string FilmUrlImg { get; set; } = null!;

        public string FilmDirector { get; set; } = null!;

        public DateOnly FilmRelaseDate { get; set; }

        public long FilmCategoryId { get; set; }

       
    }
}
