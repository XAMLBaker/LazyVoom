using OpenSilver.Simulator;
using System;

namespace Sample1.Simulator
{
    internal static class Startup
    {
        [STAThread]
        static int Main(string[] args)
        {
            return SimulatorLauncher.Start (typeof (App));
        }
    }
}
