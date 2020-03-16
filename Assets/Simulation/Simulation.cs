using System;
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using Simulation.Common;
namespace Simulation
{
    using UnityEngine;
    public class Simulation:MonoBehaviour
    {
        void Start()
            {
                Medium ether = new Medium();
                Robot r1 = new Robot("robot 1",ref ether);
                Robot r2 = new Robot("robot 2",ref ether);
                Frame message = new Frame(1,1,1); 
                ether.Transmit(1, message);
                ether.Transmit(0, message);
                Debug.Log("Done ");
            }
        }
}