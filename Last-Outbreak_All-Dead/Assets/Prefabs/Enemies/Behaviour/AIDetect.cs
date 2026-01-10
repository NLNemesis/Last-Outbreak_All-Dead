using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetect : MonoBehaviour
{
    #region Variables
    public bool canAttack;
    public bool dealDamage;

    private AIMove aiMove;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        aiMove = GetComponentInParent<AIMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player" && canAttack)
            aiMove.Attack();
        else if (other.name == "Player" && dealDamage)
            aiMove.DealDamage();
    }
}
