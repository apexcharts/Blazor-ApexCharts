using System;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Builds the violin <see cref="ViolinDistribution"/> that apexcharts.js expects from a set of raw
    /// observations, using a Gaussian kernel-density estimate with a Silverman rule-of-thumb bandwidth.
    /// The core library renders the shape from this profile; it does not estimate density itself.
    /// </summary>
    internal static class ViolinDensity
    {
        // Number of samples along the value axis used to describe each density curve. 64 is smooth
        // enough for rendering while keeping the serialized payload small.
        private const int GridPoints = 64;

        /// <summary>
        /// Computes the density profile and raw-observation list for one violin.
        /// </summary>
        public static ViolinDistribution Compute(IReadOnlyList<double> observations)
        {
            var points = observations.Select(v => (decimal)v).ToList();

            if (observations.Count == 0)
            {
                return new ViolinDistribution { Density = new List<decimal[]>(), Points = points };
            }

            var n = observations.Count;
            var min = observations.Min();
            var max = observations.Max();

            var h = SilvermanBandwidth(observations);
            if (h <= 0 || double.IsNaN(h) || double.IsInfinity(h))
            {
                // Identical or single observations: use a narrow band so the violin is still visible.
                var span = max > min ? max - min : Math.Max(1.0, Math.Abs(max) * 0.01);
                h = span / 20.0;
            }

            var lo = min - 3 * h;
            var hi = max + 3 * h;
            if (hi <= lo) hi = lo + 1;

            var invNH = 1.0 / (n * h);
            var invSqrt2Pi = 1.0 / Math.Sqrt(2 * Math.PI);

            var density = new List<decimal[]>(GridPoints);
            for (var i = 0; i < GridPoints; i++)
            {
                var x = lo + (hi - lo) * i / (GridPoints - 1);
                var sum = 0.0;
                for (var k = 0; k < n; k++)
                {
                    var u = (x - observations[k]) / h;
                    sum += Math.Exp(-0.5 * u * u);
                }
                var weight = invNH * invSqrt2Pi * sum;
                density.Add(new[] { (decimal)x, (decimal)weight });
            }

            return new ViolinDistribution { Density = density, Points = points };
        }

        // Silverman's rule of thumb: h = 0.9 * min(std, IQR/1.349) * n^(-1/5).
        private static double SilvermanBandwidth(IReadOnlyList<double> x)
        {
            var n = x.Count;
            if (n < 2) return 0;

            var mean = x.Average();
            var variance = x.Sum(v => (v - mean) * (v - mean)) / (n - 1);
            var std = Math.Sqrt(variance);

            var iqr = InterquartileRange(x);
            var sigma = iqr > 0 ? Math.Min(std, iqr / 1.349) : std;
            if (sigma <= 0) sigma = std;

            return 0.9 * sigma * Math.Pow(n, -1.0 / 5.0);
        }

        private static double InterquartileRange(IReadOnlyList<double> x)
        {
            var sorted = x.OrderBy(v => v).ToArray();
            return Quantile(sorted, 0.75) - Quantile(sorted, 0.25);
        }

        // Linear-interpolation quantile over an already-sorted array.
        private static double Quantile(double[] sorted, double q)
        {
            if (sorted.Length == 0) return 0;
            if (sorted.Length == 1) return sorted[0];

            var pos = (sorted.Length - 1) * q;
            var lo = (int)Math.Floor(pos);
            var hi = (int)Math.Ceiling(pos);
            var frac = pos - lo;
            return sorted[lo] + (sorted[hi] - sorted[lo]) * frac;
        }
    }
}
