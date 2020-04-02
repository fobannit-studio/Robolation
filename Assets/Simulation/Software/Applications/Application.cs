using System.Collections.Generic;
using Simulation.Utils;
using Simulation.Common;
using UnityEngine;
using System;

namespace Simulation.Software
{
    abstract class Application: MonoBehaviour
    {
       
     
        protected Dictionary<Message, Action<Frame>> ActionsOnRecive;

        protected OperatingSystem software;

      
        // protected Dictionary<Message, FrameAction> actions;


        public virtual void ReceiveFrame(Frame frame)
        {
            if (ActionsOnRecive.ContainsKey(frame.message))
            {
                Action<Frame> action = ActionsOnRecive[frame.message];
                action(frame);
            }

         
        }
        public virtual void Activate() { }
        
        public void installOn(OperatingSystem system)
        {
            this.software = system;

            
        }
    }

}