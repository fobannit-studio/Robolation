using System.Collections.Generic;
using Simulation.Common;
using Simulation.Utils;
using Simulation.Robots;
using Simulation.Components;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Reflection;
using System;
namespace Simulation.Software
{
    public abstract class OperatingSystem : ICommunicator
    {
        public Radio radio;
        public Robot attributedRobot;
        protected int operatorMac;
        public int OperatorMac
        {
            set => operatorMac = value;
            get => operatorMac;
        }
        public GameObject GameObject {get => attributedRobot.gameObject;}
        protected abstract DestinationRole IReceive { get; }
        public Vector3 Position => attributedRobot.transform.position;
        public ReadOnlyCollection<Application> ReqiuredSoft => requiredSoft.AsReadOnly();
        protected List<Application> requiredSoft;
        protected abstract void LoadSoft();
        public void Init(Robot robot)
        {
            attributedRobot = robot;
            attributedRobot.radio.software = this;
            radio = attributedRobot.radio;
            operatorMac = -1;
            LoadSoft();
            InstallSoft();
        }
        protected void InstallSoft()
        {
            foreach (var application in ReqiuredSoft)
            {
                application.installOn(this);
                application.Activate();
            }
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
