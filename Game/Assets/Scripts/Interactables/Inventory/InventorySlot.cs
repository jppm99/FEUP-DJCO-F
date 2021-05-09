using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string item = "";
    private int count;

    public Image icon;
    public Sprite sprite;
    public Text text;

    public void AddNewItem(string item) {
        this.item = item;
        count = 1;

        icon.sprite = sprite;
        icon.color = new Color(1,1,1,1);
        text.text = count.ToString();

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
