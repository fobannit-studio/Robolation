using System.Collections.Generic;
using System;
using Simulation.Common;

namespace Simulation.World
{
    public class Medium
    {
        private static int radioId = 0; 
        private Dictionary<int, Action<Frame>> _registeredRadios = new Dictionary<int,Action<Frame>>();

        public void Transmit(int radio, Frame message)
        {
            this._registeredRadios[radio](message);
        }

        public void RegisterRadio(Action<Frame> radio)
        {   
            _registeredRadios.Add(Medium.radioId, radio);
            Medium.radioId += 1;
        }
        
    }
}