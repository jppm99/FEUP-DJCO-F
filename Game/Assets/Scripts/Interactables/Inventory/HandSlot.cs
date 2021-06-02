using UnityEngine;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour
{
    private string item = "";
    private Sprite sprite;
    private GameObject player;
    private GameObject canvas;

    public Image icon;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
    }

    public void AddItem(string item, Sprite sprite) 
    {
        this.item = item;
        this.sprite = sprite;

        icon.sprite = this.sprite;
        icon.color = new Color(1,1,1,1);

        if (this.item == "buildableGeneratorItem")
            player.GetComponent<PlayerHand>().UpdateHandItem("hammer");
        else
            player.GetComponent<PlayerHand>().UpdateHandItem(this.item);
    }

    public string GetItem() 
    {
        return item;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void ResetSlot()
    {
        item = "";

        icon.color = new Color(0,0,0,0);
    }

    public void RemoveItem()
    {
        canvas.GetComponent<InventoryUI>().AddItem(item, sprite);

        ResetSlot();
    }
}
