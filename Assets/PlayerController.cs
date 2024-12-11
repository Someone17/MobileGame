using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed;
    private Vector3 _pos;
    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";

    public string tagToCheckEndLine = "EndLine";

    private bool _canRun;

    public GameObject EndScreen;

    private float _currentSpeed;
    private Vector3 _startPosition;

    public bool invincible = false;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private float _baseSpeedToAnimation = 7;
    
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    public static PlayerController Instance { get; private set; }

    void Update()
    {
        if(!_canRun) return;

        
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
        
    }

    private void Start(){
        _startPosition = transform.position;
        ResetSpeed();
        animatorManager.Play(AnimatorManager.AnimationType.RUN);

    }

    private void OnCollisionEnter(Collision collision){
        if(collision.transform.tag == tagToCheckEnemy){
            if(!invincible){ 
            MoveBack();
            EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

     private void OnTriggerEnter(Collider other){
        if(other.transform.tag == tagToCheckEndLine){
            if(!invincible) EndGame();
        }
     }

    private void MoveBack(){
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE){
        _canRun = false;
        EndScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void Awake(){
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
        EndScreen.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPowerUpText(string s){
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeed(float f){
        _currentSpeed = f;
    }

    public void ResetSpeed(){
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b = true){
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease){
       /* var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(){
       /* var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/
        transform.DOMoveY(_startPosition.y, .1f);
    }
}
