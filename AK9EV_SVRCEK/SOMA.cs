using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static AK9EV_SVRCEK.PSO;

namespace AK9EV_SVRCEK
{
    public class SOMA
    {
        public enum SomaType
        {
            AllToOne,
            AllToAll
        }
        public class Individual
        {
            public double[] Position { get; set; }
            public double Fitness { get; set; }

            public Individual(int dimension)
            {
                Position = new double[dimension];
            }
        }
        public Dimenzion Dimenzion { get; set; }
        public int FES { get; set; }
        public int Iterations { get; set; }
        public double PathLength { get; set; }
        public double StepSize { get; set; }
        public double PRT { get; set; }
        public int PopulationSize { get; set; }
        public List<Individual> Population { get; set; }
        public Individual Leader { get; set; }
        EVRandom Random = new EVRandom();
        SomaType Type;

        public SOMA(Dimenzion dimenzion, SomaType type, int fes = 2000, int iterations = 30, double pathLength = 3, double stepSize = 0.11, double prt = 0.7)
        {
            FES = fes * (int)dimenzion;
            Dimenzion = dimenzion;
            Iterations = iterations;
            PathLength = pathLength;
            StepSize = stepSize;
            PRT = prt;
            Type = type;
            Population = new List<Individual>();
            switch (Dimenzion)
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


            for (int i = 0; i < PopulationSize; i++)
            {
                var individual = new Individual((int)Dimenzion);
                for (int j = 0; j < (int)Dimenzion; j++)
                {
                    individual.Position[j] = Random.EVNextDouble();
                }
                Population.Add(individual);
            }
        }
        public List<Result> Run()
        {
            List<Result> retLst = new List<Result>();

            for (int fce = 0; fce < 25; fce++)
            {
                Result result = new Result();
                result.Function = fce.ToString();
                for (int iteraiton = 0; iteraiton < Iterations; iteraiton++)
                {
                    int fesCounter = 0;
                    Population = new List<Individual>();
                    for (int i = 0; i < PopulationSize; i++)
                    {
                        var individual = new Individual((int)Dimenzion);
                        for (int j = 0; j < (int)Dimenzion; j++)
                        {
                            individual.Position[j] = Random.EVNextDouble();
                        }
                        Population.Add(individual);
                    }



                    while (fesCounter < FES)
                    {
                        if (Type == SomaType.AllToOne)
                            AllToOne(ref fesCounter);
                        else
                            AllToAll(ref fesCounter);
                        
                    }
                    if(Type == SomaType.AllToAll)
                        Leader = Population.OrderByDescending(individual => individual.Fitness).First();

                    result.Fitness.Add(Leader.Fitness);
                }
                retLst.Add(result);
            }

            return retLst;
        }

        private void AllToOne(ref int fesCounter)
        {
            Leader = Population.OrderByDescending(individual => individual.Fitness).First();

            foreach (var item in Population)
            {
                double[] tempPosition = new double[(int)Dimenzion];
                for (int i = 0; i < (int)Dimenzion; i++)
                {
                    double lenght = StepSize * Random.EVNextDouble() * PathLength;
                    double ptr = Random.NextDouble() < PRT ? 1 : 0;
                    tempPosition[i] = item.Position[i] + ptr * lenght * (Leader.Position[i] - item.Position[i]);
                }

                double fittness = FitnessFunctions.EvaluateFitness(tempPosition, 1);
                fesCounter++;
                if (fittness > item.Fitness)
                {
                    item.Position = tempPosition;
                    item.Fitness = fittness;
                }
            }
        }

        private void AllToAll(ref int fesCounter)
        {
            foreach (var leader in Population)
            {
                foreach(var item2 in Population.Where(x => x != leader))
                {
                    double[] tempPosition = new double[(int)Dimenzion];
                    for (int i = 0; i < (int)Dimenzion; i++)
                    {
                        double lenght = StepSize * Random.EVNextDouble() * PathLength;
                        double ptr = Random.NextDouble() < PRT ? 1 : 0;
                        tempPosition[i] = leader.Position[i] + ptr * lenght * (item2.Position[i] - leader.Position[i]);
                    }

                    double fittness = FitnessFunctions.EvaluateFitness(tempPosition, 1);
                    fesCounter++;
                    if (fittness > leader.Fitness)
                    {
                        leader.Position = tempPosition;
                        leader.Fitness = fittness;
                    }
                }
            }
        }
    }
}
