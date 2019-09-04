using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pamola.Phasor.UT
{
    public class TrigonometryExtensionsUT
    {

        public static IEnumerable<object[]> angleData { get; } = new List<object[]>()
        {
            new object[]
            {
                45,
                1.570796327 / 2
            },
            new object[]
            {
                90,
                1.570796327
            },
            new object[]
            {
                180,
                1.570796327 * 2
            }
        };

        [Theory]
        [MemberData(nameof(angleData))]
        public void ConvertintDegreesToRadians(
            double degree,
            double radian)
        {
            Assert.Equal(radian, TrigonometryExtensions.Degree2Radians(degree), 7);
        }

        [Theory]
        [MemberData(nameof(angleData))]
        public void ConvertintRadiansToDegrees(
            double degree,
            double radian)
        {
            Assert.Equal(degree, TrigonometryExtensions.Radians2Degree(radian), 7);
        }
    }
}
