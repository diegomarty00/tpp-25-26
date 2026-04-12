using System;
using activity10;
namespace TPP.Concurrency.Threads
{

    public class VectorModulusProgram
    {

        static void Main(string[] args)
        {
            Ejercicio();
        }

        /// <summary>
        /// Creates a random vector of short numbers
        /// </summary>
        /// <param name="numberOfElements">The size of the vector</param>
        /// <param name="lowest">The lowest value to be used in the generation of vector elements</param>
        /// <param name="greatest">The greatest value to be used in the generation of vector elements</param>
        /// <returns>The random vector</returns>
        public static short[] CreateRandomVector(int numberOfElements, short lowest, short greatest)
        {
            short[] vector = new short[numberOfElements];
            Random random = new Random();
            for (int i = 0; i < numberOfElements; i++)
                vector[i] = (short)random.Next(lowest, greatest + 1);
            return vector;
        }


        public static long CountSequential(BitcoinValueData[] data, double threshold)
        {
            long count = 0;
            foreach (var d in data)
                if (d.Value >= threshold)
                    count++;
            return count;
        }

        public static void Ejercicio()
        {
            
            BitcoinValueData[] data = Utils.GetBitcoinData();
            double threshold = 7000;

            CountSequential(data, threshold);
            Console.WriteLine("hilos;ejecucion;ticks");
            for (int threads = 1; threads <= 50; threads++)
            {
                for (int run = 0; run < 15; run++)
                {
                    Master master = new Master(data, threads, threshold);

                    DateTime before = DateTime.Now;
                    long result = master.ComputeCount();
                    DateTime after = DateTime.Now;

                    Console.WriteLine($"{threads};{run};{(after - before).Ticks}");

                    GC.Collect();
                    GC.WaitForFullGCComplete();
                }
            }
        }
    }

}
