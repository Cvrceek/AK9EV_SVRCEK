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
                //case 5:
                //    return FitnessFunctions.My5(position);
                default:
                    throw new NotImplementedException();
            }
        }

        public static void DrawCharts()
        {
            for (int i = 0; i < 5; i++)
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
    }
}
