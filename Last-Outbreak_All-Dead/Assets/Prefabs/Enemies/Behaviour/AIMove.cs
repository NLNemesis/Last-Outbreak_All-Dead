using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    private bool detectedPlayer;

    [Header("Stats")]
    public int health;
    public float moveSpeed;
    private float currentSpeed;

    [Header("References")]
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    #endregion

    #region On Triggers
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            detectedPlayer = true;
            player = other.gameObject;
        }
        else
        {
            detectedPlayer = false;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlayer)
        {
            agent.speed = moveSpeed;
            agent.SetDestination(player.transform.position);
            animator.Play("Walk");
        }
        else
        {
            agent.speed = 0;
            animator.Play("Idle");
        }
    }
}
