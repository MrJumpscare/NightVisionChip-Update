using SMLHelper.V2.Options;
using System;
using UnityEngine;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace Night_Vision_Update
{
    [Menu("Night Vision Update")]
    public class NightConfig : ConfigFile
    {
        [Keybind("Night Vision Toggle Key")]
        public KeyCode nightvision_toggle_keyp = KeyCode.P;
    }

}

