using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour
{

    public static GlobalGameManager globalManager;



    // Start is called before the first frame update
    void Start()
    {
        if (!globalManager)
        {
            globalManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
}
