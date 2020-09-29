using CerSys.Data.Model;
using CertSys.Data.SqlServer.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CerSys.Domain
{
    public class CalcServices : ICalcServices
    {

        #region Properts
        private readonly IConfigurationRepository _configurationRepository;
        #endregion

        #region Constructor
        public CalcServices(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        #endregion

        #region Methods

        public List<Pilar> GetPilars(int number, double distane)
        {
            List<Pilar> pilars = new List<Pilar>();

            pilars.Add(new Pilar { Position = 1, Reinforced = true });

            for (int i = 0; i < number - 1; i++)
            {
                pilars.Add(new Pilar { Distance = distane * (i + 1), Position = i + 2 });
            }

            Pilar last = pilars.LastOrDefault();

            pilars.Add(new Pilar { Position = last.Position + 1, Distance = last.Distance + distane, Reinforced = true });

            return pilars;
        }

        public List<Pilar> SetPilarReinforced(double distMax, List<Pilar> pilars)
        {
            List<Pilar> auxpilar = new List<Pilar>(pilars);

            foreach (Pilar item in pilars)
            {
                if (!item.Reinforced && item.Distance % distMax == 0)
                {
                    Pilar aux = new Pilar
                    {
                        Distance = item.Distance,
                        Position = item.Position,
                        Reinforced = true
                    };
                    auxpilar.RemoveAt(item.Position - 1);
                    auxpilar.Insert(item.Position - 1, aux);
                }
            }

            return auxpilar;
        }

        public List<Pilar> GetPilarsReinforced(List<Pilar> pilars)
        {
            return pilars.Where(w => w.Reinforced).ToList();
        }

        public int SaveConfiguration(Configuration Configuration)
        {
            return _configurationRepository.Insert(Configuration);
        }

        public Configuration GetLastConfiguration()
        {
            return _configurationRepository.GetLastConfiguration();
        }

        public double CalcDistMax(double max, double total)
        {
            double qnt = Math.Ceiling(total / max);

            return total / qnt;
        }

        public List<int> CalcReforcoBase(double max, double total, double maxReforcoBase)
        {
            double qnt = Math.Ceiling(total / max);

            var list = new List<int>();

            var dist = total / qnt;

            var reduc = 0;

            for (var i = 1; i <= (qnt + 1); i++)
            {
                //first
                if (i == 1)
                {
                    list.Add(i);
                    continue;
                }

                //last
                if (i == qnt + 1)
                {
                    list.Add(i);
                    continue;
                }

                if ((maxReforcoBase - ((i - reduc) * dist)) <= 0)
                {
                    reduc = i;
                    list.Add(i);
                }
            }

            return list;
        }


        #endregion
    }
}