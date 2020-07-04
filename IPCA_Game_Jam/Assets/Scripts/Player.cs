using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator playerAnimator;
    private bool isWalking;
    private bool isAiming;
    private float speed;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        isAiming = false;
        playerAnimator = gameObject.GetComponent<Animator>();
        speed = 0.1f;

        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool aim = Input.GetButton("Aim");
        if (aim && !isAiming)
        {
            playerAnimator.SetBool("isAiming", true);
            isAiming = true;
        }
        else if(!aim && isAiming)
        {
            playerAnimator.SetBool("isAiming", false);
            isAiming = false;
        }

        if(aim && canShoot && Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            if (!isWalking)
            {
                isWalking = true;
                playerAnimator.SetBool("isWalking", isWalking);
            }

            float z = Input.GetAxis("Vertical")*speed;
            float x = Input.GetAxis("Horizontal")*speed;
            Walk(x, z);
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                playerAnimator.SetBool("isWalking", isWalking);
            }
        }

    }

    private void Walk(float x, float z)
    {
        gameObject.transform.Translate(new Vector3(x, 0, z));
    }

    private void Shoot()
    {
        playerAnimator.SetTrigger("shoot");
        canShoot = false;
        Invoke("CanShootAgain", .5f);
    }

    private void CanShootAgain()
    {
        canShoot = true;
    }
}
