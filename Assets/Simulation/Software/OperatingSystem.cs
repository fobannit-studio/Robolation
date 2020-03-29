using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using Simulation.Components;
using UnityEngine;
namespace Simulation.Software
{
    abstract class OperatingSystem : ICommunicator
    {
       

        public readonly Radio radio;
        public Robot attributedRobot;
        protected int operatorMac;
        
        public int OperatorMac{
            set =>  operatorMac=value;
            get => operatorMac;
            
        }
        public Vector3 Position
        {
            
            get
            {
                return attributedRobot.Position;
            }
        }
        public abstract List<Application> ReqiuredSoft
        {
            get;
        }
        public OperatingSystem(ref Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
            radio = attributedRobot.radio;
            operatorMac = -1;

            
        }
        protected void InstallSoft()
        {
            foreach (var application in ReqiuredSoft)
            {
                application.installOn(this);
            }
        }
        protected abstract DestinationRole IReceive
        {
            get;
        }
        protected bool isForMe(Frame message)
        {
            return message.destinationRole == IReceive || message.destinationRole is DestinationRole.NoMatter;
        }
        public void HandleFrame(Frame frame)
        {
            if (isForMe(frame))
            {
                Debug.Log($"{this.GetType().Name} recognized itself and start parsing message: {frame}");
                foreach (var application in ReqiuredSoft)
                {
                    application.ReceiveFrame(frame);
                }               
            }
        }
    }
}