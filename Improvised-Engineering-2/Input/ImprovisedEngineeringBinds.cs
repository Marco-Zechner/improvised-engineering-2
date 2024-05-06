using Improvised_Engineering_2.Config;
using RichHudFramework.UI;
using RichHudFramework.UI.Client;
using static Improvised_Engineering_2.Config.ImprovisedEngineeringConfig;
using static Improvised_Engineering_2.ImprovisedEngineeringMain;

namespace Improvised_Engineering_2
{
    public class ImprovisedEngineeringBinds : ImprovisedEngineeringBase
    {
        public static BindsConfig Cfg
        {
            get
            {
                return new BindsConfig
                {
                    mainGroup = MainGroup.GetBindDefinitions(),
                };
            }
            set
            {
                Instance.mainGroup.TryLoadBindData(value.mainGroup);
                Instance.UpdateBindProperties();
            }
        }

        public static IBind PickUp { get; private set; }
        public static IBind LetGo { get; private set; }
        public static IBind IncreaseDistance { get; private set; }
        public static IBind DecreaseDistance { get; private set; }
        public static IBind ToggleMode { get; private set; }
        
        public static IBindGroup MainGroup { get { return Instance.mainGroup; } }

        private static ImprovisedEngineeringBinds Instance
        {
            get
            {
                if (_instance == null)
                    Init();

                return _instance;
            }
            set { _instance = value; }
        }
        private static ImprovisedEngineeringBinds _instance;
        private readonly IBindGroup mainGroup;

        private ImprovisedEngineeringBinds() : base(false, true)
        {
            mainGroup = BindManager.GetOrCreateGroup("Main");
            mainGroup.RegisterBinds(BindsConfig.DefaultMain);
        }

        public static void Init()
        {
            if (_instance == null)
            {
                _instance = new ImprovisedEngineeringBinds();
                Cfg = ImprovisedEngineeringConfig.Current.binds;

                ImprovisedEngineeringConfig.OnConfigSave += _instance.UpdateConfig;
                ImprovisedEngineeringConfig.OnConfigLoad += _instance.UpdateBinds;

                _instance.UpdateBindProperties();
            }
        }

        public override void Close()
        {
            ImprovisedEngineeringConfig.OnConfigSave -= UpdateConfig;
            ImprovisedEngineeringConfig.OnConfigLoad -= UpdateBinds;
            Instance = null;

            PickUp = null;
            LetGo = null;
            IncreaseDistance = null;
            DecreaseDistance = null;
            ToggleMode = null;
        }

        private void UpdateBindProperties()
        {
            PickUp = mainGroup["pickUp"];
            LetGo = mainGroup["letGo"];
            IncreaseDistance = mainGroup["increaseDistance"];
            DecreaseDistance = mainGroup["decreaseDistance"];
            ToggleMode = mainGroup["toggleMode"];
        }

        private void UpdateConfig()
        {
            ImprovisedEngineeringConfig.Current.binds = Cfg;
        }

        private void UpdateBinds()
        {
            Cfg = ImprovisedEngineeringConfig.Current.binds;
        }
    }
}
