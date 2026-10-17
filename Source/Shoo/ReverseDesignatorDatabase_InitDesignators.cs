using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace Shoo;

[HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators")]
internal static class ReverseDesignatorDatabase_InitDesignators
{
    public static void Postfix(ref List<Designator> ___desList)
    {
        ___desList?.Add(new Designator_Shoo());
    }
}