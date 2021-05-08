using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private InventorySlot[] slots;

    public Transform itemsParent;


    // Start is called before the first frame update
    void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // UpdateUI is called everytime a item is picked
    void UpdateUI() 
    {
        string item = inventory.GetLastItemPicked();

        for (int i = 0; i < slots.Length; i++) {
            if (!slots[i].Used())
                break;

            if (slots[i].GetItem() == item)
                slots[i].SetCount(1);
        }

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].Used())
                continue;

            Debug.Log("using " + i);
            slots[i].AddNewItem(item);

            break;
        }
    }
}
