using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public GameObject InventoryObject;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInventory();
    }

    void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && !InventoryObject.activeSelf)
        {
            InventoryObject.SetActive(true);
            Time.timeScale = 0;
        }

        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape) && InventoryObject.activeSelf))
        {
            InventoryObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
