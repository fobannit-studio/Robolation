using System.Collections;
using UnityEngine;
using Simulation.Robots;

namespace Assets.Simulation.Robots.Transporters
{
    public class Walle : Robot
    {
        [SerializeField]
        private Transform LeftArm;
        [SerializeField]
        private Transform RightArm;
        [SerializeField]
        private Transform Plate;

        public override int BuildIterations => 8;

        protected override int cointainer_size => 40;


    }
}