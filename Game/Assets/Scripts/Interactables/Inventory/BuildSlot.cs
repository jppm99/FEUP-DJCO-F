using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class BuildSlot : MonoBehaviour
{
    private string item = "";
    private Dictionary<string, int> requirements;
    private GameObject canvas;
    private Text[] lines;

    public Image icon;
    public Sprite sprite;
    
    public Text line1;
    public Text line2;
    public Text line3;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        lines = new Text[] {line1, line2, line3};
    }

    public void SetItem(string item, Dictionary<string, int> requirements)
    {
        this.item = item;
        this.requirements = requirements;

        icon.sprite = sprite;

        for (int i = 0; i < this.requirements.Count; i++) {
            lines[i].text = this.requirements.ElementAt(i).Key + " x" + this.requirements.ElementAt(i).Value.ToString();
        }
    }

    public void UpdateText(string item, int count)
    {
        for (int i = 0; i < requirements.Count; i++) {
            if (requirements.ElementAt(i).Key != item)
                continue;

            if (count >= requirements.ElementAt(i).Value)
                lines[i].color = new Color(0, 0.45f, 0.02f, 1);
            else
                lines[i].color = new Color(0.76f ,0.02f ,0 ,1);
        }
    }

    public string GetItem()
    {
        return item;
    }

    public Dictionary<string, int> GetRequirements()
    {
        return requirements;
    }

    public void BuildItem()
    {
        canvas.GetComponent<InventoryUI>().BuildItem(this);
    }
}
