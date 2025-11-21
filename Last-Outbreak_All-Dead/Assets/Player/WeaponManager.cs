using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public int weaponID;

    private bool aiming;
    private float horizontalMove;

    [Header("References")]
    private PlayerMovement PM;
    private Inventory inventory;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = GetComponentInParent<PlayerMovement>();
        inventory = GetComponentInParent<Inventory>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    void HandleMovement()
    {
        if (Input.GetButton("Horizontal"))
        {
            #region Rotation
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * PM.rotateSpeed;
            PM.gameObject.transform.Rotate(0, horizontalMove, 0);
            #endregion
        }
        else
        {
            horizontalMove = 0;
        }
    }

    void HandleAnimations()
    {
        if (Input.GetMouseButtonDown(1) && !aiming)
        {
            aiming = true;
            //Change animation layer;
        }

        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            //Change animation layer;
        }
    }
}
