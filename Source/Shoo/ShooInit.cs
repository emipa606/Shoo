using System.Reflection;
using HarmonyLib;
using Verse;

namespace Shoo;

[StaticConstructorOnStartup]
internal static class ShooInit
{
    static ShooInit()
    {
        new Harmony("jamaicancastle.shoo").PatchAll(Assembly.GetExecutingAssembly());
    }
}