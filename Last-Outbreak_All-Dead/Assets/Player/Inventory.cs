using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Variables
    [Header("Holder")]
    public Image[] SlotImage;
    public string[] SlotName;
    public Button[] SlotButton;
    private int SlotAvailable;

    [Header("Selection Panel")]
    public GameObject[] ExamineObjectUI;
    private int FirstSlot;
    private int SecondSlot;
    private bool CombinationOn;

    [Header("References")]
    private PlayerMovement PM;
    public GameObject SelectionPanel;

    [Header("Combined Items")]
    public string[] CombinedItemName;
    public Sprite[] CombinedItemIcon;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
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
                SlotImage[i].sprite = icon;
                SlotImage[i].color = new Color(255, 255, 255, 255);
                SlotButton[i].enabled = true;
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
                SlotButton[i].enabled = false;
                SlotAvailable++;
                break;
            }
        }
    }    

    public void RemoveSpecificSlot(int slotID)
    {
        SlotName[slotID] = "Empty";
        SlotImage[slotID].color = new Color(255, 255, 255, 0);
        SlotButton[slotID].enabled = false;
        SlotAvailable++;
    }
    #endregion

    #region Selection Panel
    public void SelectSlot(int slotID)
    {
        if (!CombinationOn)
            FirstSlot = slotID;
        else
        {
            SecondSlot = slotID;
            CombineItem();
        }
    }

    public void UseItem()
    {
        if (SlotName[FirstSlot] == "First_Aid")
        {
            //Heal Player
            RemoveSpecificSlot(FirstSlot);
        }
        else if (SlotName[FirstSlot] == "Green_Herb")
        {
            //Heal Player
            RemoveSpecificSlot(FirstSlot);
        }
    }

    public void ExamineItem()
    {
        for (int i = 0; i < ExamineObjectUI.Length; i++)
        {
            if (SlotName[FirstSlot] ==  ExamineObjectUI[i].name)
                ExamineObjectUI[i].SetActive(true);
            else
                ExamineObjectUI[i].SetActive(false);
        }
    }

    public void ToggleCombination()
    {
        CombinationOn = true;
    }

    public void CombineItem()
    {
        if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Blue_Herb")
          ||(SlotName[FirstSlot] == "Blue_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(CombinedItemName[0], CombinedItemIcon[0]);
            CombinationOn = false;
            SelectionPanel.SetActive(false);
        }
        else if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Red_Herb")
          || (SlotName[FirstSlot] == "Red_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(CombinedItemName[1], CombinedItemIcon[1]);
            CombinationOn = false;
            SelectionPanel.SetActive(false);
        }
        else if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Green_Herb")
           || (SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(CombinedItemName[2], CombinedItemIcon[2]);
            CombinationOn = false;
            SelectionPanel.SetActive(false);
        }
    }

    public void TrashItem()
    {
        RemoveSpecificSlot(FirstSlot);
    }
    #endregion
}
