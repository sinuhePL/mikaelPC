﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndGameController : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button start2Button;
    [SerializeField] Button quitButton;
    private int winnerId;
    [SerializeField] private Text winnerText;
    [SerializeField] private Image helmet;
    [SerializeField] private Image shield;
    [SerializeField] private Sprite hreHelmet;
    [SerializeField] private Sprite franceHelmet;
    [SerializeField] private Sprite hreShield;
    [SerializeField] private Sprite franceShield;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.onGameOver += SetWinnerId;
        EventManager.onResultMenuClosed += ShowEndGamePanel;
        winnerId = 0;
    }

    public void QuitClicked()
    {
        SoundManagerController.Instance.PlayClick();
        quitButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.15f, 20);
        GameManagerController.Instance.LoadLevel("MenuScene");
    }

    public void StartClicked()
    {
        Sequence mySequence30 = DOTween.Sequence();
        SoundManagerController.Instance.PlayClick();
        mySequence30.Append(startButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.15f, 20));
        mySequence30.Append(transform.DOScale(0.0f, 0.25f).SetEase(Ease.InBack));
        //SoundManagerController.Instance.ResumeMusic(1.0f);
        BattleManager.Instance.StartAgain();
        BattleManager.Instance.isInputBlocked = false;
        winnerId = 0;
        SoundManagerController.Instance.ResumeMusic();
    }

    public void Start2Clicked()
    {
        Sequence mySequence31 = DOTween.Sequence();
        SoundManagerController.Instance.PlayClick();
        mySequence31.Append(start2Button.transform.DOPunchScale(new Vector3(0.1f, 0.1f), 0.15f, 20));
        mySequence31.Append(transform.DOScale(0.0f, 0.25f).SetEase(Ease.InBack));
        //SoundManagerController.Instance.ResumeMusic(1.0f);
        BattleManager.Instance.StartAfterDeployment();
        BattleManager.Instance.isInputBlocked = false;
        winnerId = 0;
        //SoundManagerController.Instance.ResumeMusic();
    }

    private void ShowEndGamePanel(string mode)
    {
        if (winnerId != 0)
        {
            SoundManagerController.Instance.StopMusic();
            BattleManager.Instance.isInputBlocked = true;
            if (winnerId == 2)
            {
                if (GameManagerController.Instance.isPlayer2Human) SoundManagerController.Instance.PlayEndGame(true);
                else SoundManagerController.Instance.PlayEndGame(false);
                winnerText.text = "Kingdom of France";
                helmet.sprite = franceHelmet;
                shield.sprite = franceShield;
            }
            else if (winnerId == 1)
            {
                if (GameManagerController.Instance.isPlayer1Human) SoundManagerController.Instance.PlayEndGame(true);
                else SoundManagerController.Instance.PlayEndGame(false);
                winnerText.text = "Holy Roman Empire";
                helmet.sprite = hreHelmet;
                shield.sprite = hreShield;
            }
            transform.DOScale(1.5f, 0.25f).SetEase(Ease.OutBack);
        }
    }

    private void SetWinnerId(int wId)
    {
        winnerId = wId;
    }

    private void OnDestroy()
    {
        EventManager.onGameOver -= SetWinnerId;
        EventManager.onResultMenuClosed -= ShowEndGamePanel;
    }
}
