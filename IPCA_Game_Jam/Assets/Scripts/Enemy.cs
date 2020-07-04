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

    //private CapsuleCollider collider;

    private int life = 2;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        //collider = GetComponent<CapsuleCollider>();
        //collider.enabled = false;
        //Invoke("EnableCollider", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius && isAlive) {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("isAgro", true);
            minimapIcon.layer = LayerMask.NameToLayer("DisplayMinimapEnemy");

            if (distance <= agent.stoppingDistance) {
                animator.SetBool("isAttacking", true);
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
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
        GetComponent<Collider>().enabled = true;
    }

    public void getHit() {
        life--;
        Debug.Log("HI");
        if(life == 0) {
            agent.isStopped = true;
            isAlive = false;
            animator.SetBool("isDead", true);
        }
    }
}
