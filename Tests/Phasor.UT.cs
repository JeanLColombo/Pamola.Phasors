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
    }
}
