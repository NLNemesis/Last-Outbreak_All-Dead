using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    public int health;
    public float speed;
    private float currentSpeed;
    private bool playerDetected;

    [Header("References")]
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            playerDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (playerDetected)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
            animator.Play("Walk");
        }
        else if (playerDetected && !player.activeSelf)
        {
            playerDetected = false;
            agent.speed = 0;
            agent.SetDestination(this.transform.position);
            animator.Play("Idle");
        }
        else
        {
            playerDetected = false;
            agent.speed = 0;
            agent.SetDestination(this.transform.position);
            animator.Play("Idle");
        }
    }

    public void TakeDamage(int value)
    {
        health -= value;

        if (health <= 0)
        {
            agent.enabled = false;
            animator.Play("Death");
        }
        else
        {
            animator.Play("Hit");
        }
    }
}
