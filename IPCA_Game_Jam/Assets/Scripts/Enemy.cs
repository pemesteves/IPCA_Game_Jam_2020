using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform target;
    NavMeshAgent agent;
    private Animator animator;

    public GameObject minimapIcon;

    private CapsuleCollider collider;

    /* SOUNDS */
    public AudioClip[] zombieSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] bitingSounds;
    private AudioSource audioSource;
    private bool playingAttackSound;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        Invoke("EnableCollider", 1.0f);
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayRandomSound", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius) {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("isAgro", true);
            minimapIcon.layer = LayerMask.NameToLayer("DisplayMinimapEnemy");

            if (distance <= agent.stoppingDistance) {
                animator.SetBool("isAttacking", true);
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                PlayAttackSound();
            } else {
                animator.SetBool("isAttacking", false);
            }
        } else
        {
            minimapIcon.layer = LayerMask.NameToLayer("MinimapEnemy");
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
        collider.enabled = true;
    }

    private void PlayRandomSound()
    {
        audioSource.clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
        audioSource.Play();
        Invoke("PlayRandomSound", audioSource.clip.length * 1.5f);
    }

    private void PlayAttackSound()
    {
        if (playingAttackSound) return;
        CancelInvoke("PlayRandomSound");
        audioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
        audioSource.Play();
        Invoke("FinishAttackSound", audioSource.clip.length);
        playingAttackSound = true;
    }

    private void FinishAttackSound()
    {
        playingAttackSound = false;
        PlayRandomSound();
    }
}
