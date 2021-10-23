using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItemChain : MonoBehaviour
{

    public Button chainButton;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            chainButton.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }


}
