using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Variables
    private int UIOpened;

    [Header("References")]
    public GameObject InventoryUI;
    public UnityEvent OpenInventoryEvent;
    public UnityEvent CloseInventoryEvent;
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
                Time.timeScale = 0.001f;
                OpenInventoryEvent.Invoke();
            }
            else
            {
                UIOpened = 0;
                Time.timeScale = 1;
                CloseInventoryEvent.Invoke();
            }
        }
    }
    #endregion
}
