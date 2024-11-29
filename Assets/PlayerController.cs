using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if(!_canRun) return;

        
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.transform.tag == tagToCheckEnemy){
            EndGame();
        }
    }

     private void OnTriggerEnter(Collider other){
        if(other.transform.tag == tagToCheckEndLine){
            EndGame();
        }
     }

    private void EndGame(){
        _canRun = false;
        EndScreen.SetActive(true);
    }

    public void Awake(){
        _canRun = true;
        EndScreen.SetActive(false);
    }
}
