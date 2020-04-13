using Simulation.Utils;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Simulation.Common;
using System;

namespace Simulation.Software
{
    class Movement : Application
    {

        private CommunicationBasedApplicationState movement;
        private CommunicationBasedApplicationState waiting;
        public override void Activate()
        {
            currentState = waiting;
        }
        public override void initStates()
        {
            movement = new Moving(this);
        }
        public void SetMovingState()
        {
            currentState = movement;
        }
        public void SetWaitingState()
        {
            currentState = waiting;
        }

        // private void SendACK(Frame frame)
        // {
        //     Frame response = new Frame(
        //        TransmissionType.Unicast,
        //        DestinationRole.Operator,
        //        MessageType.ACK,
        //        Message.MoveTo,
        //        destMac: frame.srcMac);

        //     Software.radio.SendFrame(response);
        // }
        // private void ReceiveMoveOrder(Frame frame)
        // {
        //     var coords = frame.payload.floatPayload;
        //     Debug.Log(Software.attributedRobot.transform.position);
        //     Software.attributedRobot.MoveOrder(new Vector3(coords[0], coords[1], coords[2]));
        //     SendACK(frame);
        // }
    }
}
