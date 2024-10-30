using System;
using System.Collections.Generic;

namespace FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

public partial class Tag
{
    public long TagId { get; set; }

    public string TagName { get; set; } = null!;

    public string TagDescription { get; set; } = null!;

    public virtual ICollection<FilmsTagsPivot> FilmsTagsPivots { get; set; } = new List<FilmsTagsPivot>();
}
