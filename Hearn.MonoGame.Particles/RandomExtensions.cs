using System;
using System.Collections.Generic;
using System.Text;

namespace Hearn.MonoGame.Particles
{
    static class RandomExtensions
    {
        public static double NextDouble(this Random rnd, double minValue, double maxValue)
        {
            return rnd.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static float NextFloat(this Random rnd, double minValue, double maxValue)
        {
            return (float)rnd.NextDouble(minValue, maxValue);
        }
    }
}
