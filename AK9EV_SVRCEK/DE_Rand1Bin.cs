using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static AK9EV_SVRCEK.PSO;

namespace AK9EV_SVRCEK
{
    public class DE_Rand1Bin
    {
        public Dimenzion Dimenzion { get; set; }
        public int FES { get; set; }
        public int Iterations { get; set; }
        public List<double[]> Population { get; set; }
        public double F { get; set; }
        public double CR { get; set; }
        public int MaxGenerations { get; set; }
        private int PopulationSize { get; set; }
        public double Fitness { get; set; }

        //maxGen 10^3 - 10^6 viz prednasky
        public DE_Rand1Bin(Dimenzion dimenzion, int fes = 2000, int iterations = 30, double f = 0.8, double cr = 0.9, int maxGen = 100000)
        {
            Dimenzion = dimenzion;
            FES = fes * (int)dimenzion;
            Iterations = iterations;
            F = f;
            CR = cr;
            MaxGenerations = maxGen;
            Fitness = double.MaxValue;

            Population = new List<double[]>();

            switch (this.Dimenzion)
            {
                case Dimenzion.Two:
                    PopulationSize = 10;
                    break;
                case Dimenzion.Ten:
                    PopulationSize = 20;
                    break;
                case Dimenzion.Thirty:
                    PopulationSize = 50;
                    break;
            }

            EVRandom random = new EVRandom();
            for (int i = 0; i < PopulationSize; i++)
            {
                var array = new double[PopulationSize];

                for (int j = 0; j < (int)Dimenzion; j++)
                {
                    array[j] = random.EVNextDouble();
                }
                Population.Add(array);
            }
        }

        public List<Result> Run()
        {
            List<Result> retLst = new List<Result>();
            for (int fce = 0; fce < 3; fce++)
            {
                Result result = new Result();
                result.Function = fce.ToString();

                for (int iteraiton = 0; iteraiton < Iterations; iteraiton++)
                {
                    int FESCounter = 0;
                    double bestFitness = double.MaxValue;

                    for (int gen = 0; gen < MaxGenerations && FESCounter < FES; gen++)
                    {
                        for (int i = 0; i < PopulationSize && FESCounter < FES; i++)
                        {
                            EVRandom random = new EVRandom();

                            double[] r1 = Population[random.Next(PopulationSize)];
                            double[] r2 = Population[random.Next(PopulationSize)];
                            double[] r3 = Population[random.Next(PopulationSize)];

                            double[] actualVector = Population[i];
                            double[] newVector = new double[(int)Dimenzion];

                            for (int j = 0; j < (int)Dimenzion; j++)
                            {
                                if (random.NextDouble() < CR)
                                    newVector[j] = r1[j] + F * (r2[j] - r3[j]);
                                else
                                    newVector[j] = actualVector[j];
                            }

                            double actulFitness = FitnessFunctions.EvaluateFitness(actualVector, fce);
                            FESCounter++;
                            double newFitness = FitnessFunctions.EvaluateFitness(newVector, fce);
                            FESCounter++;
                            if (newFitness < actulFitness)
                            {
                                Population[i] = newVector;
                                if (newFitness < bestFitness)
                                    bestFitness = newFitness;
                            }
                        }
                    }
                    result.Fitness.Add(bestFitness);
                }
                retLst.Add(result);
            }
            return retLst;
        }
    }
}
