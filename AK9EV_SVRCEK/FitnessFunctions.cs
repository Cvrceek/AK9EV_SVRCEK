using ILNumerics.Drawing.Plotting;
using ILNumerics.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using System.Runtime.Remoting;

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
                    return FitnessFunctions.ChatGPTGen1(position);
                case 2:
                    return FitnessFunctions.Rastrigin(position);
                    //return FitnessFunctions.Sphere(position);
                case 3:
                    return FitnessFunctions.Schwefel(position);
                case 4:
                    return FitnessFunctions.Levy(position);
                case 5:
                    return FitnessFunctions.Sphere(position);
                case 6:
                    return FitnessFunctions.SchwefelNo226(position);
                case 7:
                    return FitnessFunctions.ChungReynolds(position);
                case 8:
                    return FitnessFunctions.EggHolder(position);
                case 9:
                    return FitnessFunctions.Quartic(position);
                case 10:
                    return FitnessFunctions.YaoLiu09(position);
                case 11:
                    return FitnessFunctions.BentCigar(position);
                case 12:
                    return FitnessFunctions.Qing(position);
                case 13:
                    return FitnessFunctions.Mishra11(position);
                case 14:
                    return FitnessFunctions.Mishra07(position);
                case 15:
                    return FitnessFunctions.DixonPrice(position);
                case 16:
                    return FitnessFunctions.Alpine01(position);
                case 17:
                    return FitnessFunctions.StyblinskiTang(position);
                case 18:
                    return FitnessFunctions.HyperEllipsoid(position);
                case 19:
                    return FitnessFunctions.DebsNo1(position);
                case 20:
                    return FitnessFunctions.Trid(position);
                case 21:
                    return FitnessFunctions.Rana(position);
                case 22:
                    return FitnessFunctions.Plateau(position);
                case 23:
                    return FitnessFunctions.CosineMixture(position);
                case 24:
                    return FitnessFunctions.Michalewicz(position);
                default:
                    throw new NotImplementedException();
            }
        }

        public static void DrawCharts()
        {
            for (int i = 0; i < 25; i++)
            {
                var plotCube = new PlotCube();
                List<double> xCoor = new List<double>();
                for (int j = -100; j < 101; j++)
                {
                    xCoor.Add(j);
                }

                List<double> yCoor = new List<double>();
                foreach (var item in xCoor)
                {
                    yCoor.Add(FitnessFunctions.EvaluateFitness(new double[] { item }, i)); //FitnessFunctions.Rastrigin(new double[] { item })) ;
                }

                var linePlot = new LinePlot(yCoor.ToArray());

                plotCube.Add(linePlot);

                var scene2D = new Scene()
                {
                    plotCube
                };


                using (var driver = new GDIDriver(scene: scene2D))
                {
                    scene2D.Configure();
                    driver.Render();
                    driver.BackBuffer.Bitmap.Save(i.ToString() + "_2D.png");
                }

                var scene = new Scene()
                {
                    new PlotCube(twoDMode: true)
                    {
                        new Surface((x, y) =>  (float)FitnessFunctions.EvaluateFitness(new double[]{ x, y }, i),  //   FitnessFunctions.Rastrigin(new double[]{x, y}),
                        xmin: -100, xmax: 100, xlen:100,
                        ymin: -100, ymax: 100, ylen: 100,
                        colormap: Colormaps.Hot)
                        {
                            UseLighting = true
                        }
                    }
                };

                scene.First<PlotCube>().Rotation = Matrix4.Rotation(Vector3.UnitX, .8f)
                                       * Matrix4.Rotation(Vector3.UnitZ, .6f);

                using (var driver = new GDIDriver(scene: scene))
                {
                    scene.Configure();
                    driver.Render();
                    driver.BackBuffer.Bitmap.Save(i.ToString() + "_3D.png");
                }
            }
        }


        /// <summary>
        /// chatGPT
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
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
        /// <summary>
        /// vygenerovaná ChatGPT
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double ChatGPTGen1(double[] values)
        {
            double sum = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Sin(values[i]) * Math.Exp(-Math.Pow(values[i], 2));
            }
            return sum;
        }
        /// <summary>
        /// ChatGPT
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double Rastrigin(double[] values)
        {
            return 10 * values.Length + values.Sum(x => Math.Sqrt(x) - 10 * Math.Cos(2 * Math.PI * x));
        }
        /// <summary>
        /// ChatGPT
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Schwefel(double[] x)
        {
            int d = x.Length;
            double sum = 0;
            for (int i = 0; i < d; i++)
            {
                sum += x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i])));
            }
            return 418.9829 * d - sum;
        }
        /// <summary>
        /// ChatGPT
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Levy(double[] x)
        {
            int d = x.Length;
            double w0 = 1 + (x[0] - 1) / 4;
            double wd = 1 + (x[d - 1] - 1) / 4;
            double term1 = Math.Pow(Math.Sin(Math.PI * w0), 2);
            double term3 = Math.Pow(wd - 1, 2) * (1 + Math.Pow(Math.Sin(2 * Math.PI * wd), 2));
            double sum = 0;
            for (int i = 0; i < d - 1; i++)
            {
                double wi = 1 + (x[i] - 1) / 4;
                sum += Math.Pow(wi - 1, 2) * (1 + 10 * Math.Pow(Math.Sin(Math.PI * wi + 1), 2));
            }
            return term1 + sum + term3;
        }
        /// <summary>
        /// ChatGPT
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Sphere(double[] x)
        {
            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i] * x[i];
            }
            return sum;
        }
        public static double SchwefelNo226(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum -= x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i])));
            }

            return sum;
        }
        public static double ChungReynolds(double[] x)
        {
            int dimension = x.Length;
            double sumOfSquares = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sumOfSquares += Math.Pow(x[i], 2);
            }

            return Math.Pow(sumOfSquares, 2);
        }
        public static double EggHolder(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension - 1; i++)
            {
                double term1 = -x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i] - x[i + 1] - 47)));
                double term2 = -(x[i + 1] + 47) * Math.Sin(Math.Sqrt(Math.Abs(0.5 * x[i] + x[i + 1] + 47)));

                sum += term1 + term2;
            }

            return sum;
        }
        public static double Quartic(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += (i + 1) * Math.Pow(x[i], 4);
            }

            return sum;
        }
        public static double YaoLiu09(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Pow(x[i], 2) - 10 * Math.Cos(2 * Math.PI * x[i]) + 10;
            }

            return sum;
        }
        public static double BentCigar(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 2; i < dimension; i++)
            {
                sum += Math.Pow(x[i], 2);
            }

            return Math.Pow(x[0], 2) + 1e6 * sum;
        }
        public static double Mishra11(double[] x)
        {
            int dimension = x.Length;

            double sum1 = 0.0;
            double product = 1.0;

            for (int i = 0; i < dimension; i++)
            {
                sum1 += Math.Abs(x[i]);
                product *= Math.Abs(x[i]);
            }

            double sum2 = Math.Pow(product, 1.0 / dimension);

            return Math.Pow((sum1 / dimension) - sum2, 2);
        }
         private static int Factorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }
        public static double Mishra07(double[] x)
        {
            int dimension = x.Length;
            double product = 1.0;

            for (int i = 0; i < dimension; i++)
            {
                product *= x[i];
            }

            int factorialN = Factorial(dimension);

            return Math.Pow(product - factorialN, 2);
        }
        public static double DixonPrice(double[] x)
        {
            double result = Math.Pow(x[0] - 1, 2);

            for (int i = 1; i < x.Length; i++)
            {
                double term = i * (2 * Math.Pow(x[i], 2) - x[i - 1]);
                result += Math.Pow(term, 2);
            }

            return result;
        }
        public static double Alpine01(double[] x)
        {
            double sum = 0.0;
            int dimension = x.Length;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Abs(x[i] * Math.Sin(x[i]) + 0.1 * x[i]);
            }

            return sum;
        }
        public static double StyblinskiTang(double[] x)
        {
            double sum = 0.0;
            int dimension = x.Length;

            for (int i = 0; i < dimension; i++)
            {
                double xi = x[i];
                sum += Math.Pow(xi, 4) - 16 * Math.Pow(xi, 2) + 5 * xi;
            }

            return 0.5 * sum; 
        }
        public static double HyperEllipsoid(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Pow(x[i], 2);
            }

            return sum;
        }
        public static double DebsNo1(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Pow(Math.Sin(5 * Math.PI * x[i]), 2);
            }

            return sum;
        }
        public static double Trid(double[] x)
        {
            int dimension = x.Length;
            double sum1 = 0.0;
            double sum2 = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum1 += Math.Pow(x[i] - 1, 2);
            }

            for (int i = 1; i < dimension; i++)
            {
                sum2 += x[i - 1] * x[i];
            }

            return sum1 - sum2;
        }
        public static double Rana(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension - 1; i++)
            {
                double term1 = x[i] * Math.Sin(Math.Sqrt(Math.Abs(x[i + 1] + 1 + x[i]))) * Math.Cos(Math.Sqrt(Math.Abs(x[i] - x[i + 1] + 1)));
                double term2 = (x[i + 1] + 1) * Math.Cos(Math.Sqrt(Math.Abs(x[i] - x[i + 1] + 1)));
                sum += term1 + term2;
            }

            return sum;
        }
        public static double Qing(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Pow(x[i] * x[i] - (i + 1), 2);
            }

            return sum;
        }
        public static double Plateau(double[] x)
        {
            int dimension = x.Length;
            double sum = 0.0;

            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Abs(x[i]);
            }

            return 30.0 + sum;
        }
        public static double CosineMixture(double[] x)
        {
            int dimension = x.Length;

            double sum1 = 0.0;
            for (int i = 0; i < dimension; i++)
            {
                sum1 += Math.Cos(5.0 * Math.PI * x[i]);
            }

            double sum2 = 0.0;
            for (int i = 0; i < dimension; i++)
            {
                sum2 += Math.Pow(x[i], 2);
            }
            return 0.1 * sum1 - sum2;
        }
        public static double Michalewicz(double[] x)
        {
            double sum = 0.0;
            int dimension = x.Length;
            double m = 10.0; 

            for (int i = 0; i < dimension; i++)
            {
                double xi = x[i];
                sum += Math.Sin(xi) * Math.Pow(Math.Sin(((i + 1) * xi * xi) / Math.PI), 2 * m);
            }

            return -sum; 
        }
    }
}
