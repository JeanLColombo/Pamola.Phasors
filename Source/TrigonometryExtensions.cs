using System;
using System.Collections.Generic;
using System.Text;

namespace Pamola.Phasor
{
    public static class TrigonometryExtensions
    {
        public static double Degree2Radians(double angleDegrees) => angleDegrees * Math.PI / 180.0;
        public static double Radians2Degree(double angleRandians) => angleRandians * 180.0 / Math.PI;

        //TODO: Test Trigonometry Extensions
    }
}
