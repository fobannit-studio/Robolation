using System.Collections.Generic;
using System;
using Simulation.Common;
using UnityEngine;

namespace Simulation.World
{
    public class Medium
    {
        private static int freeMac = 0; 
        private Dictionary<int, (Action<Frame> radio, Func<Vector2> gps)> _macTable = new Dictionary<int,(Action<Frame>, Func<Vector2>)>();

        public void Transmit(int mac, Frame message)
        {
            this._macTable[mac].radio(message);
        }

        // Assigns MAC-address to radio and register it in mac table
        public int RegisterRadio(Action<Frame> radio, Func<Vector2> gps)
        {   
            int macAddress = Medium.freeMac; 
            Medium.freeMac += 1;
            _macTable.Add(macAddress, (radio, gps));
            return macAddress;
        }
        
    }
}