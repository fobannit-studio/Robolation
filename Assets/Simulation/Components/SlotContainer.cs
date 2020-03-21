using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulation.Common;
using System.Collections.Concurrent;
using Simulation.Utils;
public class SlotContainer : IContainer
{

    private ConcurrentDictionary<BuildingMaterial, int> currentMaterials;
    private ConcurrentDictionary<BuildingMaterial, int> MaxMaterials;


    public SlotContainer(ConcurrentDictionary<BuildingMaterial, int> Max)
    {
        MaxMaterials = Max;
        currentMaterials = new ConcurrentDictionary<BuildingMaterial, int>();

        foreach (var item in MaxMaterials.Keys)
        {
            currentMaterials.TryAdd(item, 0);
        }


    }

    public ConcurrentDictionary<BuildingMaterial, int> FreeSpace()
    {
        var result = new ConcurrentDictionary<BuildingMaterial, int>();
        foreach (var item in currentMaterials.Keys)
        {
            result.TryAdd(item,MaxMaterials[item]-currentMaterials[item]);
        }

        return result;
    }

    public int Take(BuildingMaterial material, int requestedAmount)
    {
        throw new System.NotImplementedException();

    }

    public void Put(BuildingMaterial material, int amount)
    {
        
    }
}
