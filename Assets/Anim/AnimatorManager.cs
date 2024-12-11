using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetup;

    public enum AnimationType{
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f){
        foreach(var animation in animatorSetup){
            if(animation.type == type){
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedFactor;
                break;
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Play(AnimationType.RUN);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Play(AnimationType.IDLE);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            Play(AnimationType.DEAD);
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}