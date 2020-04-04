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


    public SlotContainer(ConcurrentDictionary<BuildingMaterial, int> Max,bool full=false)
    {
        MaxMaterials = Max;
        currentMaterials = new ConcurrentDictionary<BuildingMaterial, int>();

        foreach (var item in MaxMaterials.Keys)
        {
            currentMaterials.TryAdd(item, 0);
        }
        if (full)
        {
            currentMaterials = Max;
        }


    }
    public void AddSlot(BuildingMaterial mat, int maxAmount)
    {
        this.MaxMaterials.TryAdd(mat, maxAmount);
        this.currentMaterials.TryAdd(mat, 0);

    }
    public SlotContainer()
    {
        MaxMaterials = new ConcurrentDictionary<BuildingMaterial, int>();
        currentMaterials = new ConcurrentDictionary<BuildingMaterial, int>();
    }
    public ConcurrentDictionary<BuildingMaterial, int> GetMax()
    {
        return MaxMaterials;
    }
    public ConcurrentDictionary<BuildingMaterial,int> GetContent()
    {
        return currentMaterials;
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
        
        if (currentMaterials.TryGetValue(material, out int incontainer))
        {
            int taken = Mathf.Min(incontainer, requestedAmount);
            currentMaterials[material] -= taken;
            return taken;
        }

        throw new NoMaterialInContainerException();

    }

    public void Put(BuildingMaterial material, int amount)
    {
        if (currentMaterials.TryGetValue(material, out int incontainer))
        {
            if (incontainer+amount>MaxMaterials[material])
            {
                throw new ContainerIsFullException();
            }
            currentMaterials[material] += amount;
          
        }
        else
        {
            throw new NoMaterialInContainerException();
        }
       
    }
}
