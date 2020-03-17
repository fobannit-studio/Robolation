using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
using Simulation.World;
using System;
using UnityEngine;
namespace Simulation.Components
{

    class Radio
    {
        // Method provides to Medium, where message 
        // is transmitted.
        private readonly Action<Frame> Gateaway;
        public int maxListenersNumber;
        public readonly int macAddress;
        protected float range;
        // Receiver, that will handle messages sent by this radio.
        public IReceiver controller;
        private List<int> macTable = new List<int>();

        public Radio(float range, int maxListenersNumber, ref Medium ether):this(range, maxListenersNumber, null,ref ether)
        {}
        public Radio(float range, int maxListenersNumber, IReceiver controller, ref Medium ether)
        {
            this.controller = controller;
            this.range = range;
            this.macAddress = ether.RegisterRadio(ReceiveFrame);
            this.maxListenersNumber = maxListenersNumber;
            this.Gateaway = ether.Transmit;
        }
        public void NotifySubscribers(Frame frame)
        {
            frame.srcMac = macAddress;
            frame.destinationRole = DestinationRole.NoMatter;
            foreach (int macAddress in macTable)
            {
                frame.destMac = macAddress;
                SendFrame(frame);   
            }
        }
        public bool AddListener(int macAddress)
        {
            if(macTable.Count < maxListenersNumber)
            {
                macTable.Add(macAddress);
                return true;
            }
            return false;
        }
        public void SendFrame(Frame frame)
        {
            frame.srcMac = macAddress;
            Gateaway(frame);
        }
        public void ReceiveFrame(Frame frame)
        {
            if(isAbleToReceive(frame))
            {
                Debug.Log($"{this.controller.GetType().Name}'s radio received frame");
                controller.HandleFrame(frame);
            }
        }
        protected bool isAbleToReceive(Frame frame)
        {
            if(!System.Object.ReferenceEquals(controller, null))
            {
                return true;
            }
            return false;
        }
    }
}