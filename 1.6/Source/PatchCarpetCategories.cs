using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterArchitect;

[HarmonyPatch(typeof(TerrainDefGenerator_Carpet), nameof(TerrainDefGenerator_Carpet.CarpetFromBlueprint))]
public static class CarpetFromBlueprint
{
    static CarpetFromBlueprint()
    {
        Log.Message("[BetterArchitect] CarpetFromBlueprint patch loaded.");
    }

    public static void Postfix(TerrainDef __result, TerrainTemplateDef tp)
    {
        Log.Message("[BetterArchitect]CarpetFromBlueprint postfix called for " + __result.defName);
        if (tp.GetModExtension<TemplateCategoryExtension>() is { } extension)
        {
            __result.designationCategory = extension.designationCategory;
            Log.Message("[BetterArchitect] Template now has category " + __result.designationCategory.defName);
        }
    }
}