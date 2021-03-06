using ICities;
using ColossalFramework.UI;
using CitiesHarmony.API;


namespace BUBO
{
    /// <summary>
    /// The base mod class for instantiation by the game.
    /// </summary>
    public class BUBOMod : IUserMod
    {
        public static string ModName => "Build Up Build OUt";
        public static string Version => "0.0.1";

        public string Name => ModName + " " + Version;
        public string Description => Translations.Translate("BUBO_DESC");


        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Apply Harmony patches via Cities Harmony.
            // Called here instead of OnCreated to allow the auto-downloader to do its work prior to launch.
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());

            // Load the settings file.
            ModSettings.Load();
        }


        /// <summary>
        /// Called by the game when the mod is disabled.
        /// </summary>
        public void OnDisabled()
        {
            // Unapply Harmony patches via Cities Harmony.
            if (HarmonyHelper.IsHarmonyInstalled)
            {
                Patcher.UnpatchAll();
            }
        }


        /// <summary>
        /// Called by the game when the mod options panel is setup.
        /// </summary>
        /* public void OnSettingsUI(UIHelperBase helper)
        {
            // Language options.
            UIHelperBase languageGroup = helper.AddGroup(Translations.Translate("TRN_CHOICE"));
            UIDropDown languageDropDown = (UIDropDown)languageGroup.AddDropdown(Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index, (value) => { Translations.Index = value; ModSettings.Save(); });
            languageDropDown.autoSize = false;
            languageDropDown.width = 270f;

            // Panel options.
            UIHelperBase panelGroup = helper.AddGroup(Translations.Translate("ABLC_OPT_PNL"));
            UICheckBox onRightCheck = (UICheckBox)panelGroup.AddCheckbox(Translations.Translate("ABLC_OPT_RT"), ModSettings.onRight, (value) => { ModSettings.onRight = value; ModSettings.Save(); } );
            UICheckBox showPanelCheck = (UICheckBox)panelGroup.AddCheckbox(Translations.Translate("ABLC_OPT_SHO"), ModSettings.showPanel, (value) => { ModSettings.showPanel = value; ModSettings.Save(); });

            // Gameplay options.
            UIHelperBase gameGroup = helper.AddGroup(Translations.Translate("ABLC_OPT_PLY"));
            UICheckBox abandonHistCheck = (UICheckBox)gameGroup.AddCheckbox(Translations.Translate("ABLC_OPT_HNA"), ModSettings.noAbandonHistorical, (value) => { ModSettings.noAbandonHistorical = value; ModSettings.Save(); });
            UICheckBox abandonAnyCheck = (UICheckBox)gameGroup.AddCheckbox(Translations.Translate("ABLC_OPT_ANA"), ModSettings.noAbandonAny, (value) => { ModSettings.noAbandonAny = value; if (value) abandonHistCheck.isChecked = true; ModSettings.Save(); });
            UICheckBox loadLevelCheck = (UICheckBox)gameGroup.AddCheckbox(Translations.Translate("ABLC_OPT_CLL"), ModSettings.loadLevelCheck, (value) => { ModSettings.loadLevelCheck = value; ModSettings.Save(); });
        } */
    }
}
