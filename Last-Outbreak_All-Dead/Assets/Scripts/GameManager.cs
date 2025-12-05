using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Variables
    private int UIOpened;

    [Header("References")]
    public GameObject inventoryUI;
    public UnityEvent OpenInventoryEvent;
    public UnityEvent CloseInventoryEvent;
    private PlayerMovement pm;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
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
            if (inventoryUI.activeSelf == false)
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
                pm.UnfreezePlayer();
            }
        }
    }
    #endregion

    #region Cursor Handler
    public void ToggleCursor(bool toggle)
    {
        if (toggle)
        {
            Cursor.visible = toggle;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = toggle;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    #endregion

    #region Time Scale
    public void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
    #endregion
}
