using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI() {
        print("Updating UI");
    }
}
