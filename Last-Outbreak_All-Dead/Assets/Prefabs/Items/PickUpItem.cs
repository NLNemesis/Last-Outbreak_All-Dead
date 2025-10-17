using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool GroundItem;
    private bool CanInteract;

    [Header("References")]
    private PlayerMovement PM;
    private Animator animator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            CanInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            CanInteract = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            PM.freeze = true;
            CanInteract = false;
            if (GroundItem) animator.Play("Pick_Down");
            else animator.Play("Pick_Up");
            this.gameObject.SetActive(false);
        }
    }
}
