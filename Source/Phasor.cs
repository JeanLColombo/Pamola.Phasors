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
            Phase = phase;
            Magnitude = magnitude;
        }

        public Phasor(double magnitude, double phase, double frequency)
            : this(magnitude, phase)
        {
            Frequency = frequency;
        }

        private double _magnitude;
        private double _phase;


        public Phasor() : this(0.0, 0.0) { }

        public double Magnitude
        {
            get => _magnitude;
            set
            {
                var negative = Convert.ToDouble(value < 0.0);

                Phase += negative * TrigonometryExtensions.Degree2Radians(180);
                _magnitude = value * (1 - 2 * negative);
            }
        }

        public double Frequency { get; set; }

        public double Phase { get => _phase; set => _phase = value; }

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
