using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    private bool inventoryEnabled;
    private Inventory inventory;
    private InventorySlot[] slots;
    private BuildSlot[] buildSlots;

    public Transform itemsParent;
    public Transform buildsParent;
    public GameObject inventoryUI;
    public GameObject build;


    // Start is called before the first frame update
    void Start()
    {
        inventoryEnabled = false;

        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        buildSlots = buildsParent.GetComponentsInChildren<BuildSlot>();
        Debug.Log(buildSlots.Length);
        
        inventory.onItemChangedCallback += UpdateUI;

        LoadInventory();
        LoadBuilds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled) {
            inventoryUI.SetActive(true);
            build.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else {
            inventoryUI.SetActive(false);
            build.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // LoadInventory is called to load a previous saved inventory
    void LoadInventory()
    {
        int currentSlot = 0;
        int count = 0;

        // Only for testing
        inventory.AddItemQqlCoisa(10);
        inventory.AddStick(10);
        inventory.AddRock(10);
        inventory.AddMetal(10);
        inventory.AddFood(10);
        inventory.AddAxe(1);
        inventory.AddSword(1);

        if (inventory.GetItemQqlCoisaCount() > 0) {
            slots[currentSlot].AddNewItem("qqlcoisa", inventory.GetItemQqlCoisaCount());
            currentSlot++;
        }

        if (inventory.GetStickCount() > 0) {
            slots[currentSlot].AddNewItem("stick", inventory.GetStickCount());
            currentSlot++;
        }

        if (inventory.GetRockCount() > 0) {
            slots[currentSlot].AddNewItem("rock", inventory.GetRockCount());
            currentSlot++;
        }

        if (inventory.GetMetalCount() > 0) {
            slots[currentSlot].AddNewItem("metal", inventory.GetMetalCount());
            currentSlot++;
        }

        if (inventory.GetFoodCount() > 0) {
            slots[currentSlot].AddNewItem("food", inventory.GetFoodCount());
            currentSlot++;
        }

        if (inventory.GetAxeCount() > 0) {
            slots[currentSlot].AddNewItem("axe", inventory.GetAxeCount());
            currentSlot++;
        }

        if (inventory.GetSwordCount() > 0) {
            slots[currentSlot].AddNewItem("sword", inventory.GetSwordCount());
            currentSlot++;
        }
    }

    // LoadBuilds is called to load all items available to build
    void LoadBuilds()
    {
        Dictionary<string, int> axeRequirements = new Dictionary<string, int>();
        axeRequirements.Add("stick", 4);
        axeRequirements.Add("rock", 2);

        buildSlots[0].SetItem("axe", axeRequirements);

        Dictionary<string, int> swordRequirements = new Dictionary<string, int>();
        swordRequirements.Add("stick", 3);
        swordRequirements.Add("rock", 1);
        swordRequirements.Add("metal", 5);

        buildSlots[1].SetItem("sword", swordRequirements);
    }

    // UpdateUI is called everytime a item is picked
    void UpdateUI() 
    {
        bool updated = false;
        string item = inventory.GetLastItemPicked();

        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].Used())
                break;

            if (slots[i].GetItem() == item) {
                slots[i].SetCount(inventory.GetCount(item));
                updated = true;
            }
        }

        if (!updated) {
            for (int i = 0; i < slots.Length; i++) {
                if (slots[i].Used())
                    continue;

                slots[i].AddNewItem(item);

                break;
            }
        }
    }

    // Called when an item is removed
    public void RemoveItem(InventorySlot slot)
    {
        if (inventory.SpendItem(slot.GetItem()) == 0) {
            slot.ResetSlot();
            UpdateSlotsOrder();
        }
        else
            slot.SetCount(inventory.GetCount(slot.GetItem()));
    }

    // Called after a slot becomes empty
    private void UpdateSlotsOrder()
    {
        int emptySlotIndex = -1;

        for (int i = 0; i < slots.Length; i++) {
            if (emptySlotIndex == -1) {
                if (!slots[i].Used())
                    emptySlotIndex = i;
                
                continue;
            }

            if (slots[i].Used()) {
                slots[emptySlotIndex].AddNewItem(slots[i].GetItem(), inventory.GetCount(slots[i].GetItem()));
                slots[i].ResetSlot();

                i = emptySlotIndex;
                emptySlotIndex = -1;
            }
        }
    }
}
