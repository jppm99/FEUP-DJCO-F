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
    }

    public void SetCount(int count) {
        this.count = count;
        text.text = this.count.ToString();
    }

    public bool Used() 
    {
        return item != "";
    }

    public string GetItem() 
    {
        return item;
    }

    public void UseItem()
    {
        count--;

        if (count == 0) {
            ResetSlot();
        }
    }

    private void ResetSlot()
    {
        item = "";
        count = 0;

        icon.color = new Color(0,0,0,0);
        text.text = "";
    }
}
