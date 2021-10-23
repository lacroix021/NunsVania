using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    private Skeleton skele;

    public bool statusDead = false;

    // Start is called before the first frame update
    void Start()
    {
        skele = GetComponent<Skeleton>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //esta funcion la deben tener todos los enemigos para que cuando se haga cambio de room, respawneen
    public void ResetEnemy()
    {
        skele.gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
        skele.currentHealth = skele.healthMax;
    }
}
