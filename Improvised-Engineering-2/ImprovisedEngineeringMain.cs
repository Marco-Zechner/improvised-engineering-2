using Improvised_Engineering_2.Config;
using RichHudFramework.Client;
using RichHudFramework.Internal;
using RichHudFramework.IO;
using RichHudFramework.UI.Client;
using System;
using VRage.Game.Components;

namespace Improvised_Engineering_2
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation | MyUpdateOrder.AfterSimulation, -1)]
    public sealed partial class ImprovisedEngineeringMain : ModBase
    {
        public static string MOD_NAME = "Improvised Engineering 2";
        
        public NetworkProtobuf Networking = new NetworkProtobuf(56161);

        public static ImprovisedEngineeringMain Instance { get; private set; }
        public static ImprovisedEngineeringConfig Cfg => ImprovisedEngineeringConfig.Current;

        public ImprovisedEngineeringMain() : base(true, true)
        {
            if (Instance == null)
                Instance = this;
            else
                throw new Exception("Only one instance of ImprovisedEngineeringMain can exist at any given time.");

            LogIO.FileName = "ImprovisedEngineeringLog.txt";
            ImprovisedEngineeringConfig.FileName = "ImprovisedEngineering2Config.xml";

            ExceptionHandler.ModName = MOD_NAME;
            ExceptionHandler.PromptForReload = true;
            ExceptionHandler.RecoveryLimit = 3;
        }



        public override void BeforeStart()
        {
            Networking.Register();

            Debug.Log(ImprovisedEngineeringMain.MOD_NAME, message: "BeforeStart", informUser: true);
        }

        protected override void UnloadData()
        {
            Networking?.Unregister();
            Networking = null;
        }

        protected override void AfterInit()
        {
            if (ExceptionHandler.IsClient)
            {
                CanUpdate = false;
                ImprovisedEngineeringMain.MOD_NAME = "AfterINIT";
                RichHudClient.Init(ExceptionHandler.ModName, HudInit, OnHudReset);
            }
        }

        private void HudInit()
        {
            ImprovisedEngineeringMain.MOD_NAME = "HudInit";

            if (ExceptionHandler.IsClient)
            {
                CanUpdate = true;

                ImprovisedEngineeringBinds.Init();
                ImprovisedEngineeringConfig.Load();
                AddChatCommands();
                InitSettingsMenu();

                if (ImprovisedEngineeringConfig.WasConfigOld)
                    RichHudTerminal.OpenToPage(helpMain);
                bindsPage.Enabled = true;
            }
        }


        protected override void Update()
        {
            Debug.Log(message: "Update", informUser: true);
        }

        private void OnHudReset() {
            ImprovisedEngineeringMain.MOD_NAME = "OnHudReset";
        }

        public override void BeforeClose()
        {
            if (ExceptionHandler.IsClient)
            {
                ImprovisedEngineeringConfig.Save();
                //QuickActionHudSpace.Close();
            }

            if (ExceptionHandler.Unloading)
            {
                //TerminalUtilities.Close();
                Instance = null;
            }
        }

        public abstract class ImprovisedEngineeringBase : ModuleBase
        {
            public ImprovisedEngineeringBase(bool runOnServer, bool runOnClient) : base(runOnServer, runOnClient, ImprovisedEngineeringMain.Instance)
            { }
        }

        //public override void UpdateAfterSimulation()
        //{
        //    // No world loaded (might be unloaded already or still loading (opening/closing world)
        //    if (MyAPIGateway.Session == null)
        //        return;

        //    // This Instance acts as Server
        //    if (MyAPIGateway.Utilities.IsDedicated || MyAPIGateway.Session.IsServer)
        //        ServerAfterUpdate();
            
        //    // This Instance has a Player
        //    if (MyAPIGateway.Session.Player?.Character != null)
        //        ClientAfterUpdate();
        //}

        //private void ServerAfterUpdate()
        //{
        //    //MyAPIGateway.Utilities.ShowMessage("Pc", $"Server");
        //}

        //private void ClientAfterUpdate()
        //{
        //    // Client not Controlling Player in World => (Spectating, Navigating a Menu, RemoteControlling a Grid, ...)
        //    if (MyAPIGateway.Session.IsCameraUserControlledSpectator ||
        //        MyAPIGateway.Session.ControlledObject as IMyCharacter == null ||
        //        MyAPIGateway.Gui.IsCursorVisible ||
        //        MyAPIGateway.Gui.ChatEntryVisible ||
        //        MyAPIGateway.Gui.GetCurrentScreen != MyTerminalPageEnum.None)

        //    {
        //        return;
        //    }
            
        //    //MyAPIGateway.Utilities.ShowMessage("Pc", $"Client");
        //}
    }
}
