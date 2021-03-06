﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Attack
{
    public CounterAttack(int aId, int arrId, bool state, int army, Unit o, int keyField, bool isKFTaken, int tId, Vector3 p, string aType, string dType) : base(aId, arrId, state, army, o, keyField, isKFTaken, tId, p)
    {
        attackName = "Counter Attack";
        switch (aType)
        {
            case "Gendarmes":
                attackDiceNumber = 4;
                defenceDiceNumber = 0;
                specialOutcomeType = 5;
                break;
            case "Landsknechte":
                attackDiceNumber = 2;
                defenceDiceNumber = 0;
                specialOutcomeType = 2;
                break;
            case "Suisse":
                attackDiceNumber = 2;
                defenceDiceNumber = 0;
                specialOutcomeType = 1;
                break;
            case "Imperial Cavalery":
                attackDiceNumber = 4;
                defenceDiceNumber = 0;
                specialOutcomeType = 8;
                break;
            case "Arquebusiers":
                attackDiceNumber = 5;
                defenceDiceNumber = 0;
                specialOutcomeType = 5;
                break;
            case "Artillery":
                attackDiceNumber = 3;
                defenceDiceNumber = 0;
                specialOutcomeType = 6;
                break;
        }
    }

    public CounterAttack(int aId, int arrId, bool state, int army, Unit o, int keyField, bool isKFTaken, int tId, Vector3 p, int aNum, int dNum) : base(aId, arrId, state, army, o, keyField, isKFTaken, tId, p)
    {
        attackName = "Counter Attack";
        attackDiceNumber = aNum;
        defenceDiceNumber = dNum;
    }

    public override void SpecialOutcome(ref StateChange sc)
    {
        if (keyFieldId != 0 && !isKeyFieldTaken)
        {
            sc.keyFieldChangeId = keyFieldId;
            sc.keyFieldNewOccupantId = owner.GetArmyId();
        }
        else
        {
            if (specialOutcomeType == 1)
            {
                sc.defenderMoraleChanged = -1;
            }
            if (specialOutcomeType == 2)
            {
                sc.defenderMoraleChanged = -2;
            }
            if (specialOutcomeType == 3)
            {
                sc.defenderStrengthChange = -1;
            }
            if (specialOutcomeType == 4)
            {
                sc.defenderStrengthChange = -2;
            }
            if (specialOutcomeType == 5)
            {
                sc.attackerMoraleChanged = 1;
            }
            if (specialOutcomeType == 6)
            {
                sc.attackerMoraleChanged = 2;
            }
        }
        sc.specialOutcomeDescription = GetSpecialOutcomeDescription();
    }

    public override string GetSpecialOutcomeDescription()
    {
        if (keyFieldId != 0 && !isKeyFieldTaken)
        {
            return BattleManager.Instance.GetKeyFieldName(keyFieldId) + " captured! \n (+1 attack die)";
        }
        else
        {
            if (specialOutcomeType == 1) return "Defender loses 1 morale";
            if (specialOutcomeType == 2) return "Defender loses 2 morale";
            if (specialOutcomeType == 3) return "Defender loses 1 strength";
            if (specialOutcomeType == 4) return "Defender loses 2 strength";
            if (specialOutcomeType == 5) return "Attacker gains 1 morale";
            if (specialOutcomeType == 6) return "Attacker gains 2 morale";
        }
        return "";
    }

    public override List<StateChange> GetOutcomes()
    {
        List<StateChange> stc;
        StateChange sc;

        stc = new List<StateChange>();
        if (attackDiceNumber == 2)
        {
            //                                                am  dm  as ds kf kfo
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.25f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.1111111f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.611111111f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.027777777f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
        }
        else if (attackDiceNumber == 3)
        {
            //                                                am  dm  as ds kf kfo
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.5f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.259259259f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.171296297f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.06944444f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
        }
        else if (attackDiceNumber == 4)
        {
            //                                                am  dm  as ds kf kfo
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.41666666f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.2098765432f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -2, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.0625f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -2, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.012345679f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.16666666f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.041666666f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.0185185185f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.071759259f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
        }
        else if (attackDiceNumber == 5)
        {
            //                                                am  dm  as ds kf kfo
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.13888888f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.0617289351f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -2, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.1875f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -2, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.045267489f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.37037037f);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, -1, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.11574f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, -1, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.06172839f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
            sc = new StateChange(owner.GetUnitId(), targetId, 0, 0, 0, 0, 0, 0, new List<int>(activatesAttacks.ToArray()), new List<int>(deactivatesAttacks.ToArray()), new List<int>(), 0.01877572f);
            SpecialOutcome(ref sc);
            stc.Add(sc);
        }
        return stc;
    }

    public override Attack GetCopy(Unit o)
    {
        CounterAttack nca;
        nca = new CounterAttack(attackId, arrowId, isActiveState, armyId, o, keyFieldId, isKeyFieldTaken, targetId, arrowPosition, attackDiceNumber, defenceDiceNumber);
        foreach (int i in activatesAttacks)
        {
            nca.AddActivatedAttackId(i);
        }
        foreach (int i in deactivatesAttacks)
        {
            nca.AddDeactivatedAttackId(i);
        }
        return (Attack)nca;
    }
}
