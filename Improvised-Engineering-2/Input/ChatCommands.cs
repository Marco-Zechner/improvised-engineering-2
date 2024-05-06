using RichHudFramework.Internal;
using RichHudFramework.IO;
using RichHudFramework.UI;
using RichHudFramework.UI.Client;
using System;
using Improvised_Engineering_2.Config;
using static Improvised_Engineering_2.Config.ImprovisedEngineeringConfig;

namespace Improvised_Engineering_2
{
    public sealed partial class ImprovisedEngineeringMain
    {
        private void AddChatCommands()
        {
            CmdManager.GetOrCreateGroup("/bv2", new CmdGroupInitializer 
            {
                { "help", x => RichHudTerminal.OpenToPage(helpMain) },
                { "resetBinds", x => ImprovisedEngineeringBinds.Cfg = BindsConfig.Defaults },
                { "save", x => ImprovisedEngineeringConfig.SaveStart() },
                { "load", x => ImprovisedEngineeringConfig.LoadStart() },
                { "resetConfig", x => ImprovisedEngineeringConfig.ResetConfig() },

                // Debug/Testing
                { "reload", x => ExceptionHandler.ReloadClients() },
                { "crash", x => Crash() },
                { "printControlsToLog", x => LogIO.WriteToLogStart($"Control List:\n{HelpText.controlList}") },
            });
        }
        

        private static void Crash()
        {
            throw new Exception($"Crash chat command was called.");
        }
    }
}