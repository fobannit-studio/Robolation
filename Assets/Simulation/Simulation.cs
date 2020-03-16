using System;
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using Simulation.Common;
namespace Simulation
{
    


    public class Simulation : MonoBehaviour
    {
        void Start()
        {
            Medium ether = new Medium();
            Robot r1 = new Robot("robot 1", ref ether);
            Robot r2 = new Robot("robot 2", ref ether);

            BuildingMaterial steel = new BuildingMaterial("steel", (1,1,1), 100);
            var gold = new BuildingMaterial("gold", (1,1,1), 200);

            var cont = new Container(100000);
            cont.put(steel, 10);
            cont.put(gold, 15);
            Debug.Log(cont);
            cont.get(steel, 12);
            Debug.Log(cont);

            Frame message = new Frame(1, 1, 1);
            ether.Transmit(1, message);
            ether.Transmit(0, message);
            Debug.Log("Done ");
        }
    }
}