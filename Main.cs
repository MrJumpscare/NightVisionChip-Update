using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using Nautilus.Handlers;
using UnityEngine;
using System.IO;
using Nautilus.Options.Attributes;
using Nautilus.Json;

namespace Night_Vision_Update
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class NightVisionChipMain : BaseUnityPlugin
    {
        public static bool nightVisionEnabled = false;
        public void LateUpdate()
        {
            if (Input.GetKeyDown(NightVisionChipMain.config.nightvision_toggle_keyp))
            {
                nightVisionEnabled = !nightVisionEnabled;
                Player_Update_Patcher.ToggleNightVision();
            }
        }

        private const string myGUID = "com.mrjumpscare.nightvisionupdate";
        private const string pluginName = "Night Vision Chip Update";
        private const string versionString = "1.0.1";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        public static float ambientIntensity;
        public static Color ambientLight;
        internal static NightConfig config { get; } = OptionsPanelHandler.RegisterModOptions<NightConfig>();

        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        private static string ModPath = Path.GetDirectoryName(NightVisionChipMain.myAssembly.Location);

        internal static string AssetsFolder = Path.Combine(NightVisionChipMain.ModPath, "Assets");

        public void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;
            InitializePrefabs();
        }
        private void InitializePrefabs()
        {
            NightVisionChip.Register();
        }

        [Menu("Night Vision Update")]
        public class NightConfig : ConfigFile
        {
            [Keybind("Night Vision Toggle Key")]
            public KeyCode nightvision_toggle_keyp = KeyCode.P;
        }
    }
}

