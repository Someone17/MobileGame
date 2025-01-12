using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide = 3;
    [Header("Sounds")]
    public AudioSource audioSource;
    public GameObject graphicItem;

    public ParticleSystem particleSystem;

    private void Start(){
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.transform.CompareTag(compareTag)){
            Collect();
        }
    }

    protected virtual void HideItens(){
        if(graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        
    }

   protected virtual void Collect()
    {
        HideItens();
        OnCollect();
    }

    private void HideObject(){
        gameObject.SetActive(false);
    }

    // Update is called once per frame
   protected virtual void OnCollect()
    {
        
        if(audioSource != null) audioSource.Play();
        if(particleSystem != null){
            particleSystem.transform.SetParent(null);
            particleSystem.Play();
        
        } 
            
    }
}
