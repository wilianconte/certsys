using System;

namespace CertSys.Core
{
    public static class Calculos
    {
        private static double CalcDistMax(double max, double total)
        {
            double qnt = Math.Ceiling(total / max);

            return total / qnt;
        }

        private static int CalcPilarReforco(double distPilar, double distReforco)
        {
            return (int)Math.Floor(distReforco / distPilar);
        }

        public static  double TestCalcDistMax(double one, double two)
        {
            return CalcDistMax(one, two);
        }

        public static int TestCalcPilarReforco(double distone, double disttwo)
        {
            return CalcPilarReforco(distone, disttwo);
        }
    }
}
