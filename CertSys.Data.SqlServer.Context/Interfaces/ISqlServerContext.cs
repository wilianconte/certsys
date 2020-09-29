using System.Data.SqlClient;

namespace CertSys.Data.MSSQL.Context.Interfaces
{
    public interface ISqlServerContext
    {
        SqlConnection GetConnection();
    }
}
