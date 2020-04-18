﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpLandsknechteController : UnitController
{
    public override void InitializeUnit(int unitId, int armyId, int forwardAttackId, int leftAttackId, int rightAttackId, int tileId, int deployPosition)
    {
        Vector3 tempPos;

        base.InitializeUnit(unitId, armyId, forwardAttackId, leftAttackId, rightAttackId, tileId, deployPosition);
        _unitType = "Landsknechte";
        _unitCaption.text = "von Frundsberg";
        if (_armyId == 1)
        {
            _unitCaption.transform.position = transform.position + new Vector3(1.0f, 0.0f, -1.5f);
        }
        else
        {
            _unitCaption.transform.position = transform.position + new Vector3(1.0f, 0.0f, 2.0f);
            _unitCaption.transform.rotation = _unitCaption.transform.rotation * Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
        // inicjalizacja squadów
        for (int i = 0; i < initialStrength; i++)
        {
            tempPos = transform.position;
            tempPos.x = tempPos.x + (i % 3)*1.0f;
            if (i > 2)
            {
                if (_armyId == 1) tempPos.z = tempPos.z - 1.0f;
                else tempPos.z = tempPos.z + 1.0f;
            }
            if(i > 5)
            {
                if (_armyId == 1) tempPos.z = tempPos.z - 1.0f;
                else tempPos.z = tempPos.z + 1.0f;
            }
            _squads[i] = Instantiate(unitSquadPrefab, tempPos, transform.rotation);
            _squads[i].GetComponentInChildren<PawnController>().UnitId = _unitId;
            _squads[i].transform.SetParent(transform);
        }
        PlaceWidget(deployPosition);
    }
}
