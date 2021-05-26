using UnityEngine;
using UnityEngine.UI;

public class RequirementSlot : MonoBehaviour
{
    public string item;
    public int quantity;
    public Text text;

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
            text.color = new Color(0.76f ,0.02f ,0 ,1);
        }
        else {
            satisfied = true;
            text.color = new Color(0, 0.45f, 0.02f, 1);
        }
    }

    public bool GetSatisfied()
    {
        return satisfied;
    }
}
