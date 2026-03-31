using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterArchitect;

[HarmonyPatch]
public static class TerrainFromLeather
{
    static DesignationCategoryDef CarpetCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Ferny_FloorsCarpet");
    
    [HarmonyTargetMethod]
    public static System.Reflection.MethodInfo TargetMethod()
    {
        return AccessTools.TypeByName("VFEMedieval.VFEMedieval_DefGenerator_GenerateImpliedDefs_PreResolve_Patch").GetMethod("TerrainFromLeather");
    }

    [HarmonyPostfix]
    public static void Postfix(TerrainDef __result)
    {
        __result.designationCategory = CarpetCategory;
    }
}