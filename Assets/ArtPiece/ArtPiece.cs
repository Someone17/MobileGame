using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    public GameObject currentArt;
    
    public void ChangePiece(GameObject piece)
    {
        if(currentArt != null) Destroy(currentArt);

        currentArt = Instantiate(piece, transform);
        currentArt.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
