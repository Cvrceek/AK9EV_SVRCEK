using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public static class Helper
    {
        public static double BoundsCheck(double value)
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
    }
}
