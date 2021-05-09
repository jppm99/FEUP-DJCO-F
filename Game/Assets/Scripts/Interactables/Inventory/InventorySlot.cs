using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string item = "";
    private int count;

    public Image icon;
    public Sprite sprite;

    public void AddNewItem(string item) {
        this.item = item;
        count = 1;

        icon.sprite = sprite;
        icon.color = new Color(1,1,1,1);

        Debug.Log("Added item: " + item);
    }

    public void SetCount(int count) {
        this.count = count;
    }

    public bool Used() {
        return item != "";
    }

    public string GetItem() {
        return item;
    }
}
