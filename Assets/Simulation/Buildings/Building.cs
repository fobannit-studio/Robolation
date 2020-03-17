using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Components;
using Simulation.Common;
using System.Collections.Concurrent;
public class Building : MonoBehaviour
{
    private Container container;

    public int stage=0;
    public Mesh[] frames;

    private ConcurrentDictionary<BuildingMaterial, int> neededComponents;
   

    public void setComponents(ConcurrentDictionary<BuildingMaterial, int> mats)
    {
        neededComponents = mats;
    }
    public ConcurrentDictionary<BuildingMaterial, int> getComponents()
    {
        return neededComponents;
    }

    public void Build(int quantity,BuildingMaterial mat)
    {

        neededComponents[mat] += quantity;
    }

    public  void  Animate()
    {
        this.GetComponent<MeshFilter>().mesh = frames[stage];
    }







}
