using System;
namespace Simulation.Common{
 
    [Serializable()]
    public class ContainerIsFullException : System.Exception
    {
        public ContainerIsFullException() : base() { }
        public ContainerIsFullException(string message) : base(message) { }
        public ContainerIsFullException(string message, System.Exception inner) : base(message, inner) { }
    }
}