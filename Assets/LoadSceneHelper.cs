using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s);
    }
}
