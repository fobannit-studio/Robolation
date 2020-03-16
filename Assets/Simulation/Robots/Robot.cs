using System;
using UnityEngine;
using Simulation.Common;
using Simulation.World;
using Simulation.Roles;
namespace Simulation.Robots
{
    class Robot
    {
        private string _id;
        public Robot(string id, ref Medium ether)
        {
            this._id = id;
            ether.RegisterRadio(this.Receive);
        }
        public void Receive(Frame message)
        {
            Debug.Log($"{_id} received message {message}");
        }
    }
}