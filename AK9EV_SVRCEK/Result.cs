using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    public class Result
    {

        public string Function{ get; set; }
        public List<double> Fitness { get; set; }

        public Result()
        {
            Fitness = new List<double>();
        }
    }
}
