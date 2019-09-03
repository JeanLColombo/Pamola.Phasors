using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Pamola.Phasor
{
    interface IPhasor
    {
        Complex Value { get; }

        double RmsValue { get; }

        double Period { set; get; }
    }
}
