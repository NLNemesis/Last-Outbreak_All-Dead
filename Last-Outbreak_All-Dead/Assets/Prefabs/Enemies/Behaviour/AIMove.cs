using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        currentSpeed = speed;
        agent.speed = 0;

        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
            playerDetected = true;
        else
            playerDetected = false;
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
            animator.Play("Walk");
            agent.speed = currentSpeed;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            animator.Play("Idle");
            agent.speed = 0;
            agent.SetDestination(this.transform.position);
        }
    }
}
