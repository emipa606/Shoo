using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace Shoo;

internal class WorkGiver_Shoo : WorkGiver_InteractAnimal
{
    private const float RefireTicks = 1250;

    public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
    {
        return pawn.Map.designationManager.SpawnedDesignationsOfDef(ShooDefOf.Shoo).Select(des => des.target.Thing);
    }

    protected override bool CanInteractWithAnimal(Pawn pawn, Pawn animal, bool forced)
    {
        if (!pawn.CanReserve(animal))
        {
            return false;
        }

        return !animal.Downed && animal.CanCasuallyInteractNow();
    }

    public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
    {
        if (t is not Pawn pawn2 || !pawn2.NonHumanlikeOrWildMan())
        {
            return null;
        }

        if (pawn.Map.designationManager.DesignationOn(t, ShooDefOf.Shoo) == null)
        {
            return null;
        }

        if (!(Find.TickManager.TicksGame < pawn2.mindState.lastAssignedInteractTime + RefireTicks))
        {
            return !CanInteractWithAnimal(pawn, pawn2, forced) ? null : JobMaker.MakeJob(ShooDefOf.ShooJob, t);
        }

        JobFailReason.Is(AnimalInteractedTooRecentlyTrans);
        return null;
    }
}