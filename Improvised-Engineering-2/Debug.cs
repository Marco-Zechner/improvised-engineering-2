using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Utils;

namespace Improvised_Engineering_2
{
    static class Debug
    {
        public static void Log(string sender = "Improvised Engineering 2", object message = null, bool informUser = false)
        {
            //if (!Config.improvisedConfig.useDebug) return;


            if (informUser)
                Message(sender, message);

            MyLog.Default.WriteLineAndConsole($"{sender}: {message}");
        }

        public static void Message(object message)
        {
            //if (!Config.improvisedConfig.useDebug) return;

            if (MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowMessage("Pc", $"{message}");
        }

        public static void Message(string sender, object message)
        {
            //if (!Config.improvisedConfig.useDebug) return;

            if (MyAPIGateway.Session?.Player != null)
                MyAPIGateway.Utilities.ShowMessage(sender, $"{message}");
        }

        public static void Notify(object message, int ms = 2000, string color = "Blue", bool ping = false)
        {
            //if (!Config.improvisedConfig.useDebug) return;

            if (MyAPIGateway.Session?.Player != null)
            {
                MyAPIGateway.Utilities.ShowNotification(message.ToString(), ms, color);
                if (ping)
                    MyVisualScriptLogicProvider.PlayHudSoundLocal();
            }
        }

    }
}
