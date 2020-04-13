﻿using Simulation.Utils;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System;
namespace Simulation.Software
{
    class MoveOrder : Application
    {
        // Contains record for every uncompleted move order
        private Dictionary<(float, float, float), MovementTracker> movementTrackingThreads
        = new Dictionary<(float, float, float), MovementTracker>();
        public override void Activate()
        { }
        public override void initStates()
        { }
        /// <summary>
        /// Move transporter to position. If transporter is not given - move random transporter from 
        /// its subsribtion list 
        /// </summary>
        public void MoveToPosition(float x, float y, float z, int? robotMac = null)
        {
            var pos = new[] { x, y, z };
            var movementTrackingThread = AttributedSoftware.GameObject.AddComponent<MovementTracker>();
            movementTrackingThreads.Add((x, y, z), movementTrackingThread);
            movementTrackingThread.installOn(AttributedSoftware);
            movementTrackingThread.Activate();
        }
        public override void ReceiveFrame(Frame frame)
        {
            (float x, float y, float z) = frame.payload;
            FinishMoveOrderTracking(x, y, z);
        }
        public void FinishMoveOrderTracking(float x, float y, float z)
        {
            movementTrackingThreads.Remove((x, y, z));
        }



        // public void SendOrder()
        // {
        //     Frame frame = new Frame(
        //         TransmissionType.Unicast,
        //         DestinationRole.Transporter,
        //         MessageType.Service,
        //         Message.MoveTo,
        //         payload: new Payload(floatPayload: xyz));
        //     software.radio.NotifySubscribers(frame);
        // }
        // private void ReceiveACK(Frame frame)
        // {
        //     Debug.Log(frame);
        // }

    }
}