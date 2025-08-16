using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace Shoo;

[HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators")]
internal class ReverseDesignatorDatabase_InitDesignators
{
    public static void Postfix(ref ReverseDesignatorDatabase __instance)
    {
        var desList =
            AccessTools.Field(typeof(ReverseDesignatorDatabase), "desList").GetValue(__instance) as List<Designator>;
        desList?.Add(new Designator_Shoo());
    }
}