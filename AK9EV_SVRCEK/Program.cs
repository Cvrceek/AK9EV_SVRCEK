﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK9EV_SVRCEK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PSO pso = new PSO(Dimenzion.Ten);
            pso.Run();
        }
    }
}
