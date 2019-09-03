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
            Magnitude = magnitude;
            Phase = phase;
        }

        public double Magnitude { get; set; }
        public double Frequency { get; set; }
        public double Phase { get; set; }

        private Complex ComputeAt(double time) => Magnitude * Complex.Exp(new Complex(0, (time * 2 * Math.PI * Frequency) + Phase));

        public Complex Value() => ComputeAt(0.0);

        public Complex Value(double time) => ComputeAt(time);

        public double RmsValue { get => Math.Sqrt(2.0) * Magnitude; set => Magnitude = value / (Math.Sqrt(2.0)); }

        public double Period => 1/Frequency;

        //TODO: Unit test Phasor Class.
    }
}
