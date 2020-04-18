using Simulation.Utils;
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
        public override void initStates()
        { }
        /// <summary>
        /// Move transporter to position. If transporter is not given - move random transporter from 
        /// its subsribtion list 
        /// </summary>
        public void MoveToPosition(float x, float y, float z, Action controlReturn)
        {
            var pos = new[] { x, y, z };
            var movementTrackingThread = AttributedSoftware.GameObject.AddComponent<MovementTracker>();
            movementTrackingThreads.Add((x, y, z), movementTrackingThread);
            movementTrackingThread.installOn(AttributedSoftware);
            movementTrackingThread.Position = pos;
            movementTrackingThread.ReturnControl = controlReturn;
        }
        public override void ReceiveFrame(Frame frame)
        {
            if (frame.message is Message.MoveTo)
            {
                (float x, float y, float z) = frame.payload;
                movementTrackingThreads[(x, y, z)].ReceiveFrame(frame);
            }
        }
        public void FinishMoveOrderTracking(float x, float y, float z)
        {
            movementTrackingThreads.Remove((x, y, z));
        }
    }
}
