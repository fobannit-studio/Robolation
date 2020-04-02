
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using System.Collections.Generic;
using Simulation.Software;
using System;

namespace Simulation
{


    public class Simulation : MonoBehaviour
    {

        public  List<Robot> robots;
        public List<Building> buildings;

        public void Init(List<(Type soft, Robot robot)> robots, List<Building> buildings, List<Warehouse> warehouses)
        {

            // Steps:
            // 1. Create Medium an Robots
            // 2. Initialize all buildings (create materials there)
            // 3. Start operator lookup.



            Medium ether = new Medium();

           

            foreach (var item in robots)
            {
                item.robot.Init(1000, ref ether);
                var soft = Activator.CreateInstance(item.soft)as Software.OperatingSystem;
                soft.Init(item.robot);
                this.robots.Add(item.robot);
                Debug.Log(string.Format("Installed {0} on {1}", item.soft.Name,item.robot.GetType().Name));
            }

            

            //foreach (var robot in robots)
            //{
            //    robot.Init(1000, ref ether);
            //}


            //var a = new OperatorSoftware(robots[1]);
            //var b = new TransporterSoftware(robots[0]);






            //Vector2 center = new Vector2(0, 0);
            //Vector2 veryFar = new Vector2(100, 100);
            //Vector2 veryClose = new Vector2(5, 5);
            //Parrot r1 = new Parrot(center, 50, ref ether);
            //IRB1100 r2 = new IRB1100(veryFar, 10, ref ether);
            //Spot r3 = new Spot(veryClose, 10, ref ether);

            //// Should be so
            //Operator oper = new Operator(r1);
            //Builder builder = new Builder(r2);
            //Transporter transporter = new Transporter(r3);
            //oper.SendAllTransportToPosition(10.0F, 10.0F, 10.0F);
            // 
            // r1.NotifySubscribers(message);
            // r1.role.SendAllTransportToPosition((10.0,10.0));

            // Frame message = new Frame(
            //     TransmissionType.Unicast,
            //     DestinationRole.NoMatter,
            //     MessageType.Request,
            //     Message.StopWork,
            //     1, 1, 1);
            // ether.Transmit(message);
            // ether.Transmit(message);
            Debug.Log("Done ");

            // Create test later
            // BuildingMaterial steel = new BuildingMaterial("steel", (1, 1, 1), 100);
            // var gold = new BuildingMaterial("gold", (1, 1, 1), 200);

            // var cont = new Container(100000);
            // cont.put(steel, 10);
            // cont.put(gold, 15);
            // Debug.Log(cont);
            // cont.get(steel, 12);
            // Debug.Log(cont);
        }


    }
}