using UnityEngine;
using UnityEngine.UI;

public class RequirementSlot : MonoBehaviour
{
    public string item;
    public int quantity;
    public Text text;
    public Image circle;
    public Sprite redSprite;
    public Sprite greenSprite;

    private bool satisfied = false;


    // Start is called before the first frame update
    void Start()
    {
        text.text = quantity.ToString();
    }

    public void UpdateSatisfied(int count)
    {
        if (count < quantity) {
            satisfied = false;
            circle.sprite = redSprite;
        }
        else {
            satisfied = true;
            circle.sprite = greenSprite;
        }
    }

    public bool GetSatisfied()
    {
        return satisfied;
    }
}
