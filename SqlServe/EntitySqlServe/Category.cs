using System;
using System.Collections.Generic;

namespace FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

public partial class Category
{
    public long CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryImg { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
