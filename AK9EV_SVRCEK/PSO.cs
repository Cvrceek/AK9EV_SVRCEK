using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public class PSO
    {
        #region SubClasses
        public class Particle
        {
            public double Speed { get; set; }
            public double[] Best { get; set; }
            public double[] Position { get; set; }
            public double Fitness { get; set; }
        }

        public class Swarm
        {
            public List<Particle> Particles { get; set; }
            public double[] Best { get; set; }
            public double Fitness { get; set; }
            public int SwarmSize { get; }

            public Swarm(int swarmSize, int dimenzion)
            {
                EVRandom random = new EVRandom();
                SwarmSize = swarmSize;

                Particles = new List<Particle>();
                for(int i = 0; i < swarmSize; i++)
                {
                    var particle = new Particle()
                    {
                        Speed = 0,
                        Best = new double[dimenzion],
                        Position = new double[dimenzion],
                        Fitness = double.MaxValue
                    };
                    for(int j = 0; j < dimenzion; j++)
                    {
                        particle.Position[j] = random.EVNextDouble();
                    }
                    Particles.Add(particle);
                }
                Best = new double[dimenzion];
                Fitness = double.MaxValue;
            }
        }
        #endregion
        public Dimenzion Dimenzion { get; set; }
        public int FES { get; set; }
        public int Iterations { get; set; }
        public Swarm SwarmItems { get; set; }
        public double Weight { get; set; }
        public double C1 { get; set; }
        public double C2 { get; set; }
        public int MaxVelocity { get; set; }
        public PSO(Dimenzion dimenzion, int fes = 2000, int iterations = 30, double c1 = 1.49618, double c2= 1.49618, double weight = 0.7298, int maxVelocity=2)
        {
            Dimenzion = dimenzion;
            Weight = weight;
            C1 = c1;
            C2 = c2;
            MaxVelocity = maxVelocity;
            //switch (this.Dimenzion)
            //{
            //    case Dimenzion.Two:
            //        SwarmItems = new Swarm(10,2);
            //        break;
            //    case Dimenzion.Ten:
            //        SwarmItems = new Swarm(20,10);
            //        break;
            //    case Dimenzion.Thirty:
            //        SwarmItems = new Swarm(50,30);
            //        break;
            //}
            FES = fes * (int)dimenzion;
            Iterations = iterations;
        }

        public void Run()
        {
            EVRandom random = new EVRandom();
            
            for (int iteraiton = 0; iteraiton < Iterations; iteraiton++)
            {
                int FEScounter = 0;

                switch (this.Dimenzion)
                {
                    case Dimenzion.Two:
                        SwarmItems = new Swarm(10, 2);
                        break;
                    case Dimenzion.Ten:
                        SwarmItems = new Swarm(20, 10);
                        break;
                    case Dimenzion.Thirty:
                        SwarmItems = new Swarm(50, 30);
                        break;
                }

                while (FEScounter < FES)
                {
                    foreach (var particle in SwarmItems.Particles)
                    {
                        for (int i = 0; i < particle.Position.Length && FEScounter < FES; i++)
                        {
                            //vzorecek na rychlot
                            particle.Speed = Weight * particle.Speed +
                                         C1 * random.NextDouble() * (particle.Best[i] - particle.Position[i]) +
                                         C2 * random.NextDouble() * (SwarmItems.Best[i] - particle.Position[i]);
                            if (particle.Speed > MaxVelocity || particle.Speed < -MaxVelocity)
                            {
                                particle.Speed = Math.Max(-MaxVelocity, Math.Min(MaxVelocity, particle.Speed));
                            }
                            //pozicovani
                            double newPosition = particle.Position[i] + particle.Speed;
                            particle.Position[i] = Math.Max(-100, Math.Min(100, newPosition));
                            FEScounter++;
                        }

                        double fitness = EvaluateFitness(particle.Position);

                        if (fitness < particle.Fitness)
                        {
                            particle.Fitness = fitness;
                            Array.Copy(particle.Position, particle.Best, particle.Position.Length);
                        }

                        if (fitness < SwarmItems.Fitness)
                        {
                            SwarmItems.Fitness = fitness;
                            Array.Copy(particle.Position, SwarmItems.Best, particle.Position.Length);
                        }
                    }
                }

                Console.WriteLine(SwarmItems.Best);
                Console.WriteLine(SwarmItems.Fitness);
                Console.WriteLine();
            }
        }


          private double EvaluateFitness(double[] position)
        {
            // Hodnoty x a y z pole position
            double x = position[0];
            double y = position[1];

            // Kvadratická fitness funkce
            double fitness = Math.Pow(x - 3, 2) + Math.Pow(y + 2, 2);

            return fitness;
        }
    }
}
