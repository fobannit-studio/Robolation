using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Simulation.Utils;
using Simulation.Common;
namespace Simulation.Components
{
    class Container
    {
        private ConcurrentDictionary<string, int> materialsInContainer = new ConcurrentDictionary<string, int>();
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
                if (value > 0)
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
        public void put(BuildingMaterial material, int amount)
        {
            FreeSpace -= material.Volume * amount;
            Weight += material.Weight * amount;
            if (!materialsInContainer.TryAdd(material.Type, amount))
            {
                materialsInContainer[material.Type] += amount;
            }
        }
        public int get(BuildingMaterial material, int requestedAmount)
        {
            int returnedAmount = 0;
            if (materialsInContainer.TryGetValue(material.Type, out int amountInContainer))
            {
                returnedAmount = amountInContainer > requestedAmount ? requestedAmount : amountInContainer;
                materialsInContainer[material.Type] -= returnedAmount;
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
            foreach (KeyValuePair<string, int> material in materialsInContainer)
            {
                sb.Append(material.ToString());
            }
            sb.Append($"\nFree space: {FreeSpace}\nWeight: {Weight}\n");
            return sb.ToString();
        }
    }

}