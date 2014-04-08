using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Utils
    {
        public static void Normalise(ref double dx, ref double dy)
        {
            double norm = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            dx /= norm;
            dy /= norm;
        }
    }
}
