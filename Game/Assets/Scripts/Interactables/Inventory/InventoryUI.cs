using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    private bool inventoryEnabled;
    private Inventory inventory;
    private InventorySlot[] slots;
    private BuildSlot[] buildSlots;
    private Dictionary<string, Sprite> sprites;
    private InventorySlot selectedItem;

    public GameObject inventoryUI;
    public GameObject handSlot;

    // Sprites
    public Sprite stickIcon;
    public Sprite rockIcon;
    public Sprite metalIcon;
    public Sprite meatIcon;
    public Sprite catanaIcon;
    public Sprite knifeIcon;
    public Sprite axeIcon;
    public Sprite monsterGeneratorItemIcon;
    public Sprite hiddenGeneratorItemIcon;
    public Sprite buildableGeneratorItemIcon;
    public Sprite diaryIcon;


    // Start is called before the first frame update
    void Start()
    {
        inventoryEnabled = false;

        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        
        inventory.onItemChangedCallback = UpdateUI;
        inventory.onLoadInventoryCallback = LoadInventory;

        sprites = new Dictionary<string, Sprite>();
        sprites.Add("stick", stickIcon);
        sprites.Add("rock", rockIcon);
        sprites.Add("metal", metalIcon);
        sprites.Add("meat", meatIcon);
        sprites.Add("catana", catanaIcon);
        sprites.Add("knife", knifeIcon);
        sprites.Add("axe", axeIcon);
        sprites.Add("monsterGeneratorItem", monsterGeneratorItemIcon);
        sprites.Add("hiddenGeneratorItem", hiddenGeneratorItemIcon);
        sprites.Add("buildableGeneratorItem", buildableGeneratorItemIcon);
        sprites.Add("diary", diaryIcon);
    }

    // Update is called once per frame
    void Update()
    {
        bool oldInventoryState = this.inventoryEnabled;
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryEnabled = !inventoryEnabled;
        }

        if (oldInventoryState != inventoryEnabled && inventoryEnabled) {
            Time.timeScale = 0;
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (oldInventoryState != inventoryEnabled && !inventoryEnabled) {
            Time.timeScale = 1;
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // LoadInventory is called to load a previous saved inventory
    void LoadInventory()
    {
        inventoryEnabled = false;

        slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
        buildSlots = inventoryUI.GetComponentsInChildren<BuildSlot>();

        int currentSlot = 0;
        int count = 0;

        // Only for testing
        // inventory.SetStick(20);
        // inventory.SetRock(20);
        // inventory.SetMetal(20);
        // inventory.SetMeat(20);
        // inventory.SetCatana(0);
        // inventory.SetKnife(0);
        // inventory.SetAxe(0);
        // inventory.SetMonsterGeneratorItem(true);
        // inventory.SetHiddenGeneratorItem(true);
        // inventory.SetBuildableGeneratorItem(true);
        // inventory.SetDiary(true);

        if (inventory.GetStickCount() > 0) {
            slots[currentSlot].AddNewItem("stick", stickIcon, inventory.GetStickCount());
            currentSlot++;
        }

        if (inventory.GetRockCount() > 0) {
            slots[currentSlot].AddNewItem("rock", rockIcon, inventory.GetRockCount());
            currentSlot++;
        }

        if (inventory.GetMetalCount() > 0) {
            slots[currentSlot].AddNewItem("metal", metalIcon, inventory.GetMetalCount());
            currentSlot++;
        }

        if (inventory.GetMeatCount() > 0) {
            slots[currentSlot].AddNewItem("meat", meatIcon, inventory.GetMeatCount());
            currentSlot++;
        }

        if (inventory.GetCatanaCount() > 0) {
            slots[currentSlot].AddNewItem("catana", catanaIcon, inventory.GetCatanaCount());
            currentSlot++;
        }

        if (inventory.GetKnifeCount() > 0) {
            slots[currentSlot].AddNewItem("knife", knifeIcon, inventory.GetKnifeCount());
            currentSlot++;
        }

        if (inventory.GetAxeCount() > 0) {
            slots[currentSlot].AddNewItem("axe", axeIcon, inventory.GetAxeCount());
            currentSlot++;
        }

        if (inventory.GetMonsterGeneratorItem()) {
            slots[currentSlot].AddNewItem("monsterGeneratorItem", monsterGeneratorItemIcon);
            currentSlot++;
        }

        if (inventory.GetHiddenGeneratorItem()) {
            slots[currentSlot].AddNewItem("hiddenGeneratorItem", hiddenGeneratorItemIcon);
            currentSlot++;
        }

        if (inventory.GetBuildableGeneratorItem()) {
            slots[currentSlot].AddNewItem("buildableGeneratorItem", buildableGeneratorItemIcon);
            currentSlot++;
        }

        if (inventory.GetDiary()) {
            slots[currentSlot].AddNewItem("diary", diaryIcon);
            currentSlot++;
        }

        LoadBuilds();
    }

    // LoadBuilds is called to load all items available to build
    void LoadBuilds()
    {
        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].SetRequirements();
            buildSlots[i].UpdateRequirements("stick", inventory.GetStickCount());
            buildSlots[i].UpdateRequirements("rock", inventory.GetRockCount());
            buildSlots[i].UpdateRequirements("metal", inventory.GetMetalCount());
            buildSlots[i].UpdateBuilds("catana", inventory.GetCatanaCount());
            buildSlots[i].UpdateBuilds("axe", inventory.GetAxeCount());
            buildSlots[i].UpdateBuilds("knife", inventory.GetKnifeCount());
            buildSlots[i].UpdateBuilds("buildableGeneratorItem", inventory.GetCount("buildableGeneratorItem"));
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

                slots[i].AddNewItem(item, sprites[item]);

                break;
            }
        }

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateRequirements(item, inventory.GetCount(item));
        }
    }

    // Called when an item is removed
    public void RemoveItem(InventorySlot slot, int count = 1)
    {
        string item = slot.GetItem();

        if (inventory.SpendItem(item, count) <= 0) {
            slot.ResetSlot();
            UpdateSlotsOrder();
        }
        else
            slot.SetCount(inventory.GetCount(item));

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateRequirements(item, inventory.GetCount(item));
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
    public void UpdateSlotsOrder()
    {
        int emptySlotIndex = -1;

        for (int i = 0; i < slots.Length; i++) {
            if (emptySlotIndex == -1) {
                if (!slots[i].Used())
                    emptySlotIndex = i;
                
                continue;
            }

            if (slots[i].Used()) {
                slots[emptySlotIndex].AddNewItem(slots[i].GetItem(), slots[i].GetSprite(), inventory.GetCount(slots[i].GetItem()));
                slots[i].ResetSlot();

                i = emptySlotIndex;
                emptySlotIndex = -1;
            }
        }
    }

    // BuildItem is called when building an item
    public void BuildItem(string item)
    {
        inventory.AddItem(item);
    }

    public void ChangeSelected(InventorySlot slot)
    {
        if (selectedItem != null)
            this.selectedItem.DeSelect();
        
        this.selectedItem = slot;
    }

    public void Use()
    {
        if (this.selectedItem != null) {
            this.selectedItem.UseItem();
            this.selectedItem = null;
        }
    }

    public void Equip(string item, Sprite sprite)
    {
        this.handSlot.GetComponent<HandSlot>().AddItem(item, sprite);
    }

    public void AddItem(string item, Sprite sprite)
    {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].Used())
                continue;

            slots[i].AddNewItem(item, sprite);

            break;
        }

        for (int i = 0; i < buildSlots.Length; i++) {
            buildSlots[i].UpdateRequirements(item, inventory.GetCount(item));
        }
    }

    public void Enequip()
    {
        this.handSlot.GetComponent<HandSlot>().RemoveItem();   
    }

    // Close is called when clicking the close button
    public void Close()
    {
        inventoryEnabled = !inventoryEnabled;
        
        Time.timeScale = 1;
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
