namespace Simulation.Common
{
    public struct Frame
    {
        public int srcId, destId, actionId;
        public Frame(int srcId, int destId, int actionId)
        {
            this.srcId = srcId;
            this.destId = destId;
            this.actionId = actionId;
        }
        
        public override string ToString(){
            return $"From {this.srcId} to {this.destId}. Action {this.actionId}";
        }
    }
}