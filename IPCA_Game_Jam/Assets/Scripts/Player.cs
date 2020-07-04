using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public GameController gameController;

    /* PLAYER ANIMATOR */
    private Animator playerAnimator;
    private bool isWalking;
    private bool isAiming;
    private bool isHoldingLantern;

    private float speed;

    /* SHOOTING */
    private bool canShoot;
    private GameObject gun;
    public GameObject gunBarrel;
    private Transform gunBarrelTransform;
    public GameObject bullet; //Bullet Prefab
    public ParticleSystem muzzleFlash;
    public GameObject target;

    /* HEALTH BAR */
    public HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isWalking = false;
        isAiming = false;
        isHoldingLantern = false;
        playerAnimator = gameObject.GetComponent<Animator>();
        speed = 4f;

        canShoot = true;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        gun = GameObject.FindGameObjectWithTag("Gun");
    }

    void FixedUpdate()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);

        bool holdLantern = Input.GetButton("HoldLantern");
        if (holdLantern && !isHoldingLantern)
        {
            playerAnimator.SetBool("isHoldingLantern", true);
            isHoldingLantern = true;
        }
        else if(!holdLantern && isHoldingLantern)
        {
            playerAnimator.SetBool("isHoldingLantern", false);
            isHoldingLantern = false;
        }

        bool aim = Input.GetButton("Aim");
        if (aim && !isAiming)
        {
            playerAnimator.SetBool("isAiming", true);
            isAiming = true;
            target.SetActive(true);
        }
        else if(!aim && isAiming)
        {
            playerAnimator.SetBool("isAiming", false);
            isAiming = false;
            target.SetActive(false);
        }

        if (aim && canShoot && Input.GetButtonDown("Shoot")) {
            gunBarrelTransform = gunBarrel.transform;
            Shoot();
        }
    }

    private void Walk(float x, float z)
    {
        Vector3 vel = transform.TransformDirection(new Vector3(x, 0, z));
		vel = vel - rb.velocity;

		vel.x = Mathf.Clamp(vel.x, -10, 10);
		vel.z = Mathf.Clamp(vel.z, -10, 10);
		vel.y = 0;
		rb.AddForce(vel, ForceMode.VelocityChange);
    }

    private void Shoot()
    {
        playerAnimator.SetTrigger("shoot");
        gun.GetComponent<AudioSource>().Play();
        muzzleFlash.Play();
        canShoot = false;
        Invoke("CanShootAgain", .4f);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30);
        if(hit.collider.gameObject.tag == "Enemy")
            hit.collider.gameObject.GetComponent<Enemy>().getHit();
    }

    private void CanShootAgain()
    {
        canShoot = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            playerAnimator.SetTrigger("die");
            gameController.GameOver();
        }
    }
}
