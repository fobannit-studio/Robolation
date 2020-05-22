using Simulation.Common;
using UnityEngine;
namespace Simulation.Utils
{
    public interface IAction
    {
        void DoAction();
    }
    public interface ICommunicator
    {
        Vector3 Position
        {
            get;
        }
        void HandleFrame(Frame frame);
    }

    public interface IContainer
    {
        /// <summary>
        /// Put a BuildingMaterial in this container
        /// </summary>
        /// <param name="material"> What will be stored in container</param>
        /// <param name="amount">Amount to put in container</param>
        /// <exception cref="ContainerIsFullException">Thrown when container is full</exception>
        void Put(BuildingMaterial material, int amount);
        /// <summary>
        /// Get some amount of material from container
        /// </summary>
        /// <param name="material"> Type of material you want to get</param>
        /// <param name="requestedAmount">How much will be taken from container</param>
        /// <exception cref="NoMaterialInContainerException">Thrown when container does not contains this material</exception>
        int Take(BuildingMaterial material, int requestedAmount);


        bool CanTake(BuildingMaterial material, int requestedAmount);
        bool CanPut(BuildingMaterial material, int requestedAmount);

        bool TransferTo(IContainer container, BuildingMaterial material, int amount);
   




    }

}