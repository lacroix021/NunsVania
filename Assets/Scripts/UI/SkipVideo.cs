using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipVideo : MonoBehaviour
{

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //<>
        timer += Time.deltaTime;

        if(timer >= 60)
        {
            Skip();
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene(2);
    }
}
