using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    //převzato z : https://www.dropbox.com/sh/u3cpa39t9yh5fsf/AAB6EarzZ1NTl6iRGoyxAeB7a?dl=0&file_subpath=%2FPython_SOMA_ATO%2FSOMA_ATO.py&preview=Python_SOMA_ATO.zip
    public class SOMA_ATO
    {
        //public void Main()
        //{
        //    Console.WriteLine("Hello! SOMA ATO is working, please wait... ");

        //    // Control Parameters of SOMA
        //    double Step = 0.11, PRT = 0.1, PathLength = 3;

        //    // The domain (search space)
        //    double VarMin = -500, VarMax = 500;

        //    // Number of dimensions of the problem
        //    int dimension = 10;

        //    // Other parameters
        //    int PopSize = 100, Max_Migration = 100;

        //    // Create the initial Population
        //    double[,] pop = new double[dimension, PopSize];
        //    Random random = new Random();

        //    for (int i = 0; i < dimension; i++)
        //    {
        //        for (int j = 0; j < PopSize; j++)
        //        {
        //            pop[i, j] = VarMin + random.NextDouble() * (VarMax - VarMin);
        //        }
        //    }

        //    double[] fitness = EvaluatePopulation(pop);

        //    int FEs = PopSize;
        //    double the_best_cost = FindMinimum(fitness);

        //    // SOMA MIGRATIONS
        //    int Migration = 0;

        //    while (Migration < Max_Migration)
        //    {
        //        Migration++;

        //        int idx = FindIndexOfMinimum(fitness);
        //        double[] leader = GetColumn(pop, idx);

        //        for (int j = 0; j < PopSize; j++)
        //        {
        //            double[] indi_moving = GetColumn(pop, j);

        //            if (j != idx)
        //            {
        //                double[,] offspring_path = new double[dimension, 0];

        //                for (double k = Step; k < PathLength; k += Step)
        //                {
        //                    double[] PRTVector = GeneratePRTVector(dimension, PRT);

        //                    double[] offspring = AddArrays(indi_moving, MultiplyArrayScalar(SubtractArrays(leader, indi_moving), k, PRTVector));

        //                    offspring_path = ConcatenateArrays(offspring_path, ConvertTo2D(offspring));

        //                    int size = offspring_path.GetLength(1);

        //                    for (int cl = 0; cl < size; cl++)
        //                    {
        //                        for (int rw = 0; rw < dimension; rw++)
        //                        {
        //                            if (offspring_path[rw, cl] < VarMin || offspring_path[rw, cl] > VarMax)
        //                            {
        //                                offspring_path[rw, cl] = VarMin + random.NextDouble() * (VarMax - VarMin);
        //                            }
        //                        }
        //                    }

        //                    double[] new_cost = EvaluatePopulation(ConvertTo2D(offspring_path));
        //                    FEs += size;

        //                    double min_new_cost = FindMinimum(new_cost);
        //                    int idz = FindIndexOfMinimum(new_cost);

        //                    double[] the_best_offspring = GetColumn(ConvertTo2D(offspring_path), idz);

        //                    if (min_new_cost <= fitness[j])
        //                    {
        //                        fitness[j] = min_new_cost;
        //                        SetColumn(ref pop, j, the_best_offspring);

        //                        if (min_new_cost <= the_best_cost)
        //                        {
        //                            the_best_cost = min_new_cost;
        //                            SetColumn(ref pop, j, the_best_offspring);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    //Console.WriteLine("Stop at Migration:  " + Migration);
        //    //Console.WriteLine("The number of FEs:  " + FEs);
        //    //Console.WriteLine("The best cost:  " + the_best_cost);
        //    //Console.WriteLine("Solution values:  ");
        //}



        static double[] EvaluatePopulation(double[,] population)
        {
            int size = population.GetLength(1);
            double[] result = new double[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = CostFunction(population, i);
            }

            return result;
        }

        static double CostFunction(double[,] population, int index)
        {
            // Implement your cost function here
            // You can use population.GetLength(0) to get the dimension of the problem
            // and population[i, index] to access the value of the ith dimension for the individual at index
            throw new NotImplementedException();
        }

        static double FindMinimum(double[] array)
        {
            double min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }

            return min;
        }

        static int FindIndexOfMinimum(double[] array)
        {
            double min = array[0];
            int index = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    index = i;
                }
            }

            return index;
        }

        static double[] GetColumn(double[,] matrix, int columnIndex)
        {
            int rows = matrix.GetLength(0);
            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                result[i] = matrix[i, columnIndex];
            }

            return result;
        }

        static void SetColumn(ref double[,] matrix, int columnIndex, double[] values)
        {
            int rows = matrix.GetLength(0);

            for (int i = 0; i < rows; i++)
            {
                matrix[i, columnIndex] = values[i];
            }
        }

        static double[] GeneratePRTVector(int dimension, double PRT)
        {
            Random random = new Random();
            double[] result = new double[dimension];

            for (int i = 0; i < dimension; i++)
            {
                result[i] = (random.NextDouble() < PRT) ? 1 : 0;
            }

            return result;
        }

        static double[,] AddArrays(double[] array1, double[] array2)
        {
            int length = array1.Length;
            double[,] result = new double[length, 2];

            for (int i = 0; i < length; i++)
            {
                result[i, 0] = array1[i];
                result[i, 1] = array2[i];
            }

            return result;
        }

        static double[,] MultiplyArrayScalar(double[,] array, double scalar, double[] mask)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = array[i, j] * scalar * mask[i];
                }
            }

            return result;
        }

        static double[,] ConvertTo2D(double[] array)
        {
            int length = array.Length;
            double[,] result = new double[length, 1];

            for (int i = 0; i < length; i++)
            {
                result[i, 0] = array[i];
            }

            return result;
        }

        static double[,] ConcatenateArrays(double[,] array1, double[,] array2)
        {
            int rows = array1.GetLength(0);
            int cols1 = array1.GetLength(1);
            int cols2 = array2.GetLength(1);
            double[,] result = new double[rows, cols1 + cols2];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    result[i, j] = array1[i, j];
                }

                for (int j = 0; j < cols2; j++)
                {
                    result[i, cols1 + j] = array2[i, j];
                }
            }

            return result;
        }
    }
}
