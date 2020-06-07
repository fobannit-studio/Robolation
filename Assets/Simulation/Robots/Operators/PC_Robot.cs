using Simulation.Robots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Robot : Robot
{
    public override int BuildIterations => int.MaxValue;

    protected override int cointainer_size => int.MaxValue;

 
}
