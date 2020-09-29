using CertSys.Data.MSSQL.Context.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CertSys.Data.MSSQL.Context
{
    public class SqlServerContext : ISqlServerContext
    {
        #region Properts
        private string ConString { get; set; }
        private SqlConnection Connection { get; set; }
        #endregion

        #region Constructor
        public SqlServerContext(string constring)
        {
            ConString = constring;
        }
        #endregion

        #region Methods
        public SqlConnection GetConnection()
        {
            if (Connection == null)
                Connection = new SqlConnection(ConString);

            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            return Connection;
        }
        #endregion
    }
}
