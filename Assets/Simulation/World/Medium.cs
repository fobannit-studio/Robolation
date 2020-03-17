using System.Collections.Generic;
using System;
using Simulation.Common;
using Simulation.Utils;
using UnityEngine;

namespace Simulation.World
{
    // TODO create as singleton
    public class Medium
    {
        private static int freeMac = 0;
        private Dictionary<int, Action<Frame, Vector2>> macTable = new Dictionary<int, Action<Frame, Vector2>>();

        public void Transmit(Frame message, Vector2 senderPosition)
        {
            if (message.transmissionType is TransmissionType.Unicast)
            {
                macTable[message.destMac](message, senderPosition);
            }
            else if (message.transmissionType is TransmissionType.Broadcast)
            {
                Broadcast(message, senderPosition);
            }
        }

        private void Broadcast(Frame message, Vector2 senderPosition)
        {
            foreach (KeyValuePair<int, Action<Frame, Vector2>> receiver in macTable)
            {
                if (receiver.Key != message.destMac)
                {
                    receiver.Value(message, senderPosition);

                }
            }

        }

        // Assigns MAC-address to radio and register in in mac table
        // this radios call method.
        // Position is added because, while receiving each radio should check by itself,
        // (based on it technical characteristics) is this radio able to receive that frame or not
        public int RegisterRadio(Action<Frame, Vector2> radio)
        {
            int macAddress = Medium.freeMac;
            Medium.freeMac += 1;
            macTable.Add(macAddress, radio);
            return macAddress;
        }

    }
}