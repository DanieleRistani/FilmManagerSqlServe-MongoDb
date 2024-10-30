using System;
using System.Collections.Generic;

namespace FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

public partial class Film
{
    public long FilmId { get; set; }

    public string FilmName { get; set; } = null!;

    public string FilmUrlImg { get; set; } = null!;

    public string FilmDirector { get; set; } = null!;

    public DateOnly FilmRelaseDate { get; set; }

    public long FilmCategoryId { get; set; }

    public virtual Category FilmCategory { get; set; } = null!;

    public virtual ICollection<FilmsTagsPivot> FilmsTagsPivots { get; set; } = new List<FilmsTagsPivot>();
}
