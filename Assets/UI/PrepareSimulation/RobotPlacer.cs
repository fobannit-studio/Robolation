﻿using Simulation.Robots;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        void OnPlaced(GameObject instantiated)
        {
            placed.Add((currentButton.assignedRobot.soft, instantiated.GetComponent<Robot>()));
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