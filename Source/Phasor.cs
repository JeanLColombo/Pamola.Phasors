using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pamola.Phasor
{
    public class Phasor : IPhasor
    {
        public Phasor(double magnitude, double phase)
        {
            var negative = Convert.ToDouble(magnitude < 0.0);

            Magnitude = magnitude * (1 - 2 * negative);
            Phase = phase + negative * TrigonometryExtensions.Degree2Radians(180);
        }

        public Phasor(double magnitude, double phase, double frequency)
            : this(magnitude, phase)
        {
            Frequency = frequency;
        }

        public Phasor() : this(0.0, 0.0) { }


        public double Magnitude { get; set; }

        public double Frequency { get; set; }

        public double Phase { get; set; }

        private Complex ComputeAt(double time) => Magnitude * Complex.Exp(new Complex(0, (time * 2 * Math.PI * Frequency) + Phase));

        public Complex Value() => ComputeAt(0.0);

        public Complex Value(double time) => ComputeAt(time);

        public double PeakValue { get => Math.Sqrt(2.0) * RmsValue; set => Magnitude = value / (Math.Sqrt(2.0)); }

        public double InstantValue(double time) => Math.Sqrt(2.0) * Value(time).Real;

        public double RmsValue => Magnitude;

        public double Period
        {
            get
            {
                if (Frequency == default(double))
                    throw new DivideByZeroException($"Phasor frequency set to {default(double)}. Period tends to infinity.");

                return 1 / Frequency;
            }
        }

    }
}
