using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
        inventory.SetItemQqlCoisa(10);
        inventory.SetStick(10);
        inventory.SetRock(10);
        inventory.SetMetal(10);
        inventory.SetFood(10);
        inventory.SetAxe(0);
        inventory.SetSword(0);

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

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateText("stick", inventory.GetStickCount());
            buildSlots[i].UpdateText("rock", inventory.GetRockCount());
            buildSlots[i].UpdateText("metal", inventory.GetMetalCount());
        }
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

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateText(item, inventory.GetCount(item));
        }
    }

    // Called when an item is removed
    public void RemoveItem(InventorySlot slot, int count = 1)
    {
        string item = slot.GetItem();

        if (inventory.SpendItem(item, count) == 0) {
            slot.ResetSlot();
            UpdateSlotsOrder();
        }
        else
            slot.SetCount(inventory.GetCount(item));

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateText(item, inventory.GetCount(item));
        }
    }

    public void RemoveItem(string item, int count)
    {
        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].Used())
                continue;

            if (slots[i].GetItem() == item)
                RemoveItem(slots[i], count);
        }
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

    // BuildItem is called when building an item
    public void BuildItem(BuildSlot slot)
    {
        bool canBuild = true;
        Dictionary<string, int> requirements = slot.GetRequirements();

        for (int i = 0; i < requirements.Count; i++) {
            if (inventory.GetCount(requirements.ElementAt(i).Key) < requirements.ElementAt(i).Value) {
                canBuild = false;
                break;
            }
        }

        if (canBuild) {
            for (int i = 0; i < requirements.Count; i++) {
                RemoveItem(requirements.ElementAt(i).Key, requirements.ElementAt(i).Value);
            }

            inventory.AddItem(slot.GetItem());
        }
    }
}
