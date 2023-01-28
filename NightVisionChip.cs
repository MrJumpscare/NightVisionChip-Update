using System;
using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using UnityEngine;
using HarmonyLib;
using SMLHelper.V2.Utility;

namespace Night_Vision_Update
{
    internal class NightVisionChip : Craftable
    {
     
        internal static TechType TechTypeID { get; private set; }

        public NightVisionChip() : base("NightVisionChip", "Night Vision HUD", "Adds night vision capabilities to your scuba HUD.")
        {
            this.OnFinishedPatching = (Spawnable.PatchEvent)Delegate.Combine(this.OnFinishedPatching, new Spawnable.PatchEvent(this.AdditionalPatching));
        }

        public override CraftTree.Type FabricatorType { get; } = CraftTree.Type.Fabricator;

        public override TechGroup GroupForPDA { get; } = TechGroup.Personal;

        public override TechCategory CategoryForPDA { get; } = TechCategory.Equipment;

        public override string AssetsFolder { get; } = NightVisionChipMain.AssetsFolder;

        public override string[] StepsToFabricatorTab { get; } = new string[]
        {
            "Personal",
            "Equipment"
        };
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Compass, true);
            yield return task;
            GameObject originalPrefab = task.GetResult();
            GameObject resultPrefab = UnityEngine.Object.Instantiate<GameObject>(originalPrefab);
            gameObject.Set(resultPrefab);
            yield break;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.Magnetite, 1)
                }
            };
        }

        private void AdditionalPatching()
        {
            NightVisionChip.TechTypeID = base.TechType;
            CraftDataHandler.SetEquipmentType(base.TechType, EquipmentType.Chip);
        }

        private static readonly NightVisionChip main = new NightVisionChip();

        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(AssetsFolder + "/NightVisionChip.png");
        }
    }
}
