using Simulation.Common;
using UnityEngine;
namespace Simulation.Utils
{
    public interface IReceiver
    {
        Vector2 Position
        {
            get;
        }
        void HandleFrame(Frame frame);
    }
    public interface IAction
    {
        void DoAction();
    }
    
}