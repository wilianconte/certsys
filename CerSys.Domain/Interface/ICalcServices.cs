using CerSys.Data.Model;
using System.Collections.Generic;

namespace CerSys.Domain
{
    public interface ICalcServices
    {
        double CalcDistMax(double max, double total);
        int SaveConfiguration(Configuration Configuration);
        Configuration GetLastConfiguration();
        List<int> CalcReforcoBase(double max, double total, double maxReforcoBase);
    }
}