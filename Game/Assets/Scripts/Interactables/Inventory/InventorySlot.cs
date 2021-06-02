using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string item = "";
    private Sprite sprite;
    private GameObject canvas;
    private GameObject player;

    public Image icon;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
    }

    public void AddNewItem(string item, Sprite sprite, int count = 1) 
    {
        this.item = item;
        this.sprite = sprite;

        icon.sprite = this.sprite;
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

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void UseItem()
    {
        if (item == "meat") {
            canvas.GetComponent<InventoryUI>().RemoveItem(this);
            player.GetComponent<PlayerLife>().IncreaseHealth(10);
        }
        else if (item == "diary")
            player.GetComponent<UseDiary>().openDiary();
        else if (item == "buildableGeneratorItem" || item == "catana" || item == "knife" || item == "axe") {
            canvas.GetComponent<InventoryUI>().Equip(item, sprite);
            ResetSlot();
            canvas.GetComponent<InventoryUI>().UpdateSlotsOrder();
        }
    }

    public void Select()
    {
        canvas.GetComponent<InventoryUI>().ChangeSelected(this);
    }

    public void ResetSlot()
    {
        item = "";

        icon.color = new Color(0,0,0,0);
        text.text = "";
    }
}
