using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Pamola.Phasor
{
    public interface IPhasor
    {
        double Magnitude { get; set; }
        double Phase { get; set; }

        Complex Value();
            
        Complex Value(double time);
    }
}
