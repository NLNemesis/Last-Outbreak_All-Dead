using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    public string SelectedItem;
    public GameObject[] ItemObject;
    public Sprite[] ItemIcon;
    private string name;
    private Sprite icon;

    [Header("Controller")]
    public bool GroundItem;
    private bool CanInteract;

    [Header("References")]
    private PlayerMovement PM;
    private Animator animator;
    private Inventory inventory;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
        inventory = FindObjectOfType<Inventory>();
        for (int i = 0; i < ItemObject.Length; i++)
        {
            if (ItemObject[i].name == SelectedItem)
            {
                name = SelectedItem;
                icon = ItemIcon[i];
                ItemObject[i].SetActive(true);
            }
            else
                ItemObject[i].SetActive(false);
        }
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
            inventory.AddItem(name, icon);
            CanInteract = false;
            if (GroundItem)
                animator.Play("Pick_Down");
            else
                animator.Play("Pick_Up");
            this.gameObject.SetActive(false);
        }
    }
}
