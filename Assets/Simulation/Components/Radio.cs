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
        public event EventHandler<MacTableCapacityChangedEventArgs> MacTableCapasityChanged;
        // Method serves as a Gateway to Medium.
        // Sends frame that shoud be transmitted and radio position.
        private readonly Action<Frame, Vector3, float> Gateway;
        public int maxListenersNumber;
        public readonly int macAddress;
        protected float range;
        public float Range { get => range; }
        // Receiver, that will handle messages sent by this radio.
        public ICommunicator software;
        private List<int> macTable = new List<int>();

        public Radio(float range, Medium ether, int maxListenersNumber = 1) : this(range, null, ether, maxListenersNumber)
        { }
        public Radio(float range, ICommunicator software,  Medium ether, int maxListenersNumber = 1)
        {
            this.software = software;
            this.range = range;
            this.macAddress = ether.RegisterRadio(ReceiveFrame);
            this.maxListenersNumber = maxListenersNumber;
            this.Gateway = ether.Transmit;
        }
        public int ListenerAmount
        {
            get => macTable.Count;
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
            if (macTable.Count < maxListenersNumber)
            {
                macTable.Add(macAddress);
                var args = new MacTableCapacityChangedEventArgs();
                args.CausingMac = macAddress;
                args.NewCapacity = macTable.Count;
                args.IsSubscription = true;
                OnMacTableCapacityChanged(args);
                return true;
            }
            return false;
        }
        public void SendFrame(Frame frame)
        {
            frame.srcMac = macAddress;
            frame.SendingOS = software.GetType();
            Debug.Log($"{this.software.GetType().Name}'s radio sent frame {frame}");
            Gateway(frame, software.Position, this.range);

        }
        public void ReceiveFrame(Frame frame, Vector3 senderPosition, float senderRange)
        {
            if (isAbleToReceive(frame, senderPosition, senderRange))
            {
                Debug.Log($"{this.software.GetType().Name}'s radio received frame ${frame}");
                software.HandleFrame(frame);
            }
        }
        protected bool isAbleToReceive(Frame frame, Vector3 senderPosition, float senderRange)
        {
            bool isControlerExists = !System.Object.ReferenceEquals(software, null);
            // If radio software doesn't exists, than later checks have no sense,
            // because they based on software characterisitcs
            if (!isControlerExists) return false;
            bool isSenderInRange = Vector3.Distance(software.Position, senderPosition) < senderRange;
            return isSenderInRange;
        }
        protected virtual void OnMacTableCapacityChanged(MacTableCapacityChangedEventArgs e)
        {
            if (MacTableCapasityChanged != null)
            {
                MacTableCapasityChanged(this, e);
            }
            else
            {
                Debug.Log("No registerd handlers");
            }
        }
    }
}