using Simulation.Robots;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Simulation.World;
namespace Simulation.UI
{
    public class RobotPlacer : MonoBehaviour
    {
        Dictionary<Type, List<(Robot robot, int amount)>> Robots;
        [SerializeField]
        private GameObject PlaceButtonExample;
        private ScrollViewManager<RobotPlaceButton> remains;
        
        private List<(Type, Robot)> placed;
        private Placer placer;
        private RobotPlaceButton currentButton;

        [SerializeField]
        private Button completed;


        public void Init(Dictionary<Type, List<(Robot robot, int amount)>> dictionary)
        {
            Robots = dictionary;
            remains = new ScrollViewManager<RobotPlaceButton>();
            placer = gameObject.AddComponent<Placer>();
            placer.Init(OnPlaced);
            placed = new List<(Type, Robot)>();

            foreach (var role in dictionary.Keys)
            {
                foreach (var tuple in dictionary[role])
                {
                    for (int i = 0; i < tuple.amount; i++)
                    {
                        var button = remains.GenerateList(PlaceButtonExample);
                        button.Init((role, tuple.robot), this);
                    }
                }
            }

           


        }
        public void PlaceAll()
        {
            var warehouse=FindObjectOfType<Warehouse>();
            int i = 1;
            var offset = new Vector3(0, 0, 1f);
            foreach (var item in remains.elements)
            {
                var robot = Instantiate(item.assignedRobot.robot.gameObject);

                robot.GetComponent<NavMeshAgent>().Warp(warehouse.transform.position+i*offset);

                placed.Add((item.assignedRobot.soft, robot.GetComponent<Robot>()));
                i++;

            }
        }

        void OnPlaced(GameObject instantiated)
        {
            var robot = instantiated.GetComponent<Robot>();
            placed.Add((currentButton.assignedRobot.soft, robot));
            robot.GetComponent<NavMeshAgent>().Warp(robot.Position);
            remains.Delete(currentButton);
            if (remains.elements.Count==0)
            {
                completed.interactable = true;
            }
            // TODO 
        }
        public List<(Type soft, Robot robot)> GetResult()
        {
            return placed;
        }
        public void RobotSelected(RobotPlaceButton button)
        {
            currentButton = button;
            var robot = Instantiate(button.assignedRobot.robot.gameObject);
            placer.ChangeObject(robot);
        }

    }
}