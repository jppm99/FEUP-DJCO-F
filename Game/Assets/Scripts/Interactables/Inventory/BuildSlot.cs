using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class BuildSlot : MonoBehaviour
{
    private string item = "";
    private Dictionary<string, int> requirements;
    private GameObject canvas;

    public Image icon;
    public Sprite sprite;
    
    public Text line1;
    public Text line2;
    public Text line3;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void SetItem(string item, Dictionary<string, int> requirements)
    {
        this.item = item;
        this.requirements = requirements;

        icon.sprite = sprite;

        line1.text = this.requirements.ElementAt(0).Key + " x" + this.requirements.ElementAt(0).Value.ToString();

        if (this.requirements.Count > 1)
            line2.text = this.requirements.ElementAt(1).Key + " x" + this.requirements.ElementAt(1).Value.ToString();
        
        if (this.requirements.Count > 2)
            line3.text = this.requirements.ElementAt(2).Key + " x" + this.requirements.ElementAt(2).Value.ToString();
    }

    // public string GetItem() 
    // {
    //     return item;
    // }

    // public void UseItem()
    // {
    //     if (item == "food") {
    //         canvas.GetComponent<InventoryUI>().RemoveItem(this);
    //         player.GetComponent<PlayerLife>().IncreaseHealth(10);
    //     }
    // }

    // public void ResetSlot()
    // {
    //     item = "";

    //     icon.color = new Color(0,0,0,0);
    //     text.text = "";
    // }
}
