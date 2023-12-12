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
                    return FitnessFunctions.Ackley(position);
                case 1:
                    return FitnessFunctions.My1(position);
                case 2:
                    return FitnessFunctions.My2(position);
                case 3:
                    return FitnessFunctions.My3(position);
                case 4:
                    return FitnessFunctions.My4(position);
                case 5:
                    return FitnessFunctions.My5(position);
                default:
                    throw new NotImplementedException();
            }
        }

        public static double Ackley(double[] x)
        {
            double a = 20;
            double b = 0.2;
            double c = 2 * Math.PI;
            int dimenzion = x.Length;

            double sum1 = 0.0;
            double sum2 = 0.0;

            for (int i = 0; i < dimenzion; i++)
            {
                sum1 += x[i] * x[i];
                sum2 += Math.Cos(c * x[i]);
            }

            double term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / dimenzion));
            double term2 = -Math.Exp(sum2 / dimenzion);

            return term1 + term2 + a + Math.Exp(1);
        }

        public static double My1(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Sin(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }

        public static double My2(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Cos(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }
        public static double My3(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Tan(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }
        public static double My4(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Cosh(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }
        public static double My5(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Tanh(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }
    }
}
