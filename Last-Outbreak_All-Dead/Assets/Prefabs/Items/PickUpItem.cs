using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : MonoBehaviour
{
    #region Variables
    public enum ItemType {Health,Spray,Ammo}
    public enum AnimationType {Up, Ground}
    [Header("Item Data")]
    public ItemType Item;
    public AnimationType Interacted_Anim;
    [Space(10)]
    public GameObject[] ItemObject;

    [Header("Interaction")]
    public KeyCode Interaction;
    private bool CanInteract;

    [Header("References")]
    private PlayerMovement PM;
    #endregion

    #region OnTriggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            CanInteract = true;
            PM = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            CanInteract = false;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Items Appears
        for (int i = 0; i < ItemObject.Length; i++)
            ItemObject[i].SetActive(false);

        for (int i = 0; i < ItemObject.Length; i++)
        {
            if (ItemObject[i].name == Item.ToString())
            {
                ItemObject[i].SetActive(true);
                break;
            }
            else if (ItemObject[i].name == Item.ToString())
            {
                ItemObject[i].SetActive(true);
                break;
            }
            if (ItemObject[i].name == Item.ToString())
            {
                ItemObject[i].SetActive(true);
                break;
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Interaction) && CanInteract)
        {
            CanInteract = false;
            PM.freeze = true;
            if (Interacted_Anim == AnimationType.Up) PM.animator.Play("Pick_Up");
            else PM.animator.Play("Pick_Up_Ground");
            this.gameObject.SetActive(false);
        }
    }
}
