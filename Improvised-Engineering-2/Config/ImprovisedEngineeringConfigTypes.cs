using RichHudFramework.IO;
using RichHudFramework.UI;
using System.Xml.Serialization;
using VRage.Input;
using VRageMath;

namespace Improvised_Engineering_2.Config
{
    [XmlRoot, XmlType(TypeName = "ImprovisedEngineeringSettings")]
    public class ImprovisedEngineeringConfig : ConfigRoot<ImprovisedEngineeringConfig>
    {
        public static bool WasConfigOld { get; private set; }
        private const int vID = 1;

        [XmlElement(ElementName = "PickUpSettings")]
        public PickUpConfig pickUp;

        [XmlElement(ElementName = "TetherSettings")]
        public TetherConfig tether;

        [XmlElement(ElementName = "UISettings")]
        public UIConfig genUI;

        [XmlElement(ElementName = "InputSettings")]
        public BindsConfig binds;

        protected override ImprovisedEngineeringConfig GetDefaults()
        {
            return new ImprovisedEngineeringConfig
            {
                VersionID = vID,
                pickUp = PickUpConfig.Defaults,
                tether = TetherConfig.Defaults,
                genUI = UIConfig.Defaults,
                binds = BindsConfig.Defaults
            };
        }

        public override void Validate()
        {
            if (VersionID != vID)
            {
                VersionID = vID;
                pickUp = PickUpConfig.Defaults;
                tether = TetherConfig.Defaults;
                genUI = UIConfig.Defaults;
                binds = BindsConfig.Defaults;
                WasConfigOld = true;
            }
            else
            {
                if (pickUp != null)
                    pickUp.Validate();
                else
                    pickUp = PickUpConfig.Defaults;

                if (tether != null)
                    tether.Validate();
                else
                    tether = TetherConfig.Defaults;

                if (genUI != null)
                    genUI.Validate();
                else
                    genUI = UIConfig.Defaults;

                if (binds != null)
                    binds.Validate();
                else
                    binds = BindsConfig.Defaults;
            }
        }

        public class PickUpConfig : Config<PickUpConfig>
        {
            [XmlElement(ElementName = "HeadOffset")]
            public Vector3 headOffset;

            [XmlElement(ElementName = "MaxLength")]
            public double maxLength;

            [XmlElement(ElementName = "MaxForce")]
            public double maxForce;

            [XmlElement(ElementName = "showStressColor")]
            public bool showStressColor;

            protected override PickUpConfig GetDefaults()
            {
                return new PickUpConfig
                {
                    headOffset = new Vector3(0, 0, 0),
                    maxLength = 20,
                    maxForce = 20000,
                    showStressColor = true,
                };
            }

            public override void Validate()
            {
                if (maxLength <= 0d)
                    maxLength = Defaults.maxLength;
                else
                    maxLength = MathHelper.Clamp(maxLength, 2d, 40d);

                if (maxForce <= 0d)
                    maxForce = Defaults.maxForce;
                else
                    maxForce = MathHelper.Clamp(maxForce, 100, 100000);
            }
        }

        public class TetherConfig : Config<TetherConfig>
        {
            [XmlElement(ElementName = "MaxLength")]
            public double maxLength;

            [XmlElement(ElementName = "MaxForce")]
            public double maxForce;

            [XmlElement(ElementName = "showStressColor")]
            public bool showStressColor;

            [XmlElement(ElementName = "RemoveDurationMinute")]
            public int removeDurationMinute;

            protected override TetherConfig GetDefaults()
            {
                return new TetherConfig
                {
                    maxLength = 20,
                    maxForce = 20000,
                    showStressColor = true,
                    removeDurationMinute = 15,
                };
            }

            public override void Validate()
            {
                if (maxLength <= 0d)
                    maxLength = Defaults.maxLength;
                else
                    maxLength = MathHelper.Clamp(maxLength, 2d, 40d);

                if (maxForce <= 0d)
                    maxForce = Defaults.maxForce;
                else
                    maxForce = MathHelper.Clamp(maxForce, 100, 100000);

                if (removeDurationMinute < 0)
                    removeDurationMinute = Defaults.removeDurationMinute;
                else
                    removeDurationMinute = MathHelper.Clamp(removeDurationMinute, 0, 60);
            }
        }

        public class UIConfig : Config<UIConfig>
        {
            [XmlElement(ElementName = "HudScale")]
            public float hudScale;

            [XmlElement(ElementName = "HudOpacity")]
            public float hudOpacity;

            [XmlElement(ElementName = "HudPosition")]
            public Vector2 hudPos;

            protected override UIConfig GetDefaults()
            {
                return new UIConfig
                {
                    hudScale = 1f,
                    hudOpacity = 0.8f,
                    hudPos = new Vector2(-0.4823f, 0.4676f),
                };
            }

            /// <summary>
            /// Checks any if fields have invalid values and resets them to the default if necessary.
            /// </summary>
            public override void Validate()
            {

                if (hudScale == default(float))
                    hudScale = Defaults.hudScale;

                if (hudOpacity < 0f || hudOpacity > 1f)
                    hudOpacity = Defaults.hudOpacity;

                hudPos = Vector2.Clamp(hudPos, -.49f * Vector2.One, .49f * Vector2.One);
            }
        }

        /// <summary>
        /// Stores data for serializing the configuration of the Binds class.
        /// </summary>
        public class BindsConfig : Config<BindsConfig>
        {

            public static BindDefinition[] DefaultMain => defaultMain.Clone() as BindDefinition[];

            private static readonly BindDefinition[]
                defaultMain = new BindGroupInitializer
                {
                    { "pickUp", MyKeys.R },
                    { "letGo", MyKeys.R },
                    { "increaseDistance", RichHudControls.MousewheelUp },
                    { "decreaseDistance", RichHudControls.MousewheelDown },
                    { "toggleMode", MyKeys.M },

                }.GetBindDefinitions();

            [XmlArray("MainGroup")]
            public BindDefinition[] mainGroup;

            protected override BindsConfig GetDefaults()
            {
                return new BindsConfig
                {
                    mainGroup = DefaultMain,
                };
            }

            /// <summary>
            /// Checks any if fields have invalid values and resets them to the default if necessary.
            /// </summary>
            public override void Validate()
            {

                if (mainGroup == null)
                    mainGroup = DefaultMain;
            }
        }
    }

}
