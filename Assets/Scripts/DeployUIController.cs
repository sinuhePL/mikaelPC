﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DeployUIController : MonoBehaviour
{
    private PictureController myPictureController;
    private WidgetController myWidgetController;
    private CaptionController myCaptionController;
    private SMController[] mySMControllers;
    private bool isEnlarged;
    private int myPosition;
    private int armyId;
    private int unitId;
    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Awake()
    {
        myPictureController = GetComponentInChildren<PictureController>();
        myWidgetController = GetComponentInChildren<WidgetController>();
        myCaptionController = GetComponentInChildren<CaptionController>();
        mySMControllers = GetComponentsInChildren<SMController>();
        isEnlarged = false;
    }

    private void OnEnable()
    {
        EventManager.onUIDeployPressed += MoveDown;
        EventManager.onUnitClicked += UnitClicked;
        EventManager.onDeploymentStart += DeploymentStart;

    }

    private void OnDestroy()
    {
        EventManager.onUIDeployPressed -= MoveDown;
        EventManager.onUnitClicked -= UnitClicked;
        EventManager.onDeploymentStart -= DeploymentStart;
    }

    private void MoveDown(int aId, int p, int uId)
    {
        if (aId == armyId && p == myPosition) return;
        if (isEnlarged)
        {
            isEnlarged = false;
            transform.DOScale(0.28f, 0.25f).SetEase(Ease.OutBack);
            transform.DOMoveX(55.0f, 0.25f).SetEase(Ease.OutBack);
            transform.DOMoveY(transform.position.y + 25.0f, 0.25f).SetEase(Ease.OutBack);
            if (aId == armyId && p < myPosition) transform.DOMoveY(startingPosition.y - 42.0f, 0.25f).SetEase(Ease.OutBack);
            return;
        }
        if(aId == armyId && p < myPosition) transform.DOMoveY(startingPosition.y - 42.0f, 0.25f).SetEase(Ease.OutBack);
        if(aId == armyId && p > myPosition) transform.DOMoveY(startingPosition.y, 0.25f).SetEase(Ease.OutBack);
    }

    private void UnitClicked(int uId)
    {
        if (uId == unitId) WidgetPressed();
    }

    private void DeploymentStart(int aId)
    {
        if (aId > armyId) Destroy(gameObject);
    }

    public void InitializeDeploy(int armyId, string unitType, int strength, int morale, int position, int aId, int myId)
    {
        myPictureController.InitialPicture(unitType);
        myWidgetController.InitalColor(armyId);
        myCaptionController.InitialCaption(unitType);
        mySMControllers[0].InitialSM(strength, morale);
        mySMControllers[1].InitialSM(strength, morale);
        myPosition = position;
        startingPosition = transform.position;
        armyId = aId;
        unitId = myId;
        if (position == 0)
        {
            WidgetPressed();
        }
        else transform.position = new Vector3(transform.position.x, transform.position.y - 42.0f, transform.position.z);
    }

    public void WidgetPressed()
    {
        if (!isEnlarged)
        {
            transform.DOScale(0.4f, 0.25f).SetEase(Ease.OutBack);
            transform.DOMoveX(75.0f, 0.25f).SetEase(Ease.OutBack);
            transform.DOMoveY(startingPosition.y - 25.0f, 0.25f).SetEase(Ease.OutBack);
            isEnlarged = true;
            EventManager.RaiseEventOnUIDeployPressed(armyId, myPosition, unitId);
        }
    }
}
