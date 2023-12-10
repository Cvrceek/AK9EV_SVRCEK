using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public static class FitnessFunctions
    {
        public static double EvaluateFitness(double[] position, int fce)
        {
            switch (fce)
            {
                case 0:
                    return FitnessFunctions.DJ1(position);
                case 1:
                    return FitnessFunctions.DJ2(position);
                case 2:
                    return FitnessFunctions.Schwefel(position);
                default:
                    throw new NotImplementedException();
            }
        }


        public static double DJ1(double[] values)
        {
            double result = 0;
            foreach (var v in values)
                result += Math.Pow(v, 2);
            return result;
        }

        public static double DJ2(double[] values)
        {
            double result = 0;
            for (int i = 0; i < values.Length - 1; i++)
                result += (100 * Math.Pow((Math.Pow(values[i], 2) - values[i + 1]), 2)) + Math.Pow((1 - values[i]), 2);
            return result;
        }

        public static double Schwefel(double[] values)
        {
            double result = 0;
            foreach (var v in values)
                result += (-1 * v) * Math.Sin(Math.Sqrt(Math.Abs(v)));
            return result;
        }
    }
}
