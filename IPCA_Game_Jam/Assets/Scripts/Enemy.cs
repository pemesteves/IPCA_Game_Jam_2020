﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameController gc;
    public float lookRadius = 10f;
    public float attackRadius;
    public GameObject player;
    public int value;
    NavMeshAgent agent;
    private Animator animator;

    public GameObject minimapIcon;

    //private CapsuleCollider collider;

    public int life;
    public int damage;

    private bool isAlive = true;

    public int minimapMaxDistance;

    /* SOUNDS */
    public AudioClip[] zombieSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] bitingSounds;
    private AudioSource audioSource;
    private bool attacking;

    private bool deadPlayer;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        //collider = GetComponent<CapsuleCollider>();
        //collider.enabled = false;
        //Invoke("EnableCollider", 1.0f);
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayRandomSound", 0f);
        deadPlayer = false;
        agent.updateRotation = true;
        velocity = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= minimapMaxDistance && isAlive)
            minimapIcon.layer = LayerMask.NameToLayer("DisplayMinimapEnemy");
        else
            minimapIcon.layer = LayerMask.NameToLayer("MinimapEnemy");

        if (deadPlayer && isAlive)
        {
            agent.isStopped = false;
            if (distance <= 0.5f)
            {
                animator.SetTrigger("bite");
            }
            else if (distance <= 2.5f)
            {
                agent.SetDestination(player.transform.position);
                agent.speed = velocity;
                animator.SetTrigger("crawl");
            }
            else
            {
                agent.SetDestination(player.transform.position);
                animator.SetBool("isAgro", true);
            }
        }
        else if (distance <= lookRadius && isAlive && !attacking) {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetBool("isAgro", true);

            if (distance <= agent.stoppingDistance) {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                Attack();
            } else {
                animator.SetBool("isAttacking", false);
            }
        } else {
            animator.SetBool("isAgro", false);
            agent.isStopped = true;
        }
    }

    void onGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void getHit() {
        life--;
        if(life == 0) {
            GetComponent<Collider>().enabled = false;
            agent.isStopped = true;
            isAlive = false;
            animator.SetBool("isDead", true);
            gc.KillEnemy(value);
        }
    }

    private void PlayRandomSound()
    {
        if(isAlive) {
            audioSource.clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
            audioSource.Play();
            Invoke("PlayRandomSound", audioSource.clip.length * 1.5f);
        }
    }

    private void Attack()
    {
        if (attacking) return;
        animator.SetBool("isAttacking", true);
        CancelInvoke("PlayRandomSound");
        audioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
        audioSource.Play();
        Invoke("Hit", 1.6f);
        Invoke("FinishAttack", 2.633f);
        attacking = true;
    }

    private void Hit() {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance < attackRadius) {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void FinishAttack()
    {
        attacking = false;
        PlayRandomSound();
    }

    public void SetPlayerDead()
    {
        deadPlayer = true;
    }
}
