using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;

namespace Improvised_Engineering_2
{
    public class Class1 : MySessionComponentBase
    {
        public NetworkProtobuf Networking = new NetworkProtobuf(56161);

        public override void BeforeStart()
        {
            Networking.Register();
        }

        protected override void UnloadData()
        {
            Networking?.Unregister();
            Networking = null;
        }

        public override void UpdateAfterSimulation()
        {
            // No world loaded (might be unloaded already or still loading (opening/closing world)
            if (MyAPIGateway.Session == null)
                return;

            // This Instance acts as Server
            if (MyAPIGateway.Utilities.IsDedicated || MyAPIGateway.Session.IsServer)
                Server();
            
            // This Instance has a Player
            if (MyAPIGateway.Session.Player?.Character != null)
                Client();
        }

        private void Server()
        {
            Debug.Message("Server");
        }

        private void Client()
        {
            // Client not Controlling Player in World => (Spectating, Navigating a Menu, RemoteControlling a Grid, ...)
            if (MyAPIGateway.Session.IsCameraUserControlledSpectator ||
                MyAPIGateway.Session.ControlledObject as IMyCharacter == null ||
                MyAPIGateway.Gui.IsCursorVisible ||
                MyAPIGateway.Gui.ChatEntryVisible ||
                MyAPIGateway.Gui.GetCurrentScreen != MyTerminalPageEnum.None)

            {
                return;
            }

            Debug.Message("Client");
        }
    }
}
