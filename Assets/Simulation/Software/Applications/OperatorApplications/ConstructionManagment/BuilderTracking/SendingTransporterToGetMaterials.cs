using Simulation.Common;
using Simulation.Utils;
using System.Collections.Generic;
namespace Simulation.Software
{
    internal class SendingTransporterToGetMaterials : CommunicationBasedApplicationState
    {
        private Stack<BuildingMaterial> requestedMaterials = new Stack<BuildingMaterial>();
        private Frame sendingFrame = new Frame();
        public SendingTransporterToGetMaterials(Application application) : base(application)
        { }

        public void RequestMaterials(List<BuildingMaterial> materials)
        {
            foreach (var material in materials) requestedMaterials.Push(material);
        }
        public override void Send()
        {
            AttributedSoftware.Radio.SendFrame(sendingFrame);
        }
        /// <summary>
        /// Handle message from transporter that can indicate if he take material successfully 
        /// or if he take material unsuccesfully
        /// </summary>
        /// <param name="frame"></param>
        public override void Receive(Frame frame)
        {
            //sendingFrame = new Frame(
            //    TransmissionType.Unicast,
            //    DestinationRole.Transporter,
            //    MessageType.Service,
            //    Message.BringMaterials,
            //    payload: Application.AdministratedBuilderPosition);
            if (frame.message is Message.BringMaterials && frame.messageType is MessageType.ACK)
           {
                // requestedMaterials.Pop()
                //sendingFrame.payload = new Payload(BuilderPosition) 
            }
            else
           {
                // requestedMaterials.Peek()
                //sendingFrame.payload = new Payload(NextWarehousePosition)
            }
        }
    }
}