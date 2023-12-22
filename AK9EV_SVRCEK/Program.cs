using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace AK9EV_SVRCEK
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //FitnessFunctions.DrawCharts();

            Task<List<Result>> deRand_2 = DERand(Dimenzion.Two);
            Task<List<Result>> deRand_10 = DERand(Dimenzion.Ten);
            Task<List<Result>> deRand_30 = DERand(Dimenzion.Thirty);

            Task<List<Result>> deBest_2 =  DEBest(Dimenzion.Two);
            Task<List<Result>> deBest_10 = DEBest(Dimenzion.Ten);
            Task<List<Result>> deBest_30 = DEBest(Dimenzion.Thirty);

            Task<List<Result>> pso_2 = PSO_(Dimenzion.Two);
            Task<List<Result>> pso_10 = PSO_(Dimenzion.Ten);
            Task<List<Result>> pso_30 = PSO_(Dimenzion.Thirty);

            Task<List<Result>> somaAllToAll_2 =  SomaAllToAll(Dimenzion.Two);
            Task<List<Result>> somaAllToAll_10 = SomaAllToAll(Dimenzion.Ten);
            Task<List<Result>> somaAllToAll_30 = SomaAllToAll(Dimenzion.Thirty);

            Task<List<Result>> somaAllToOne_2 =  SomaAllToOne(Dimenzion.Two);
            Task<List<Result>> somaAllToOne_10 = SomaAllToOne(Dimenzion.Ten);
            Task<List<Result>> somaAllToOne_30 = SomaAllToOne(Dimenzion.Thirty);

            var results = Task.WhenAll(deRand_2, deRand_10, /*deRand_30*/, deBest_2, deBest_10, /*deBest_30*/, pso_2, pso_10, /*pso_30*/, somaAllToAll_2, somaAllToAll_10, /*somaAllToAll_30*/,
                somaAllToOne_2, somaAllToOne_10, /*somaAllToOne_30*/);



        }


        static async Task<List<Result>> SomaAllToAll(Dimenzion dimenzion)
        {
            SOMA soma = new SOMA(dimenzion, SOMA.SomaType.AllToAll);
            return soma.Run();
        }

        static async Task<List<Result>> SomaAllToOne(Dimenzion dimenzion)
        {
            SOMA soma = new SOMA(dimenzion, SOMA.SomaType.AllToOne);
            return soma.Run();
        }

        static async Task<List<Result>> PSO_(Dimenzion dimenzion)
        {
            PSO pso = new PSO(dimenzion);
            return pso.Run();
        }

        static async Task<List<Result>> DERand(Dimenzion dimenzion)
        {
            DE de = new DE(DE.DEType.Rand1Bin, dimenzion);
            return de.Run();
        }

        static async Task<List<Result>> DEBest(Dimenzion dimenzion)
        {
            DE de = new DE(DE.DEType.Best1Bin, dimenzion);
            return de.Run();
        }
        



 
    }
}
