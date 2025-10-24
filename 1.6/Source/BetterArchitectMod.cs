using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterArchitect
{
    public class BetterArchitectMod : Mod
    {
        public static BetterArchitectSettings settings;

        public BetterArchitectMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<BetterArchitectSettings>();
            BetterArchitectSettings.mod = this;
            Harmony harmonyInstance = new Harmony("BetterArchitectMod");

            PatchVFEMedievalLeatherMattingGenerator(harmonyInstance);

            harmonyInstance.PatchAll();
        }

        private static void PatchVFEMedievalLeatherMattingGenerator(Harmony harmonyInstance)
        {
            System.Reflection.MethodInfo originalLeatherMattingGenerator = AccessTools.TypeByName("VFEMedieval.VFEMedieval_DefGenerator_GenerateImpliedDefs_PreResolve_Patch").GetMethod("TerrainFromLeather");
            System.Reflection.MethodInfo postfix = typeof(TerrainFromLeather).GetMethod("Postfix");

            if (originalLeatherMattingGenerator != null && postfix != null)
                harmonyInstance.Patch(original: originalLeatherMattingGenerator, postfix: postfix);
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("BA.MenuHeight".Translate() + ": " + Mathf.RoundToInt(BetterArchitectSettings.menuHeight));
            BetterArchitectSettings.menuHeight = listingStandard.Slider(BetterArchitectSettings.menuHeight, 250f, 600f);
            listingStandard.Gap();
            listingStandard.Label("BA.BackgroundAlpha".Translate() + ": " + BetterArchitectSettings.backgroundAlpha.ToStringPercent());
            BetterArchitectSettings.backgroundAlpha = listingStandard.Slider(BetterArchitectSettings.backgroundAlpha, 0f, 1.0f);
            listingStandard.Gap();
            listingStandard.CheckboxLabeled("BA.HideOnSelection".Translate(), ref BetterArchitectSettings.hideOnSelection, "BA.HideOnSelectionTooltip".Translate());
            listingStandard.Gap();
            listingStandard.CheckboxLabeled("BA.RememberSubcategory".Translate(), ref BetterArchitectSettings.rememberSubcategory, "BA.RememberSubcategoryTooltip".Translate());

            listingStandard.Gap();
            listingStandard.CheckboxLabeled("BA.UseSpecialFloorsTab".Translate(), ref BetterArchitectSettings.useSpecialFloorsTab, "BA.UseSpecialFloorsTabTooltip".Translate());

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory() => Content.Name;
    }
}
