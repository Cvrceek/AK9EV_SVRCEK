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
            //PSO pso = new PSO(Dimenzion.Thirty);
            //var pso30 = pso.Run();
            //pso = new PSO(Dimenzion.Two);
            //var pso2 = pso.Run();
            //pso = new PSO(Dimenzion.Ten);
            //var pso10 = pso.Run();

            DE_Rand1Bin rand = new DE_Rand1Bin(Dimenzion.Thirty);
            var rand30 = rand.Run();
        }
    }
}
