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
        static void Main(string[] args)
        {
            FitnessFunctions.DrawCharts();


            ////////////////////////////
            //PSO pso = new PSO(Dimenzion.Thirty);
            //var pso30 = pso.Run();
            PSO pso = new PSO(Dimenzion.Two);
            var pso2 = pso.Run();
            pso = new PSO(Dimenzion.Ten);
            var pso10 = pso.Run();

            //Result psoRes = new Result();
            ////vytvoreni hodnot do reportu
            //for (int i = 0; i < pso2.Count; i++)
            //{
            //    psoRes.Fitness.Add((pso2[i].Fitness.Sum() + pso10[i].Fitness.Sum() + pso30[i].Fitness.Sum()) / 3);
            //}



            //DE rand = new DE(DE.DEType.Rand1Bin, Dimenzion.Thirty);
            //var rand30 = rand.Run();
            DE rand = new DE(DE.DEType.Rand1Bin, Dimenzion.Ten);
            var rand10 = rand.Run();
            rand = new DE(DE.DEType.Rand1Bin, Dimenzion.Two);
            var rand2 = rand.Run();

            //Result randRes = new Result();
            ////vytvoreni hodnot do reportu
            //for (int i = 0; i < rand30.Count; i++)
            //{
            //    randRes.Fitness.Add((rand10[i].Fitness.Sum() + rand2[i].Fitness.Sum() + rand30[i].Fitness.Sum()) / 3);
            //}

            //rand = new DE(DE.DEType.Best1Bin, Dimenzion.Thirty);
            //var rand30B = rand.Run();
            rand = new DE(DE.DEType.Best1Bin, Dimenzion.Ten);
            var rand10B = rand.Run();
            rand = new DE(DE.DEType.Best1Bin, Dimenzion.Two);
            var rand2B = rand.Run();

            //Result randBRes = new Result();
            ////vytvoreni hodnot do reportu
            //for (int i = 0; i < rand30B.Count; i++)
            //{
            //    randBRes.Fitness.Add((rand10B[i].Fitness.Sum() + rand2B[i].Fitness.Sum() + rand30B[i].Fitness.Sum()) / 3);
            //}

            //SOMA soma = new SOMA(Dimenzion.Thirty, SOMA.SomaType.AllToOne);
            //var soma30 = soma.Run();
            SOMA soma = new SOMA(Dimenzion.Ten, SOMA.SomaType.AllToOne);
            var soma10 = soma.Run();
            soma = new SOMA(Dimenzion.Two, SOMA.SomaType.AllToOne);
            var soma2 = soma.Run();
            //Result somaRes = new Result();
            ////vytvoreni hodnot do reportu
            //for (int i = 0; i < soma2.Count; i++)
            //{
            //    somaRes.Fitness.Add((soma2[i].Fitness.Sum() + soma10[i].Fitness.Sum() + soma30[i].Fitness.Sum()) / 3);
            //}


            //SOMA somaA = new SOMA(Dimenzion.Thirty, SOMA.SomaType.AllToAll);
            //var soma30A = soma.Run();
            SOMA somaA = new SOMA(Dimenzion.Ten, SOMA.SomaType.AllToAll);
            var soma10A = somaA.Run();
            somaA = new SOMA(Dimenzion.Two, SOMA.SomaType.AllToAll);
            var soma2A = somaA.Run();

            //Result somaResA = new Result();
            ////vytvoreni hodnot do reportu
            //for (int i = 0; i < soma2.Count; i++)
            //{
            //    somaResA.Fitness.Add((soma2A[i].Fitness.Sum() + soma10A[i].Fitness.Sum() + soma30A[i].Fitness.Sum()) / 3);
            //}
        }



 
    }
}
