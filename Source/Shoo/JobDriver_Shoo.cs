using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Shoo;

internal class JobDriver_Shoo : JobDriver
{
    private const TargetIndex AnimalInd = TargetIndex.A;

    private readonly SimpleCurve relativeBodySizeFactor =
    [
        new CurvePoint(0.2f, 5f), // squirrel
        new CurvePoint(1f, 1f), // deer, big cat
        new CurvePoint(2f, 1f), // elk, camel
        new CurvePoint(4f, 0.1f)
    ];

    private readonly SimpleCurve wildnessFactor =
    [
        new CurvePoint(0f, 5f), // housepets
        new CurvePoint(0.25f, 2f), // alpaca, dromedary
        new CurvePoint(0.75f, 1f), // deer, wolf
        new CurvePoint(1f, 0.1f)
    ];

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return pawn.Reserve(job.GetTarget(AnimalInd), job);
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        this.FailOnDespawnedNullOrForbidden(AnimalInd);
        this.FailOnDowned(AnimalInd);
        this.FailOnNotCasualInterruptible(AnimalInd);
        yield return Toils_Goto.GotoThing(AnimalInd, PathEndMode.Touch);
        var shoo = new Toil();
        shoo.initAction = delegate
        {
            var targets = (Pawn)shoo.actor.CurJob.GetTarget(AnimalInd);
            targets.mindState.lastAssignedInteractTime = Find.TickManager.TicksGame;
            var successOdds = shoo.actor.GetStatValue(StatDefOf.TameAnimalChance); // base odds
            successOdds *= relativeBodySizeFactor.Evaluate(targets.BodySize / shoo.actor.BodySize); // size factor
            successOdds *=
                wildnessFactor.Evaluate(targets.def.GetStatValueAbstract(StatDefOf.Wildness)); //wildness factor
            successOdds = Mathf.Clamp01(successOdds);
            //successOdds = 1f; //hi guys I'm the debug flag

            if (Rand.Chance(successOdds))
            {
                targets.Map.designationManager.RemoveDesignation(
                    targets.Map.designationManager.DesignationOn(targets, ShooDefOf.Shoo));
                if (findShooFleeCell(targets, out var c))
                {
                    targets.jobs.EndCurrentJob(JobCondition.InterruptForced, false);
                    targets.jobs.TryTakeOrderedJob(new Job(ShooDefOf.FleeBecauseShoo, c, shoo.actor)
                    {
                        canBashDoors = true
                    }); // SOMEHOW canBash makes it work for herd animals. Don't ask me to explain it, because I can't.
                    MoteMaker.ThrowText(targets.Position.ToVector3(), targets.Map,
                        "ShooSuccess"
                            .Translate()); // No, I mean it, Mehni and I went through the pathfinder line by line. We don't know why it won't work without that.
                }
                else
                {
                    Log.Warning("Could not find a valid shoo location for " + targets.LabelShort);
                }
            }
            else
            {
                MoteMaker.ThrowText(targets.Position.ToVector3(), targets.Map,
                    "ShooFailure".Translate(successOdds.ToStringPercent()));
                if (!Rand.Chance(targets.RaceProps.manhunterOnTameFailChance / 2f))
                {
                    return;
                }

                targets.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
                var label = "ManhuntOnFailedShooLabel".Translate(targets.LabelShort);
                var text = "ManhuntOnFailedShoo".Translate(targets.LabelShort);
                Find.LetterStack.ReceiveLetter(label, text, LetterDefOf.ThreatSmall, targets);
            }
        };
        yield return shoo;
    }

    private bool findShooFleeCell(Pawn p, out IntVec3 cell, int minFleeDist = 30, int maxFleeDist = 50)
    {
        //try to find cells between 30 and 50 cells away that are not inside a building
        //if we fail to do so, at least try to find one that's walkable
        var root = p.Position;
        for (var i = 0; i < 100; i++)
        {
            cell = root + IntVec3.FromVector3(Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) *
                                              Rand.RangeInclusive(minFleeDist, maxFleeDist));
            var room = cell.GetRoom(pawn.Map);
            if (cell.Walkable(pawn.Map) && (i > 50 || room == null || room.IsHuge))
            {
                return true;
            }
        }

        cell = p.Position;
        return false;
    }
}