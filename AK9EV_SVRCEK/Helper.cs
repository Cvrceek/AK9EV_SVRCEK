using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public static class Helper
    {
        public static double BoundsCheck_OLD(double value)
        {
            if (value < -100)
            {
                return 2 * -100 - value;
            }
            else if (value > 100)
            {
                return 2 * 100 - value;
            }
            else
                return value;
        }

        
        // 02-Zakladni_pojmy_funkce_benchmarkovani%20.pdf - slide 41
        public static double BoundsCheck(double value, double lowerBound = -100, double upperBound = 100)
        {
            if (value > upperBound)
            {
                return upperBound - (value - upperBound);
            }
            else if (value < lowerBound)
            {
                return lowerBound + (lowerBound - value);
            }
            else
                return value;
        }
    }
}
