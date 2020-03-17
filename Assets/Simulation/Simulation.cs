using System;
using UnityEngine;
using Simulation.Robots;
using Simulation.World;
using Simulation.Common;
using Simulation.Utils;
namespace Simulation
{



    public class Simulation : MonoBehaviour
    {
        void Start()
        {
            // Steps:
            // 1. Create Medium an Robots
            // 2. Initialize all buildings (create materials there)
            // 3. Start operator lookup.
            Medium ether = new Medium();
            Vector2 center = new Vector2(0, 0);
            Vector2 veryFar = new Vector2(100, 100);
            Vector2 veryClose = new Vector2(5, 5);
            Frame message = new Frame(
          TransmissionType.Broadcast,
          DestinationRole.Transporter,
          MessageType.Service,
          Message.MoveTo,
          1,
          (1, 2)
      );

            Parrot r1 = new Parrot(center, 50, ref ether);
            IRB1100 r2 = new IRB1100(veryFar, 10, ref ether);
            Spot r3 = new Spot(veryClose, 10, ref ether);
            r1.NotifySubscribers(message);
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