using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public string weaponName;
    private bool aiming;
    private float horizontalMove;

    [Header("References")]
    private PlayerMovement pm;
    private Inventory inventory;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
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
        if (Input.GetButton("Horizontal") && aiming)
        {
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * pm.rotateSpeed;
            pm.gameObject.transform.Rotate(0, horizontalMove, 0);
        }
    }

    void HandleAnimations()
    {
        if (Input.GetMouseButtonDown(1) && !aiming)
        {
            aiming = true;
            animator.SetBool(weaponName, true);
        }

        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            animator.SetBool(weaponName, false);
        }
    }
}
