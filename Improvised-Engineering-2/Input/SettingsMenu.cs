using RichHudFramework;
using RichHudFramework.UI;
using RichHudFramework.UI.Client;
using System;
using static Improvised_Engineering_2.Config.ImprovisedEngineeringConfig;
using Improvised_Engineering_2.Config;

namespace Improvised_Engineering_2
{
    public sealed partial class ImprovisedEngineeringMain
    {
        private TextPage helpMain;
        private RebindPage bindsPage;

        private void InitSettingsMenu()
        {
            RichHudTerminal.Root.Enabled = true;

            helpMain = new TextPage()
            {
                Name = "Help",
                HeaderText = "Build Vision Help",
                SubHeaderText = "",
                Text = HelpText.GetHelpMessage(),
            };
            bindsPage = new RebindPage()
            {
                Name = "Binds",
                GroupContainer =
                {
                    { ImprovisedEngineeringBinds.MainGroup, BindsConfig.DefaultMain },
                }
            };

            RichHudTerminal.Root.AddRange(new IModRootMember[] 
            { 
                new ControlPage()
                {
                    Name = "Settings",
                    CategoryContainer =
                    {
                        GetPickUpSettings(),
                        GetTetherSettings(),
                        GetGuiSettings(),
                        GetHelpSettings(),
                    },
                },
                bindsPage,
                helpMain,
            });

            Debug.Message("Settings Menu Initialized");
        }

        private ControlCategory GetPickUpSettings()
        {
            Func<char, bool> NumFilterFunc = x => (x >= '0' && x <= '9') || x == '.';

            // X
            var floatHeadOffsetX = new TerminalTextField()
            {
                Name = "Float HeadOffset X",
                Value = Cfg.pickUp.headOffset.X.ToString(),
                CharFilterFunc = NumFilterFunc,
                CustomValueGetter = () => Cfg.pickUp.headOffset.X.ToString(),
                ControlChangedHandler = (sender, args) =>
                {
                    var textField = sender as TerminalTextField;

                    float.TryParse(textField.Value, out Cfg.pickUp.headOffset.X);
                    Cfg.Validate();
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "Local Offset from the Head for Raycase and Display of Line. X Axis"
                },
            };

            // Y
            var floatHeadOffsetY = new TerminalTextField()
            {
                Name = "Float HeadOffset Y",
                Value = Cfg.pickUp.headOffset.Y.ToString(),
                CharFilterFunc = NumFilterFunc,
                CustomValueGetter = () => Cfg.pickUp.headOffset.Y.ToString(),
                ControlChangedHandler = (sender, args) =>
                {
                    var textField = sender as TerminalTextField;

                    float.TryParse(textField.Value, out Cfg.pickUp.headOffset.Y);
                    Cfg.Validate();
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "Local Offset from the Head for Raycase and Display of Line. Y Axis"
                },
            };

            // Z
            var floatHeadOffsetZ = new TerminalTextField()
            {
                Name = "Float HeadOffset Z",
                Value = Cfg.pickUp.headOffset.Z.ToString(),
                CharFilterFunc = NumFilterFunc,
                CustomValueGetter = () => Cfg.pickUp.headOffset.Z.ToString(),
                ControlChangedHandler = (sender, args) =>
                {
                    var textField = sender as TerminalTextField;

                    float.TryParse(textField.Value, out Cfg.pickUp.headOffset.Z);
                    Cfg.Validate();
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "Local Offset from the Head for Raycase and Display of Line. Z Axis"
                },
            };

            var headOffsetTile = new ControlTile()
            {
                floatHeadOffsetX,
                floatHeadOffsetY,
                floatHeadOffsetZ
            };
            
            var maxLengthSlider = new TerminalSlider()
            {
                Name = "Max Length",
                Min = 2f,
                Max = 40f,
                ValueText = $"{Cfg.pickUp.maxLength.Round(1)}m",
                Value = (float)Cfg.pickUp.maxLength,
                CustomValueGetter = () => (float)Cfg.pickUp.maxLength,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    Cfg.pickUp.maxLength = slider.Value;
                    slider.ValueText = $"{slider.Value.Round(1)}m";
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "How long the Wire can be before you let go of the block"
                },
            };
            
