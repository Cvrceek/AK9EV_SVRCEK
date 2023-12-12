using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
