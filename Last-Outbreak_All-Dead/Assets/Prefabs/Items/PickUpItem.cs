using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    [Header("Controller")]
    public bool GroundItem;
    private bool CanInteract;

    [Header("Data")]
    public string ItemName;
    public Sprite ItemIcon;

    [Header("References")]
    private PlayerMovement PM;
    private Animator animator;
    private Inventory inventory;

    [Space(10)]
    public UnityEvent InteractionEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
        inventory = FindObjectOfType<Inventory>();
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

            if (!inventory.ItemAdded(ItemName))
                InteractionEvent.Invoke();
            else
                this.gameObject.SetActive(false);

            inventory.AddItem(ItemName, ItemIcon);
        }
    }
}
