using HarmonyLib;
using RimWorld;
using Verse;

namespace Shoo;

[HarmonyPatch(typeof(Building_Door), nameof(Building_Door.PawnCanOpen))]
internal class Building_Door_PawnCanOpen
{
    public static void Postfix(ref bool __result, Pawn p)
    {
        __result = __result || p.CurJob?.def == ShooDefOf.FleeBecauseShoo;
    }
}