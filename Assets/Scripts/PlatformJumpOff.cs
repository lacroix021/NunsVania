using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlatformJumpOff : MonoBehaviour
{
    public bool jumpOff;

    private void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Collider2D colisionPlayer = coll.collider.GetComponent<Collider2D>();
            bool crouched = coll.gameObject.GetComponent<PlayerController>().isCrouched;

            if (crouched && Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown("Jump") && crouched)
            {
                jumpOff = true;
            }

            if (crouched && jumpOff)
            {
                StartCoroutine(IgnorarColision(colisionPlayer));
            }
        }
        else
        {
            jumpOff = false;
        }
    }

    IEnumerator IgnorarColision(Collider2D playerCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, this.gameObject.GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(playerCollider, this.gameObject.GetComponent<Collider2D>(), false);
        jumpOff = false;
    }
}
