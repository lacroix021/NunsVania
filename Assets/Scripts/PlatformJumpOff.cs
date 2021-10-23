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
            if (Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                jumpOff = true;
            }
            else if (Input.GetButtonUp("Jump") || CrossPlatformInputManager.GetButtonUp("Jump"))
            {
                jumpOff = false;
            }

            bool crouched = coll.gameObject.GetComponent<PlayerController>().isCrouched;
            Collider2D colisionPlayer = coll.collider.GetComponent<Collider2D>();

            if (crouched && jumpOff)
            {
                StartCoroutine(IgnorarColision(colisionPlayer));
            }
        }
    }

    IEnumerator IgnorarColision(Collider2D playerCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, this.gameObject.GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, this.gameObject.GetComponent<Collider2D>(), false);

    }
}
