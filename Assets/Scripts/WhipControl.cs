using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipControl : MonoBehaviour
{
    public float weaponState;
    private PlayerController playerController;

    public Sprite leatherWhipBack;
    public Sprite leatherWhipFront;

    public Sprite chainWhipBack;
    public Sprite chainWhipFront;

    public Sprite fireWhipBack;
    public Sprite fireWhipFront;


    public SpriteRenderer whipStandBack;
    public SpriteRenderer whipStandFront;
    public SpriteRenderer whipCrouchBack;
    public SpriteRenderer whipCrouchFront;

    [Header("COLISIONADORES LATIGO")]
    public BoxCollider2D collWhipStandFront;
    public BoxCollider2D collWhipCrouchFront;

    [Header("POSICIONES LATIGO")]
    public Transform posWhipStandFront;
    public Transform posWhipCrouchFront;

    public GameObject whipStand;
    public GameObject whipCrouch;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponState = playerController.weaponState;



        switch (weaponState)
        {
            case 0:
                whipStand.GetComponent<WhipCombat>().attackDamage = 1;
                whipCrouch.GetComponent<WhipCombat>().attackDamage = 1;

                whipStandBack.sprite = leatherWhipBack;
                whipStandFront.sprite = leatherWhipFront;
                whipCrouchBack.sprite = leatherWhipBack;
                whipCrouchFront.sprite = leatherWhipFront;
                collWhipStandFront.size = new Vector2(0.7296486f, 0.1383057f);
                collWhipStandFront.offset = new Vector2(0.008600235f, -0.006771088f);
                collWhipCrouchFront.size = new Vector2(0.7296486f, 0.1383057f);
                collWhipCrouchFront.offset = new Vector2(0.008600235f, -0.006771088f);
                posWhipStandFront.localPosition = new Vector3(0.778f, 0.658f, 0);
                posWhipCrouchFront.localPosition = new Vector3(0.811f, 0.437f, 0);
                break;

            case 1:
                whipStand.GetComponent<WhipCombat>().attackDamage = 2;
                whipCrouch.GetComponent<WhipCombat>().attackDamage = 2;
                whipStandBack.sprite = chainWhipBack;
                whipStandFront.sprite = chainWhipFront;
                whipCrouchBack.sprite = chainWhipBack;
                whipCrouchFront.sprite = chainWhipFront;
                collWhipStandFront.size = new Vector2(1.233116f, 0.1383057f);
                collWhipStandFront.offset = new Vector2(0.005054474f, -0.006771088f);
                collWhipCrouchFront.size = new Vector2(1.233326f, 0.1383057f);
                collWhipCrouchFront.offset = new Vector2(0.007429123f, -0.006771088f);
                posWhipStandFront.localPosition = new Vector3(1.047f, 0.641f, 0);
                posWhipCrouchFront.localPosition = new Vector3(1.071f, 0.414f, 0);
                break;

            case 2:
                whipStand.GetComponent<WhipCombat>().attackDamage = 4;
                whipCrouch.GetComponent<WhipCombat>().attackDamage = 4;
                whipStandBack.sprite = fireWhipBack;
                whipStandFront.sprite = fireWhipFront;
                whipCrouchBack.sprite = fireWhipBack;
                whipCrouchFront.sprite = fireWhipFront;
                collWhipStandFront.size = new Vector2(1.233116f, 0.1383057f);
                collWhipStandFront.offset = new Vector2(0.005054474f, -0.006771088f);
                collWhipCrouchFront.size = new Vector2(1.233326f, 0.1383057f);
                collWhipCrouchFront.offset = new Vector2(0.007429123f, -0.006771088f);
                posWhipStandFront.localPosition = new Vector3(1.047f, 0.641f, 0);
                posWhipCrouchFront.localPosition = new Vector3(1.071f, 0.414f, 0);
                break;
        }
    }
}
