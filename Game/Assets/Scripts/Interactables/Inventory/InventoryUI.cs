using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool inventoryEnabled;
    private Inventory inventory;
    private InventorySlot[] slots;

    public Transform itemsParent;
    public GameObject inventoryUI;


    // Start is called before the first frame update
    void Start()
    {
        inventoryEnabled = false;

        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled) {
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else {
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
    }
}
