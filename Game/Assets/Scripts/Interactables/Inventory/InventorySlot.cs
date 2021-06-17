using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private string item = "";
    private Sprite sprite;
    private bool selected;
    private GameObject canvas;
    private GameObject player;

    public Button button;
    public Image icon;
    public Image redCircle;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Menus Canvas");
        player = GameObject.Find("Player");

        selected = false;
    }

    public void AddNewItem(string item, Sprite sprite, int count = 1) 
    {
        this.item = item;
        this.sprite = sprite;

        icon.sprite = this.sprite;
        icon.color = new Color(1,1,1,1);
        redCircle.color = new Color(1,1,1,0.78f);
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
        if (item == "meat" && player.GetComponent<PlayerLife>().GetHealth() < 50) {
            canvas.GetComponent<InventoryUI>().RemoveItem(this);
            player.GetComponent<PlayerLife>().IncreaseHealth(10);
        }
        else if (item == "diary")
            player.GetComponent<UseDiary>().openDiary();
        else if (item == "buildableGeneratorItem" || item == "catana" || item == "knife" || item == "axe") {
            canvas.GetComponent<InventoryUI>().Enequip();
            canvas.GetComponent<InventoryUI>().Equip(item, sprite);
            ResetSlot();
            canvas.GetComponent<InventoryUI>().UpdateSlotsOrder();
        }

        DeSelect();
    }

    public void Select()
    {
        if (Used()) {
            if (!selected) {
                selected = true;
                canvas.GetComponent<InventoryUI>().ChangeSelected(this);
                button.Select();
            }
            else {
                canvas.GetComponent<InventoryUI>().ChangeSelected(null);
                DeSelect();
            }
        }
    }

    public void DeSelect()
    {
        selected = false;
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResetSlot()
    {
        item = "";

        icon.color = new Color(0,0,0,0);
        redCircle.color = new Color(0,0,0,0);
        text.text = "";
    }
}
