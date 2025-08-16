using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Shoo;

internal class Designator_Shoo : Designator
{
    private readonly List<Pawn> justDesignated = [];

    public Designator_Shoo()
    {
        defaultLabel = "DesignatorShoo".Translate();
        defaultDesc = "DesignatorShooDesc".Translate();
        icon = ContentFinder<Texture2D>.Get("Shoo");
        soundDragSustain = SoundDefOf.Designate_DragStandard;
        soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
        useMouseIcon = true;
        soundSucceeded = SoundDefOf.Designate_Hunt;
        hotKey = KeyBindingDefOf.Misc1;
    }

    public override DrawStyleCategoryDef DrawStyleCategory => DrawStyleCategoryDefOf.Orders;

    public override AcceptanceReport CanDesignateCell(IntVec3 c)
    {
        if (!c.InBounds(Map))
        {
            return false;
        }

        if (!huntablesInCell(c).Any())
        {
            return "MessageMustDesignateShooable".Translate();
        }

        return true;
    }

    public override void DesignateSingleCell(IntVec3 loc)
    {
        foreach (var current in huntablesInCell(loc))
        {
            DesignateThing(current);
        }
    }

    public override AcceptanceReport CanDesignateThing(Thing t)
    {
        return t is Pawn pawn && pawn.AnimalOrWildMan() && pawn.Faction == null &&
               Map.designationManager.DesignationOn(pawn, ShooDefOf.Shoo) == null;
    }

    public override void DesignateThing(Thing t)
    {
        Map.designationManager.RemoveAllDesignationsOn(t);
        Map.designationManager.AddDesignation(new Designation(t, ShooDefOf.Shoo));
        justDesignated.Add((Pawn)t);
    }

    protected override void FinalizeDesignationSucceeded()
    {
        base.FinalizeDesignationSucceeded();
        foreach (var kind in (from p in justDesignated
                     select p.kindDef).Distinct())
        {
            var num = kind != PawnKindDefOf.WildMan ? kind.RaceProps.manhunterOnDamageChance : 0.5f;
            if (num > 0.2f)
            {
                Messages.Message("MessageAnimalDangerousToShoo".Translate(kind.GetLabelPlural()),
                    justDesignated.First(x => x.kindDef == kind), MessageTypeDefOf.CautionInput);
            }
        }

        justDesignated.Clear();
    }

    private IEnumerable<Pawn> huntablesInCell(IntVec3 c)
    {
        if (c.Fogged(Map))
        {
            yield break;
        }

        var thingList = c.GetThingList(Map);
        foreach (var thing in thingList)
        {
            if (CanDesignateThing(thing).Accepted)
            {
                yield return (Pawn)thing;
            }
        }
    }
}