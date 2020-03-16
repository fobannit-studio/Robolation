using System.Collections.Generic;
using System;
using Simulation.Common;
using Simulation.Utils;
using UnityEngine;

namespace Simulation.World
{
    public class Medium
    {
        private static int freeMac = 0;
        private Dictionary<int, Action<Frame>> macTable = new Dictionary<int, Action<Frame>>();

        public void Transmit(int mac, Frame message)
        {
            if (message.transmissionType is TransmissionType.Unicast)
            {
                this.macTable[mac](message);
            }
            else if (message.transmissionType is TransmissionType.Broadcast)
            {
                this.broadcast(message);
            }
        }

        public void broadcast(Frame message)
        {
            foreach (var radio in macTable)
            {
                radio.Value(message);
            }

        }
        // Assigns MAC-address to radio and register it in mac table
        public int RegisterRadio(Action<Frame> radio)
        {
            int macAddress = Medium.freeMac;
            Medium.freeMac += 1;
            macTable.Add(macAddress, radio);
            return macAddress;
        }

    }
}