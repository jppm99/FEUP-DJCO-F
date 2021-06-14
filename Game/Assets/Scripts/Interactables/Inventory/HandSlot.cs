using UnityEngine;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour
{
    private string item = "";
    private Sprite sprite;
    private GameObject player;
    private GameObject canvas;

    public Image icon;
    public Image buttonImage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Menus Canvas");

        buttonImage.enabled = false;
    }

    public void AddItem(string item, Sprite sprite) 
    {
        this.item = item;
        this.sprite = sprite;

        icon.sprite = this.sprite;
        icon.color = new Color(1,1,1,1);
        buttonImage.enabled = true;

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
        buttonImage.enabled = false;

        player.GetComponent<PlayerHand>().UpdateHandItem("pata");
    }

    public void RemoveItem()
    {
        if (item != "") {
            canvas.GetComponent<InventoryUI>().AddItem(item, sprite);

            ResetSlot();
        }
    }
}
