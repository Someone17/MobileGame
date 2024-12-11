using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation animation;
    public AnimationClip run;
    public AnimationClip idle;

    public void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            PlayAnimation(run);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            PlayAnimation(idle);
        }
    }
    private void PlayAnimation(AnimationClip c){
        animation.CrossFade(c.name);
    }
}
