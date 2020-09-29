using CerSys.Data.Model;
using CertSys.Data.MSSQL.Context.Interfaces;
using Dapper;

namespace CertSys.Data.SqlServer.Repository.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        #region Properts
        private ISqlServerContext SqlServerContext { get; set; }
        #endregion

        #region Constructor
        public ConfigurationRepository(ISqlServerContext sqlServerContext)
        {
            SqlServerContext = sqlServerContext;
        }
        #endregion

        #region Methods
        public int Insert(Configuration configuration)
        {
            var query = @"
            insert into configuration 
            (                
                MaxVao,
                MaxBaseReforcada,
                MinTotal
            ) 
            values
            (
                @MaxVao,
                @MaxBaseReforcada,
                @MinTotal
            )";

            return SqlServerContext.GetConnection().Execute(query, configuration);
        }

        public Configuration GetLastConfiguration()
        {
            var query = @"
            select top(1) 
                MaxVao, 
                MaxBaseReforcada, 
                MinTotal 
            from 
                Configuration 
            order by 
                id desc";

            return SqlServerContext.GetConnection().QuerySingleOrDefault<Configuration>(query);
        }
        #endregion
    }
}
