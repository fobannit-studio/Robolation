using Simulation.Utils;
using System.Collections.Generic;
using Simulation.Common;
using System;
using UnityEngine;
namespace Simulation.Software
{
    class MoveOrder : Application
    {
        // Contains record for every uncompleted move order
        private Dictionary<(float, float, float, int), MovementTracker> movementTrackingThreads
        = new Dictionary<(float, float, float, int), MovementTracker>();
        //private Queue<(float x, float y, float z, int mac, Action<Frame> action)> pendingOrders = new Queue<(float x, float y, float z, int mac, Action<Frame> action)>();
        public override void initStates()
        {
            //UseScheduler = true;
        }
        /// <summary>
        /// Move transporter to position. If transporter is not given - move random transporter from 
        /// its subsribtion list 
        /// </summary>
        //protected override void DoAction()
        //{
        //    if( pendingOrders.Count > 0) 
        //    {
        //        var order = pendingOrders.Dequeue();
        //        MoveToPosition(order.x, order.y, order.z, order.action, order.mac);
        //    }
        //}
        public void MoveToPosition(float x, float y, float z, Action<Frame> controlReturn, int targetsMac)
        {
            if (movementTrackingThreads.ContainsKey((x, y, z, targetsMac))) return;
            //pendingOrders.Enqueue((x, y, z, targetsMac, controlReturn));
            else StartThread(x, y, z, controlReturn, targetsMac);

        }
        private void StartThread(float x, float y, float z, Action<Frame> controlReturn, int targetsMac) 
        {
            var pos = new[] { x, y, z };
            var movementTrackingThread = AttributedSoftware.GameObject.AddComponent<MovementTracker>();
            movementTrackingThreads.Add((x, y, z, targetsMac), movementTrackingThread);
            movementTrackingThread.installOn(AttributedSoftware);
            movementTrackingThread.TargetsMac = targetsMac;
            movementTrackingThread.Position = pos;
            movementTrackingThread.ReturnControl = controlReturn;
        }
        public void MoveToPosition(Vector3 position, Action<Frame> controlReturn, int targetsMac) =>
            MoveToPosition(position.x, position.y, position.z, controlReturn, targetsMac);
  
        public override void ReceiveFrame(Frame frame)
        {
            if (frame.message is Message.MoveTo)
            {
                (float x, float y, float z) = frame.payload;
                movementTrackingThreads[(x, y, z, frame.srcMac)].ReceiveFrame(frame);
            }
        }
        public void FinishMoveOrderTracking(float x, float y, float z, int mac)
        {
            movementTrackingThreads.Remove((x, y, z, mac));
        }
    }
}
