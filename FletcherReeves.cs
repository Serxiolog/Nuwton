using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Nuwton
{
    class FletcherReeves
    {
        public List<double> C1 { get; set; } = new List<double>();
        public List<double> C2 { get; set; } = new List<double>();
        public List<(double min, double max)> Bounds { get; set; } = new List<(double, double)>();

        public (List<double> x, List<string> log) LoadData(List<double> x1, List<double> x2, double x1L, double x1R, double x2L, double x2R)
        {
            C1 = x1;
            C2 = x2;

            var random = new Random();
            var x0 = new List<double> {
                random.NextDouble() * (x1R - x1L) + x1L,
                random.NextDouble() * (x2R - x2L) + x2L
            };

            Bounds = new() {
                (x1L, x1R),
                (x2L, x2R),
            };


            return Optimize(x0, maxIter: 200);
        }

        public (List<double> x, List<string> log) Optimize(List<double> x0, double tol = 1e-6, int maxIter = 1000)
        {
            var log = new List<string>();
            var x = ProjectToBounds(x0);
            var grad = Gradient(x);
            var direction = Negate(grad);
            bool converged = false;

            log.Add("Iter |       x1       |       x2       |   F(x)   |  Step  |  Grad Norm ");
            log.Add("-----------------------------------------------------------------------");

            for (int iterations = 0; iterations < maxIter; iterations++)
            {
                var step = LineSearch(x, direction, grad);
                var xNew = AddVectors(x, MultiplyVectorByScalar(direction, step));
                xNew = ProjectToBounds(xNew);

                var gradNew = Gradient(xNew);
                var gradNorm = EuclideanNorm(gradNew);

                log.Add($"{iterations} | {x[0]:F6} | {x[1]:F6} | {F(x):F4} | {step:F4} | {gradNorm:F6}");

                if (gradNorm < tol)
                {
                    converged = true;
                    break;
                }

                var beta = DotProduct(gradNew, gradNew) / DotProduct(grad, grad);
                direction = AddVectors(Negate(gradNew), MultiplyVectorByScalar(direction, beta));

                x = xNew;
                grad = gradNew;
            }

            log.Add($"\nConverged: {converged}, Ans: {x[0]};{x[1]}");
            return (x, log);
        }

        private double F(List<double> x)
        {
            double x1 = C1.Select((c, i) => c * Math.Pow(x[0], 6 - i)).Sum();
            double x2 = C2.Select((c, i) => c * Math.Pow(x[1], 6 - i)).Sum();

            return x1 + x2;
                 
        }

        private List<double> Gradient(List<double> x)
        {
            return new List<double>
            {
                C1.Take(C1.Count - 1)
                    .Select((c, i) => c * (6 - i) * Math.Pow(x[0], 5 - i))
                    .Sum(),

                C2.Take(C2.Count - 1)
                    .Select((c, i) => c * (6 - i) * Math.Pow(x[1], 5 - i))
                    .Sum()
            };
        }

        private List<double> ProjectToBounds(List<double> x)
        {
            return new List<double>
            {
                Math.Clamp(x[0], Bounds[0].min, Bounds[0].max),
                Math.Clamp(x[1], Bounds[1].min, Bounds[1].max)
            };
        }

        private double LineSearch(List<double> x, List<double> direction, List<double> grad, double alpha = 0.1, double beta = 0.5)
        {
            double step = 1.0;
            var f0 = F(x);

            while (true)
            {
                var xNew = ProjectToBounds(AddVectors(x, MultiplyVectorByScalar(direction, step)));
                var fNew = F(xNew);

                if (fNew <= f0 + alpha * step * DotProduct(grad, direction) || step < 1e-6)
                    break;

                step *= beta;
            }
            return step;
        }

        // Векторные операции
        private List<double> AddVectors(List<double> a, List<double> b) =>
            new List<double> { a[0] + b[0], a[1] + b[1] };

        private List<double> MultiplyVectorByScalar(List<double> v, double s) =>
            new List<double> { v[0] * s, v[1] * s };

        private List<double> Negate(List<double> v) =>
            new List<double> { -v[0], -v[1] };

        private double DotProduct(List<double> a, List<double> b) =>
            a.Zip(b, (ai, bi) => ai * bi).Sum();

        private double EuclideanNorm(List<double> v) =>
            Math.Sqrt(v.Sum(x => x * x));

    }
}
