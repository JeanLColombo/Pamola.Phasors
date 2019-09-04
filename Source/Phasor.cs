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

        private double magnitude;
        private double frequency;

        public double Magnitude { get => magnitude; set { magnitude = CheckPhasorIntegrity(value, Magnitude); } }
        public double Frequency { get => frequency; set { frequency = CheckPhasorIntegrity(value, Frequency); } }
        public double Phase { get; set; }

        private Complex ComputeAt(double time) => Magnitude * Complex.Exp(new Complex(0, (time * 2 * Math.PI * Frequency) + Phase));

        public Complex Value() => ComputeAt(0.0);

        public Complex Value(double time) => ComputeAt(time);

        public double PeakValue { get => Math.Sqrt(2.0) * RmsValue; set => Magnitude = value / (Math.Sqrt(2.0)); }

        public double InstantValue(double time) => Math.Sqrt(2.0) * Value(time).Real;

        public double RmsValue => Magnitude;

        public double Period => 1/Frequency;

        private double CheckPhasorIntegrity(double parameter, double property)
        {
            if (parameter < 0)
                throw new ArgumentOutOfRangeException(nameof(parameter), parameter, "{nameof(property)} cannot be negative.");

            return parameter;
        }

        //TODO: Unit test Phasor Class.
    }
}
