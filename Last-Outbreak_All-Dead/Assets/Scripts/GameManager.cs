using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    private int UIOpened;
    public GameObject InventoryUI;
    public Animator canvasAnimator;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIOpened == 0 || UIOpened == 2)
            InventoryHandle();
    }

    #region Inventory Handle
    void InventoryHandle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryUI.activeSelf == false)
            {
                UIOpened = 2;
                canvasAnimator.speed = 1000f;
                canvasAnimator.Play("Open_Inventory");
                Time.timeScale = 0.001f;
            }
            else
            {
                Time.timeScale = 1f;
                canvasAnimator.speed = 1f;
                canvasAnimator.Play("Close_Inventory");
                UIOpened = 0;
            }
        }
    }
    #endregion
}
