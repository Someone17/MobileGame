using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectable> itens;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start(){
        itens = new List<ItemCollectable>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            StartAnimations();
        }
    }

    public void RegisterCoin(ItemCollectable i)
    {
        if(itens.Contains(i)){
            itens.Add(i);
            transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimations(){
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime(){
        foreach(var p in itens){
            p.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for(int i = 0; i < itens.Count; i++){
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort(){
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
