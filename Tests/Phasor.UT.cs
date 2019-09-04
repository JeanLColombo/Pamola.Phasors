using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using Xunit;

namespace Pamola.Phasor.UT
{
    public class PhasorUT
    {

        public static IEnumerable<object[]> phasorData { get; } = new List<object[]>()
        {
            new object[]
            {
                10.0,
                0.0,
                new Complex(10.0,0.0)
            },
            new object[]
            {
                5.0,
                30.0,
                new Complex(4.330127019, 2.5)
            },
            new object[]
            {
                12.0,
                90.0,
                new Complex(0.0, 12.0)
            },
            new object[]
            {
                20.0,
                135.0,
                new Complex(-14.14213562, 14.14213562)
            },
            new object[]
            {
                7.0,
                240,
                new Complex(-3.5, -6.062177827)
            },
            new object[]
            {
                1.0,
                -60,
                new Complex(0.5, -0.866025403)
            }
        };

        public static IEnumerable<object[]> rmsData { get; } = new List<object[]>()
        {
            new object[]
            {
                new double[] {14.14213562, 311.1269837},
                new double[] {10.0, 220.0},
            }
        };

        public static IEnumerable<object[]> frequencyData { get; } = new List<object[]>()
        {
            new object[]
            {
                0.0,
                0.0
            },
            new object[]
            {
                -60.0,
                -0.016666666
            },
            new object[]
            {
                50.0,
                0.02
            }
        };

        public static IEnumerable<object[]> rotatingData { get; } = new List<object[]>()
        {
            new object[]
            {
                0.0,
                2.0,
                0.5,
                new Complex(0.0, 1.0),
                new Complex(0.0, -1.0)
            },
            new object[]
            {
                45.0,
                50.0,
                0.02,
                new Complex(-0.707106781, 0.707106781),
                new Complex(0.707106781, -0.707106781)
            },
            new object[]
            {
                90.0,
                0.0,
                1.0,
                new Complex(0.0, 1.0),
                new Complex(0.0, 1.0)
            },
            new object[]
            {
                30.0,
                -20.0,
                0.05,
                new Complex(0.5, -0.866025403),
                new Complex(-0.5, 0.866025403)
            }
        };

        [Theory]
        [MemberData(nameof(phasorData))]
        public void ValueAtInstantZero(
            double magnitude,
            double phase,
            Complex phasorValue)
        {
            var V = new Phasor(magnitude, TrigonometryExtensions.Degree2Radians(phase));

            Assert.Equal(phasorValue.Real, V.Value().Real, 8);
            Assert.Equal(phasorValue.Imaginary, V.Value().Imaginary, 8);
        }

        [Theory]
        [MemberData(nameof(rmsData))]
        public void PeakValueInstantiation(
            double[] peakValue,
            double[] rmsValue)
        {
            var V = new Phasor(rmsValue[0], 0);

            Assert.Equal(peakValue[0], V.PeakValue, 7);

            V.PeakValue = peakValue[1];

            Assert.Equal(rmsValue[1], V.RmsValue, 7);
        }

        [Theory]
        [MemberData(nameof(frequencyData))]
        public void PhasorYieldsPeriod(
            double frequency,
            double period)
        {
            var V = new Phasor();

            V.Frequency = frequency;

            if (V.Frequency == 0)
                Assert.Throws<DivideByZeroException>(() => V.Period);
            else
                Assert.Equal(period, V.Period, 7);
        }

        [Theory]
        [MemberData(nameof(rotatingData))]
        public void PhasorRotation(
            double phase,
            double frequency,
            double period,
            Complex phasorAtNinety,
            Complex phaserAtTwoHundredSeventy)
        {
            var V = new Phasor(1.0, TrigonometryExtensions.Degree2Radians(phase), frequency);

            var time = new List<double> { 0.25 * period, 0.75 * period };

            Assert.Equal(phasorAtNinety.Real, V.Value(time[0]).Real, 7);
            Assert.Equal(phasorAtNinety.Imaginary, V.Value(time[0]).Imaginary, 7);

            Assert.Equal(phaserAtTwoHundredSeventy.Real, V.Value(time[1]).Real, 7);
            Assert.Equal(phaserAtTwoHundredSeventy.Imaginary, V.Value(time[1]).Imaginary, 7);

            Assert.Equal(V.Value().Real, V.Value(period).Real, 10);
            Assert.Equal(V.Value().Imaginary, V.Value(period).Imaginary, 10);
        }

        [Fact]
        public void NegativePhasorPhaseIncrease()
        {
            var V = new Phasor(-1.0, 2.0);

            Assert.Equal(1.0, V.Magnitude);
            Assert.Equal(TrigonometryExtensions.Degree2Radians(180) + 2.0, V.Phase);
        }

        [Fact]
        public void CosinusoidalWave()
        {
            var V = new Phasor(10, TrigonometryExtensions.Degree2Radians(45), 10);
            var time = new List<double> { 0.33 * V.Period, 0.67 * V.Period };

            Assert.Equal(V.PeakValue * Math.Cos(2 * Math.PI * V.Frequency * time[0] + V.Phase), V.InstantValue(time[0]), 15);
            Assert.Equal(V.PeakValue * Math.Cos(2 * Math.PI * V.Frequency * time[1] + V.Phase), V.InstantValue(time[1]), 15);
        }
    }
}
