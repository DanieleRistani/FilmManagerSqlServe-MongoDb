using System;
using System.Collections.Generic;

namespace FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

public partial class FilmsTagsPivot
{
    public long FilmTagId { get; set; }

    public long FilmTagFilmId { get; set; }

    public long FilmTagTagId { get; set; }

    public virtual Film FilmTagFilm { get; set; } = null!;

    public virtual Tag FilmTagTag { get; set; } = null!;
}
