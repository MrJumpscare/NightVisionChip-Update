using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using SMLHelper.V2.Handlers;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;

namespace Night_Vision_Update
{

    
        [BepInPlugin(myGUID, pluginName, versionString)]
        public class NightVisionChipMain : BaseUnityPlugin
        {
        public void LateUpdate()
        {
            if (Input.GetKeyDown(NightVisionChipConfig.nightvision_toggle_key))
            {
                NightVisionChipConfig.nightVisionEnabled = !NightVisionChipConfig.nightVisionEnabled;
                Player_Update_Patcher.ToggleNightVision();
            }
        }

        private const string myGUID = "com.mrjumpscare.nightvisionupdate";
            private const string pluginName = "Night Vision Chip Update";
            private const string versionString = "1.0.0";

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
                NightVisionChipMain.harmony.PatchAll();
                NightVisionChip nightVisionChip = new NightVisionChip();
                nightVisionChip.Patch();
        }
        }
    }

