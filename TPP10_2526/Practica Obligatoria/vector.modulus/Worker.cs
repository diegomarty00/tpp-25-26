using activity10;
using System;
namespace TPP.Concurrency.Threads
{

    /// <summary>
    /// Computes the addition of the square values of part of a data array
    /// </summary>
    internal class Worker
    {

        /// <summary>
        /// The data array whose modulus is going to be computed.
        /// </summary>
        private BitcoinValueData[] data;

        /// <summary>
        /// Indices of the data array indicating the elements to be used in the computation.
        /// Both fromIndex and toIndex are included in the process.
        /// </summary>
        private int fromIndex, toIndex;
        private double threshold;

        /// <summary>
        /// The result of the computation
        /// </summary>
        private long result;

        internal long Result
        {
            get { return this.result; }
        }

        public Worker(BitcoinValueData[] data, int fromIndex, int toIndex, double threshold)
        {
            this.data = data;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.threshold = threshold;
        }

        public Worker(BitcoinValueData[] data, int fromIndex, int toIndex)
        {
            this.data = data;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
        }

        /// <summary>
        /// Method that computes the addition of the squares
        /// </summary>
        internal void Compute()
        {
            result = 0;
            for (int i = fromIndex; i <= toIndex; i++)
            {
                if (data[i].Value >= threshold)
                    result++;
            }
        }

    }

}
