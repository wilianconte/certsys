using CerSys.Data.Model;

namespace CertSys.Data.SqlServer.Repository.Repository
{
    public interface IConfigurationRepository
    {
        int Insert(Configuration configuration);

        Configuration GetLastConfiguration();
    }
}