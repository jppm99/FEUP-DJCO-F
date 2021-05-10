using System.IO;
using UnityEngine;
using SPStudios.Tools;
 
using MessageType = SPStudios.Tools.UnityMessageForwarder.MessageType;

public class Inventory : ISingleton
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private string dataFile = "inventory_data.json";
    private string dataPath;
    private InventoryData inventoryData;
    private string lastItemPicked = "";

    public Inventory()
    {
        this.register();

        this.dataPath = Path.Combine(Application.persistentDataPath, this.dataFile);
        this.LoadData();

        // Getting Unity events from outside MonoBehaviours
        UnityMessageForwarder.AddListener(MessageType.OnApplicationQuit, this.WhenQuit);
    }

    private void LoadData()
    {
        try
        {
            string data = File.ReadAllText(dataPath);
            this.inventoryData = (JsonUtility.FromJson<InventoryData>(data));
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No inventory data found, first run?");
            this.inventoryData = new InventoryData();
        }
    }

    public string GetLastItemPicked()
    {
        return lastItemPicked;
    }

    public int GetCount(string item)
    {
        if (item == "qqlcoisa")
            return GetItemQqlCoisaCount();
        
        if (item == "stick")
            return GetStickCount();

        if (item == "rock")
            return GetRockCount();
        
        if (item == "metal")
            return GetMetalCount();

        return 0;
    }

    #region ItemQqlCoisa
    public int GetItemQqlCoisaCount()
    {
        return this.inventoryData.itemQqlCoisaCount;
    }

    public void SpendItemQqlCoisa(int count = -1)
    {
        this.inventoryData.itemQqlCoisaCount -= count;
    }

    public void AddItemQqlCoisa(int count = 1)
    {
        this.inventoryData.itemQqlCoisaCount += count;
        lastItemPicked = "qqlcoisa";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion

    #region STICK
    public int GetStickCount()
    {
        return this.inventoryData.Stick;
    }

    public void SpendStick(int count = -1)
    {
        this.inventoryData.Stick -= count;
    }

    public void AddStick(int count = 1)
    {
        this.inventoryData.Stick += count;
        lastItemPicked = "stick";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion
    #region ROCK
    public int GetRockCount()
    {
        return this.inventoryData.Rock;
    }

    public void SpendRock(int count = -1)
    {
        this.inventoryData.Rock -= count;
    }

    public void AddRock(int count = 1)
    {
        this.inventoryData.Rock += count;
        lastItemPicked = "rock";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion
    #region METAL
    public int GetMetalCount()
    {
        return this.inventoryData.Metal;
    }

    public void SpendMetal(int count = -1)
    {
        this.inventoryData.Metal -= count;
    }

    public void AddMetal(int count = 1)
    {
        this.inventoryData.Metal += count;
        lastItemPicked = "metal";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    #endregion

    void WhenQuit()
    {
        this.SaveData();
    }

    private void SaveData()
    {
        string data = JsonUtility.ToJson(this.inventoryData);

        File.WriteAllText(dataPath, data);

        Debug.Log("Inventory data saved to: " + dataPath);
    }

    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<Inventory>(this);
    }
}

[System.Serializable]
class InventoryData
{
    public int itemQqlCoisaCount, Stick, Rock, Metal;
}
