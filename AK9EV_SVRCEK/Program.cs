using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace AK9EV_SVRCEK
{
    internal class Program
    {
        static int lowerFes = 2000;
        static async Task Main(string[] args)
        {
            //FitnessFunctions.DrawCharts();

            //var tasks = new List<Task<List<Result>>>
            //{
            //    Task.Run(() => DERand(Dimenzion.Two)),
            //    Task.Run(() => DERand(Dimenzion.Ten)),
            //    Task.Run(() => DERand(Dimenzion.Thirty)),
            //    Task.Run(() => DEBest(Dimenzion.Two)),
            //    Task.Run(() => DEBest(Dimenzion.Ten)),
            //    Task.Run(() => DEBest(Dimenzion.Thirty)),
            //    Task.Run(() => PSO_(Dimenzion.Two)),
            //    Task.Run(() => PSO_(Dimenzion.Ten)),
            //    Task.Run(() => PSO_(Dimenzion.Thirty)),
            //    Task.Run(() => SomaAllToAll(Dimenzion.Two)),
            //    Task.Run(() => SomaAllToAll(Dimenzion.Ten)),
            //    Task.Run(() => SomaAllToAll(Dimenzion.Thirty)),
            //    Task.Run(() => SomaAllToOne(Dimenzion.Two)),
            //    Task.Run(() => SomaAllToOne(Dimenzion.Ten)),
            //    Task.Run(() => SomaAllToOne(Dimenzion.Thirty))
            //};

            //var results = await Task.WhenAll(tasks);

            //var resLst = results.ToList();



            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(resLst);
            //File.WriteAllText("results.json", json);

            var csvString = "sep=;" + System.Environment.NewLine;
            var stringResults = File.ReadAllText("results.json");
            var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<Result>>>(stringResults);
            string[,] valuesForCSV = new string[5, 25];
            for(int dim = 0; dim < 3; dim++)
            {
                int vfCSVIndex = 0;
                for (int row = dim; row <= 12+dim; row += 3)
                {
                    var temp = results[row];
                    for(int fIndex = 0; fIndex < 25; fIndex++)
                    {
                        valuesForCSV[vfCSVIndex, fIndex] = temp[fIndex].Fitness.Average().ToString();
                    }
                    vfCSVIndex++;
                }

                csvString += "DIM: " + dim.ToString() + ";" + System.Environment.NewLine;
                for(int iF = 0; iF < 25; iF++)
                {
                    string line = "";
                    for(int column  = 0; column < 5; column++) 
                    {
                        line += valuesForCSV[column, iF] + ";";
                    }
                    line += System.Environment.NewLine;
                    csvString += line;
                }


            }

            File.WriteAllText("results.csv", csvString);
        }


        static async Task<List<Result>> SomaAllToAll(Dimenzion dimenzion)
        {
            SOMA soma = new SOMA(dimenzion, SOMA.SomaType.AllToAll, fes: dimenzion == Dimenzion.Thirty ? lowerFes : 2000);
            return soma.Run();
        }

        static async Task<List<Result>> SomaAllToOne(Dimenzion dimenzion)
        {
            SOMA soma = new SOMA(dimenzion, SOMA.SomaType.AllToOne, fes: dimenzion == Dimenzion.Thirty ? lowerFes : 2000);
            return soma.Run();
        }

        static async Task<List<Result>> PSO_(Dimenzion dimenzion)
        {
            PSO pso = new PSO(dimenzion, fes: dimenzion == Dimenzion.Thirty ? lowerFes : 2000);
            return pso.Run();
        }

        static async Task<List<Result>> DERand(Dimenzion dimenzion)
        {
            DE de = new DE(DE.DEType.Rand1Bin, dimenzion, fes: dimenzion == Dimenzion.Thirty ? lowerFes : 2000);
            return de.Run();
        }

        static async Task<List<Result>> DEBest(Dimenzion dimenzion)
        {
            DE de = new DE(DE.DEType.Best1Bin, dimenzion, fes: dimenzion == Dimenzion.Thirty ? lowerFes : 2000);
            return de.Run();
        }
        



 
    }
}
