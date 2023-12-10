using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public class EVRandom : Random
    {
        public EVRandom() : base(GetSeed()) 
        {
            
        }

        public double EVNextDouble()
        {
            return base.NextDouble() * (200) + 100;
        }

        private static int GetSeed()
        {
            byte[] seedArray = new byte[4];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(seedArray);

            return BitConverter.ToInt32(seedArray, 0);
        }
    }
}
