using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string item = "";

    public Image icon;
    public Sprite sprite;
    public Text text;
    public GameObject canvas;

    public void AddNewItem(string item, int count = 1) 
    {
        this.item = item;

        icon.sprite = sprite;
        icon.color = new Color(1,1,1,1);
        text.text = count.ToString();
    }

    public void SetCount(int count) 
    {
        text.text = count.ToString();
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
        if (Used()) {
            canvas.GetComponent<InventoryUI>().RemoveItem(this);
        }
    }

    public void ResetSlot()
    {
        item = "";

        icon.color = new Color(0,0,0,0);
        text.text = "";
    }
}
