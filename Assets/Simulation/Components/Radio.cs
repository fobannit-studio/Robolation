using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
using Simulation.World;
using System;
using UnityEngine;
namespace Simulation.Components
{

    public class Radio
    {
        // Method serves as a Gateway to Medium.
        // Sends frame that shoud be transmitted and radio position.
        private readonly Action<Frame, Vector3, float> Gateway;
        public int maxListenersNumber;
        public readonly int macAddress;
        protected float range;
        // Receiver, that will handle messages sent by this radio.
        public IReceiver software;
        private List<int> macTable = new List<int>();

        public Radio(float range, int maxListenersNumber, ref Medium ether):this(range, maxListenersNumber, null,ref ether)
        {}
        public Radio(float range, int maxListenersNumber, IReceiver software, ref Medium ether)
        {
            this.software = software;
            this.range = range;
            this.macAddress = ether.RegisterRadio(ReceiveFrame);
            this.maxListenersNumber = maxListenersNumber;
            this.Gateway = ether.Transmit;
        }
        // Send's frame as unicast message to each subscriber of type given in frame
        public void NotifySubscribers(Frame frame)
        {
            frame.srcMac = macAddress;
            frame.transmissionType = TransmissionType.Unicast;
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
            Gateway(frame, software.Position, this.range);
        }
        public void ReceiveFrame(Frame frame, Vector3 senderPosition, float senderRange)
        {
            if(isAbleToReceive(frame, senderPosition, senderRange))
            {
                Debug.Log($"{this.software.GetType().Name}'s radio received frame");
                software.HandleFrame(frame);
            }
        }
        protected bool isAbleToReceive(Frame frame, Vector3 senderPosition, float senderRange)
        {
            bool isControlerExists = !System.Object.ReferenceEquals(software, null);
            // If radio software doesn't exists, than later checks have no sense,
            // because they based on software characterisitcs
            if(!isControlerExists) return false;
            bool isSenderInRange = Vector3.Distance(software.Position, senderPosition) < senderRange; 
            return isSenderInRange;
        }
    }
}