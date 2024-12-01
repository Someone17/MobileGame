using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpHeight : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2;
    public float animationDuration = 1f;
    public Ease ease = Ease.OutBack;

    protected override void StartPowerUp(){
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountHeight, duration, animationDuration, ease);
    } 

    protected override void EndPowerUp(){
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight();
    }
}
