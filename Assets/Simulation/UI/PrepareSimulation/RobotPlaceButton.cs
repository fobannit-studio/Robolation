using Simulation.Robots;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Simulation.UI
{
    public class RobotPlaceButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Text text;

        public (Type soft, Robot robot) assignedRobot;
        public RobotPlacer robotPlacer;

        void Clicked()
        {
            robotPlacer.RobotSelected(this);
        }
        public void Init((Type soft, Robot robot) assignedRobot, RobotPlacer placer)
        {
            button.onClick.AddListener(Clicked);
            this.assignedRobot = assignedRobot;
            robotPlacer = placer;
            text.text = assignedRobot.robot.GetType().Name;

        }
    }
}