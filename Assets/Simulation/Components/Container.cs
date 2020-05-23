using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Simulation.Utils;
using Simulation.Common;

using UnityEngine;
using System.Runtime.Remoting.Messaging;

namespace Simulation.Components
{
    public class Container :IContainer
    {
        private ConcurrentDictionary<BuildingMaterial, int> materialsInContainer = new ConcurrentDictionary<BuildingMaterial, int>();
        private float freeSpace;
        public float Weight; 
        public float FreeSpace
        {
            get
            {
                return freeSpace;
            }
            set
            {
                if (value >= 0)
                {
                    freeSpace = value;
                }
                else
                {
                    throw new ContainerIsFullException();
                }
            }
        }
        
        public Container(int freeSpace)
        {
            this.freeSpace = freeSpace;
        }
        // Throws exception if container is full
        public void Put(BuildingMaterial material, int amount)
        {
            if (CanPut(material,amount))
            {
                FreeSpace -= material.Volume * amount;
                Weight += material.Weight * amount;
                if (!materialsInContainer.TryAdd(material, amount))
                {
                    materialsInContainer[material] += amount;
                }
            }
            else
                throw new ContainerIsFullException();
            
            

        }

        public bool CanPut(BuildingMaterial material, int requestedAmount)
        {
            return FreeSpace >= material.Volume * requestedAmount;
        }

        public bool CanTake(BuildingMaterial material, int count)
        {
            if ( materialsInContainer.ContainsKey(material))
            {
                return materialsInContainer[material] >= count;
            }

            else
            
                return false;
            
        }
        public bool TransferTo(IContainer container,BuildingMaterial material,int amount)
        {
            if (!CanTake(material, amount))
                return false;
            if (container.CanPut(material,amount))
            {
                Take(material, amount);
                container.Put(material, amount);
                return true;
               
            }
            return false;
        }
        public int Take(BuildingMaterial material, int requestedAmount)
        {
            int returnedAmount = 0;
            if (materialsInContainer.TryGetValue(material, out int amountInContainer))
            {
                returnedAmount = Mathf.Min(amountInContainer, requestedAmount);
                materialsInContainer[material] -= returnedAmount;
                Weight -= material.Weight * returnedAmount;
                FreeSpace += material.Volume * returnedAmount;
            }
            else
            {
                throw new NoMaterialInContainerException();
            }
            return returnedAmount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<BuildingMaterial, int> material in materialsInContainer)
            {
                sb.Append(material.Key.Type);
            }
            sb.Append($"\nFree space: {FreeSpace}\nWeight: {Weight}\n");
            return sb.ToString();
        }

        public ConcurrentDictionary<BuildingMaterial, int> GetContent()
        {
            return materialsInContainer;
        }
        public int TryTransferTo(IContainer container, BuildingMaterial material, int amount)
        {
            int transfered = 0;
            for (int i = amount; i > 0; i--)
            {
                if (TransferTo(container, material, i))
                {
                    transfered = i;
                    break;
                }
            }
            return transfered;
        }
    }

}