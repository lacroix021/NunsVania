using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCombat : MonoBehaviour
{

    public int attackDamage1 = 2;
    public int attackDamage2 = 3;

    public PlayerController pController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(pController.weaponState == 1)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                
                coll.GetComponent<Skeleton>().TakeDamage(attackDamage1);

            }
        }
        
        else if(pController.weaponState == 2)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                
                coll.GetComponent<Skeleton>().TakeDamage(attackDamage2);

            }
        }
        

    }
}