            var maxForceSlider = new TerminalSlider()
            {
                Name = "Max Force",
                Min = 100f,
                Max = 100000f,
                ValueText = $"{Cfg.pickUp.maxForce.Round(1)}N",
                Value = (float)Cfg.pickUp.maxForce.Round(1),
                CustomValueGetter = () => (float)Cfg.pickUp.maxForce,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    Cfg.pickUp.maxForce = slider.Value;
                    slider.ValueText = $"{slider.Value.Round(1)}N";
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "How much force you can apply to the block"
                },
            };

            // Close if not in view
            var showStressColorBox = new TerminalCheckbox()
            {
                Name = "Show Stress Color",
                Value = Cfg.pickUp.showStressColor,
                CustomValueGetter = () => Cfg.pickUp.showStressColor,
                ControlChangedHandler = ((sender, args) => Cfg.pickUp.showStressColor = (sender as TerminalCheckbox).Value),
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "If the wire will be colored depending on the stress of the wire"
                },
            };

            return new ControlCategory()
            {
                HeaderText = "PickUp",
                SubheaderText = "Configure character pickup behaviour below",
                TileContainer =
                {
                    headOffsetTile,
                    new ControlTile() { maxLengthSlider, maxForceSlider },
                    new ControlTile() { showStressColorBox }
                },
            };
        }

        private ControlCategory GetTetherSettings()
        {
            var maxLengthSlider = new TerminalSlider()
            {
                Name = "Max Length",
                Min = 2f,
                Max = 40f,
                ValueText = $"{Cfg.tether.maxLength.Round(1)}m",
                Value = (float)Cfg.tether.maxLength,
                CustomValueGetter = () => (float)Cfg.tether.maxLength,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    Cfg.tether.maxLength = slider.Value;
                    slider.ValueText = $"{slider.Value.Round(1)}m";
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "How long the Wire can be before the 2 entities disconnect"
                },
            };

            var maxForceSlider = new TerminalSlider()
            {
                Name = "Max Force",
                Min = 100f,
                Max = 100000f,
                ValueText = $"{Cfg.pickUp.maxForce.Round(1)}N",
                Value = (float)Cfg.pickUp.maxForce.Round(1),
                CustomValueGetter = () => (float)Cfg.pickUp.maxForce,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    Cfg.pickUp.maxForce = slider.Value;
                    slider.ValueText = $"{slider.Value.Round(1)}N";
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "How much force the wire can apply to the entities"
                },
            };

            // Close if not in view
            var showStressColorBox = new TerminalCheckbox()
            {
                Name = "Show Stress Color",
                Value = Cfg.tether.showStressColor,
                CustomValueGetter = () => Cfg.tether.showStressColor,
                ControlChangedHandler = ((sender, args) => Cfg.tether.showStressColor = (sender as TerminalCheckbox).Value),
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "If the wire will be colored depending on the stress of the wire"
                },
            };

            var removeDurationMinuteSlider = new TerminalSlider()
            {
                Name = "Remove Duration [Minutes]",
                Min = 0f,
                Max = 60f,
                ValueText = $"{Cfg.tether.removeDurationMinute}min",
                Value = Cfg.tether.removeDurationMinute,
                CustomValueGetter = () => (float)Cfg.tether.removeDurationMinute,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    Cfg.tether.removeDurationMinute = (int)slider.Value;
                    slider.ValueText = $"{slider.Value}min";
                },
                ToolTip = new RichText(ToolTip.DefaultText)
                {
                    "How long tethers will exist in the world"
                },
            };

            return new ControlCategory()
            {
                HeaderText = "Tether",
                SubheaderText = "Configure tether behaviour below",
                TileContainer =
                {
                    new ControlTile() { maxLengthSlider, maxForceSlider, removeDurationMinuteSlider },
                    new ControlTile() { showStressColorBox }
                },
            };
        }

        private ControlCategory GetGuiSettings()
        {
            // Menu size
            var menuScale = new TerminalSlider()
            {
                Name = "Menu scale",
                Min = .75f,
                Max = 2f,
                Value = ImprovisedEngineeringConfig.Current.genUI.hudScale,
                ValueText = $"{(ImprovisedEngineeringConfig.Current.genUI.hudScale * 100f).Round()}%",
                CustomValueGetter = () => ImprovisedEngineeringConfig.Current.genUI.hudScale,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    ImprovisedEngineeringConfig.Current.genUI.hudScale = slider.Value;
                    slider.ValueText = $"{(slider.Value * 100f).Round()}%";
                }
            };

            // Menu opacity
            var opacity = new TerminalSlider()
            {
                Name = "Menu opacity",
                Min = .5f,
                Max = 1f,
                Value = ImprovisedEngineeringConfig.Current.genUI.hudOpacity,
                ValueText = $"{(ImprovisedEngineeringConfig.Current.genUI.hudOpacity * 100f).Round()}%",
                CustomValueGetter = () => ImprovisedEngineeringConfig.Current.genUI.hudOpacity,
                ControlChangedHandler = (sender, args) =>
                {
                    var slider = sender as TerminalSlider;

                    ImprovisedEngineeringConfig.Current.genUI.hudOpacity = slider.Value;
                    slider.ValueText = $"{(slider.Value * 100f).Round()}%";
                }
            };

            var tile1 = new ControlTile()
            {
                menuScale,
                opacity,
            };

            // Set custom position
            var setPosition = new TerminalDragBox()
            {
                Name = "Set custom position",
                AlignToEdge = true,
                CustomValueGetter = () => ImprovisedEngineeringConfig.Current.genUI.hudPos,
                ControlChangedHandler = ((sender, args) => ImprovisedEngineeringConfig.Current.genUI.hudPos = (sender as TerminalDragBox).Value),
                Value = ImprovisedEngineeringConfig.Current.genUI.hudPos,
            };

            var resetGuiButton = new TerminalButton()
            {
                Name = "Reset GUI settings",
                ControlChangedHandler = (sender, args) => ImprovisedEngineeringConfig.Current.genUI = UIConfig.Defaults,
            };

            var tile2 = new ControlTile()
            {
                setPosition,
                resetGuiButton
            };

            return new ControlCategory()
            {
                HeaderText = "GUI Settings",
                SubheaderText = "Customize appearance and menu positioning",
                TileContainer = { tile1, tile2 }
            };
        }
        
        private ControlCategory GetHelpSettings()
        {
            var openHelp = new TerminalButton()
            {
                Name = "Open help",
                ControlChangedHandler = (sender, args) => RichHudTerminal.OpenToPage(helpMain)
            };

            var tile1 = new ControlTile()
            {
                openHelp,
            };

            var loadCfg = new TerminalButton()
            {
                Name = "Load config",
                ControlChangedHandler = (sender, args) => ImprovisedEngineeringConfig.LoadStart(),
            };

            var saveCfg = new TerminalButton()
            {
                Name = "Save config",
                ControlChangedHandler = (sender, args) => ImprovisedEngineeringConfig.SaveStart()
            };

            var resetCfg = new TerminalButton()
            {
                Name = "Reset config",
                ControlChangedHandler = (sender, args) => ImprovisedEngineeringConfig.ResetConfig(),
            };

            var tile2 = new ControlTile()
            {
                loadCfg,
                saveCfg,
                resetCfg
            };

            return new ControlCategory()
            {
                HeaderText = "Help",
                SubheaderText = "Help text and config controls",
                TileContainer = { tile1, tile2 }
            };
        }
    }
}