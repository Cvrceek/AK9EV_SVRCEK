using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace AK9EV_SVRCEK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var plotCube = new PlotCube();
            List<double> xCoor = new List<double>();
            for (int i = -100; i < 101; i++)
            {
                xCoor.Add(i);
            }

            List<double> yCoor = new List<double>();
            foreach(var item in xCoor)
            {
                yCoor.Add(FitnessFunctions.My1(new double[] {item}));
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
                driver.BackBuffer.Bitmap.Save("2D.png");
            }










            var scene = new Scene()
            {
                new PlotCube(twoDMode: true)
                {
                    new Surface((x, y) =>  (float)FitnessFunctions.My1(new double[]{x, y}),
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
                driver.BackBuffer.Bitmap.Save("3D.png");
            }


            ////////////////////////////
            PSO pso = new PSO(Dimenzion.Thirty);
            var pso30 = pso.Run();
            pso = new PSO(Dimenzion.Two);
            var pso2 = pso.Run();
            pso = new PSO(Dimenzion.Ten);
            var pso10 = pso.Run();

            DE rand = new DE(DE.DEType.Rand1Bin, Dimenzion.Thirty);
            var rand30 = rand.Run();

            rand = new DE(DE.DEType.Best1Bin, Dimenzion.Thirty);
            //var randjj = rand.Run();
        }



 
    }
}
