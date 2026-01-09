using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetect : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    public bool dealDamage;
    public bool canAttack;

    [Header("References")]
    private AIMove aiMove;
    private PlayerMovement pm;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        aiMove = GetComponentInParent<AIMove>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && canAttack)
            aiMove.Attack();
        else if (other.name == "Player" && dealDamage)
            aiMove.DealDamage();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player" && canAttack)
            aiMove.Attack();
        else if (other.name == "Player" && dealDamage)
            aiMove.DealDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" && canAttack)
            aiMove.Attack();
        else if (other.name == "Player" && dealDamage)
            aiMove.DealDamage();
    }
}
