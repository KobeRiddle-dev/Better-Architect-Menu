using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterArchitect;

public static class TerrainFromLeather
{
    static DesignationCategoryDef CarpetCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Ferny_FloorsCarpet");

    public static void Postfix(TerrainDef __result)
    {
        __result.designationCategory = CarpetCategory;
    }
}