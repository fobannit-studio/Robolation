namespace Simulation.Utils
{
    // Enum describes transmission types.
    // In case if frame is of type Broadcast - 
    // it will be transmitted to every registered radio
    // If frame is Unicast - it will be destroyed after with 
    // matching radio will receive it.
    public enum TransmissionType
    {
        Broadcast,
        Unicast
    }
    // Enum defines what kind of Role could receive this message 
    public enum DestinationRole
    {
        Operator,
        Builder,
        Transporter,
        Broadcast
    }
    // Service - message type that require some action from receiver
    // Request - message type that require some information from receiver
    public enum MessageType
    {
        Service,
        Request

    }
    // Messages, that could be received by robots
    public enum Message
    {
        BringMaterial,
        StopWork,
        Subscribe
    }

}