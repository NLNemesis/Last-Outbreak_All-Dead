using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    public RuntimeAnimatorController controller;

    [Header("Data")]
    public int gunID;
    public float damage;
    public int ammo;
    public int maxAmmo;

    [Header("Recoil")]
    public bool Shoot;
    public float DamageDelay;
    public float RecoilDelay;

    private bool aiming;
    private float horizontalMove;
    private float verticalMove;

    [Header("References")]
    private PlayerMovement pm;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        animator = GetComponentInParent<Animator>();
        animator.runtimeAnimatorController = controller;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    #region Handle Movement
    void HandleMovement()
    {
        if (Input.GetMouseButton(1) && !aiming)
        {
            aiming = true;
            animator.SetBool("isAiming", true);
            animator.SetTrigger("Aim");
            pm.FreezePlayer();
        }

        if (aiming)
        {
            #region Movement-Rotation
            //Rotate Player
            horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * pm.rotateSpeed;
            pm.gameObject.transform.Rotate(0, horizontalMove, 0);
            //View Player
            verticalMove = Input.GetAxisRaw("Vertical");
            if (verticalMove > 0.2f)
            {
                animator.SetBool("Up", true);
                animator.SetBool("Center", false);
                animator.SetBool("Down", false);
            }
            else if (verticalMove < -0.2f)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Center", false);
                animator.SetBool("Down", true);
            }
            else
            {
                animator.SetBool("Up", false);
                animator.SetBool("Center", true);
                animator.SetBool("Down", false);
            }
            #endregion
        }

        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            animator.SetBool("aiming", false);
            animator.SetTrigger("Aim");
            pm.UnFreezePlayer();
        }
    }
    #endregion

    void HandleAnimations()
    {
        if (aiming)
        {
            if (Input.GetMouseButtonDown(0) && Shoot)
                StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        Shoot = false;
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(DamageDelay);
        Debug.Log("Deal Damage");
        yield return new WaitForSeconds(RecoilDelay - DamageDelay);
        Shoot = true;
    }
}
