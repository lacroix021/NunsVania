using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItemFireChain : MonoBehaviour
{
    public Button fireButton;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fireButton.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
