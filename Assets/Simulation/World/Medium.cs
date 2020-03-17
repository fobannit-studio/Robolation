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

        public void Transmit(Frame message)
        {
            if (message.transmissionType is TransmissionType.Unicast)
            {
                macTable[message.destMac](message);
            }
            else if (message.transmissionType is TransmissionType.Broadcast)
            {
                Broadcast(message);
            }
        }

        private void Broadcast(Frame message)
        {
            foreach (KeyValuePair<int, Action<Frame>> receiver in macTable)
            {
                if (receiver.Key != message.destMac)
                {
                    receiver.Value(message);

                }
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