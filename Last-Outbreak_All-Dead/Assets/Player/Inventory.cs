using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Variables
    [Header("Holder")]
    public Image[] SlotImage;
    public string[] SlotName;
    private int SlotAvailable;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SlotAvailable = SlotImage.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Add/Remove
    public void AddItem(string name, Sprite icon)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotName[i] == "Empty")
            {
                SlotName[i] = name;
                //SlotImage[i].sprite = icon;
                //SlotImage[i].color = new Color(255, 255, 255, 255);
                SlotAvailable--;
                break;
            }
        }
    }

    public void RemoveItem(string name)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotName[i] == name)
            {
                SlotName[i] = "Empty";
                SlotImage[i].color = new Color(255, 255, 255, 0);
                SlotAvailable++;
                break;
            }
        }
    }

    public void RemoveSpecificSlot(int slotID)
    {
        SlotName[slotID] = "Empty";
        SlotImage[slotID].color = new Color(255, 255, 255, 0);
        SlotAvailable++;
    }
    #endregion
}
