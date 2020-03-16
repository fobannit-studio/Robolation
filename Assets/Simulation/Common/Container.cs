using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Simulation.Common
{
    class Container
    {
        private ConcurrentDictionary<string, int> materialsInContainer = new ConcurrentDictionary<string, int>();
        private int freeSpace;
        public int FreeSpace
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
            FreeSpace -= material.Size * amount;
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
                FreeSpace += material.Size * returnedAmount;
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
                sb.AppendFormat(material.ToString());
            }
            return sb.ToString();
        }
    }

}