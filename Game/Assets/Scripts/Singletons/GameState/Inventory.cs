using System.IO;
using UnityEngine;
using SPStudios.Tools;
 
using MessageType = SPStudios.Tools.UnityMessageForwarder.MessageType;

public class Inventory : ISingleton
{
    private string dataFile = "inventory_data.json";
    private string dataPath;
    private InventoryData inventoryData;

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
