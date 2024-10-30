using System;
using System.Collections.Generic;

namespace FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

public partial class Log
{
    public string LogId { get; set; } = null!;

    public string LogErrorMessage { get; set; } = null!;

    public DateTime LogDateError { get; set; }
}
