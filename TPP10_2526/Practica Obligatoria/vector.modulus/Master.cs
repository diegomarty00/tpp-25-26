using System;
using System.Threading;
using activity10;

namespace TPP.Concurrency.Threads
{

    /// <summary>
    /// Computes the modulus (magnitude) of a data array concurrently.
    /// Creates and coordinates a set of worker threads to perform the computation.
    /// </summary>
    public class Master
    {

        /// <summary>
        /// The data array whose modulus is going to be computed.
        /// </summary>
        private BitcoinValueData[] data;

        /// <summary>
        /// Number of worker threads used in the computation.
        /// </summary>
        private int numberOfThreads;

        /// <summary>
        /// The threshold value used in the computation. 
        /// Only values greater than or equal to this threshold will 
        /// be considered in the modulus calculation. 
        /// </summary>
        private double threshold;

        public Master(BitcoinValueData[] data, int numberOfThreads, double threshold)
        {
            if (numberOfThreads < 1 || numberOfThreads > data.Length)
                throw new ArgumentException("The number of threads must be lower or equal to the elements of the vector");
            this.data = data;
            this.numberOfThreads = numberOfThreads;
            this.threshold = threshold;
        }

        /// <summary>
        /// The method that computes the modulus.
        /// </summary>
        public double ComputeModulus()
        {
            // * Workers are created
            Worker[] workers = new Worker[this.numberOfThreads];
            int elementsPerThread = this.data.Length / numberOfThreads;
            for (int i = 0; i < this.numberOfThreads; i++)
                workers[i] = new Worker(this.data,
                    i * elementsPerThread,
                    (i < this.numberOfThreads - 1) ? (i + 1) * elementsPerThread - 1 : this.data.Length - 1 // last one
                    );
            // * Threads are concurrently started
            Thread[] threads = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                threads[i] = new Thread(workers[i].Compute); // we create the threads
                threads[i].Name = "Data modulus worker " + (i + 1); // we name then (optional)
                threads[i].Priority = ThreadPriority.Normal; // we assign them a priority (optional)
                threads[i].Start();   // we start their execution
            }
            // * We wait for them to conclude their computation
            foreach (Thread thread in threads)
                thread.Join();
            // * Finally, we add their values and compute the square root
            long result = 0;
            foreach (Worker worker in workers)
                result += worker.Result;
            return Math.Sqrt(result);
        }


        public long ComputeCount()
        {

            Worker[] workers = new Worker[numberOfThreads];
            Thread[] threads = new Thread[numberOfThreads];

            int elementsPerThread = data.Length / numberOfThreads;

            for (int i = 0; i < numberOfThreads; i++)
            {

                int from = i * elementsPerThread;
                int to = (i == numberOfThreads - 1)
                         ? data.Length - 1
                         : (i + 1) * elementsPerThread - 1;

                workers[i] = new Worker(data, from, to, threshold);
                threads[i] = new Thread(workers[i].Compute);
                threads[i].Start();
            }

            foreach (Thread t in threads)
                t.Join();

            long total = 0;
            foreach (var w in workers)
                total += w.Result;

            return total;
        }


    }

}
