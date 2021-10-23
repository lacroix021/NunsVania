using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemies : MonoBehaviour
{
    //public Skeleton skel;

    public GameObject [] enemiesToRespawn;
    
    // Start is called before the first frame update
    void Start()
    {
        enemiesToRespawn = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {   //array para respawnear unicamente los enemigos que estan muertos
            for (int i= 0; i < enemiesToRespawn.Length; i++)
            {
                if(enemiesToRespawn[i].GetComponent<RespawnEnemy>().statusDead == true)
                {
                    enemiesToRespawn[i].GetComponent<RespawnEnemy>().ResetEnemy();
                    //print("estos son los que revivieron " + i);
                }
            }                    
        }        
    }


}
