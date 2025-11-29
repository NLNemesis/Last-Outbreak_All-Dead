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
    private bool CombinationOn;
    private int FirstSlot;
    private int SecondSlot;

    [Header("References")]
    private PlayerMovement PM;
    public GameObject SelectionPanel;

    [Header("Mixed Items")]
    public string[] MixedItemName;
    public Sprite[] MixedItemIcon;

    private List<string> AddedItem = new List<string>();
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
                AddedItem.Add(name);
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

    #region Select Slot Handler
    public void SelectSlot(int slotID)
    {
        if (!CombinationOn)
        {
            FirstSlot = slotID;
            SelectionPanel.SetActive(true);
        }
        else
        {
            SecondSlot = slotID;
            Combine();
            SelectionPanel.SetActive(false);
        }
    }
    #endregion

    #region Use Handler
    public void UseSlot()
    {
        if (SlotName[FirstSlot] == "First_Aid")
        {
            RemoveSpecificSlot(FirstSlot);
            //Heal Player
        }
        else if (SlotName[FirstSlot] == "Green_Herb")
        {
            RemoveSpecificSlot(FirstSlot);
            //Heal Player
        }

        SelectionPanel.SetActive(false);
    }
    #endregion

    #region Examine Handler
    public void ExamineSlot()
    {
        for (int i = 0; i < ExamineObjectUI.Length; i++)
        {
            if (ExamineObjectUI[i].name == SlotName[FirstSlot])
            {
                ExamineObjectUI[i].SetActive(true);
                break;
            }
        }
        SelectionPanel.SetActive(false);
    }
    #endregion

    #region Combine Handler
    public void ToggleCombination()
    {
        CombinationOn = true;
    }

    void Combine()
    {
        if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Blue_Herb") 
        || (SlotName[FirstSlot] == "Blue_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(MixedItemName[0], MixedItemIcon[0]);
        }
        else if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Red_Herb")
        || (SlotName[FirstSlot] == "Red_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(MixedItemName[1], MixedItemIcon[1]);
        }
        else if ((SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Green_Herb")
        || (SlotName[FirstSlot] == "Green_Herb" && SlotName[SecondSlot] == "Green_Herb"))
        {
            RemoveSpecificSlot(FirstSlot);
            RemoveSpecificSlot(SecondSlot);
            AddItem(MixedItemName[1], MixedItemIcon[1]);
        }

        CombinationOn = false;
        SelectionPanel.SetActive(false);
    }
    #endregion

    #region Trash Handler
    public void TrashSlot()
    {
        RemoveSpecificSlot(FirstSlot);
    }
    #endregion

    #region Check Added Item
    public bool ItemAdded(string name)
    {
        bool Check = false;
        for (int i = 0; i < AddedItem.Count; i++)
        {
            if (name == AddedItem[i])
            {
                Check = true;
                break;
            }
        }
        return Check;
    }
    #endregion
}
