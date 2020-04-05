using System;
namespace Simulation.Utils 
{
    public class MacTableCapacityChangedEventArgs : EventArgs
    {
        public int NewCapacity { get; set; }
        public int CausingMac { get; set; }
        public bool IsSubscription { get; set; }
    }

}