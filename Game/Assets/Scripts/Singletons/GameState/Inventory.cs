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

        if (item == "food")
            return GetFoodCount();

        if (item == "axe")
            return GetAxeCount();

        if (item == "sword")
            return GetSwordCount();

        return 0;
    }

    public void AddItem(string item)
    {
        if (item == "qqlcoisa")
            AddItemQqlCoisa();
        
        if (item == "stick")
            AddStick();

        if (item == "rock")
            AddRock();
        
        if (item == "metal")
            AddMetal();

        if (item == "food")
            AddFood();

        if (item == "axe")
            AddAxe();

        if (item == "sword")
            AddSword();
    }

    public int SpendItem(string item, int count = 1)
    {
        if (item == "qqlcoisa")
            return SpendItemQqlCoisa(count);
        
        if (item == "stick")
            return SpendStick(count);

        if (item == "rock")
            return SpendRock(count);
        
        if (item == "metal")
            return SpendMetal(count);

        if (item == "food")
            return SpendFood(count);

        if (item == "axe")
            return SpendAxe(count);

        if (item == "sword")
            return SpendSword(count);

        return 0;
    }

    #region ItemQqlCoisa
    public int GetItemQqlCoisaCount()
    {
        return this.inventoryData.itemQqlCoisaCount;
    }

    public int SpendItemQqlCoisa(int count = 1)
    {
        this.inventoryData.itemQqlCoisaCount -= count;
        return this.inventoryData.itemQqlCoisaCount;
    }

    public void AddItemQqlCoisa(int count = 1)
    {
        this.inventoryData.itemQqlCoisaCount += count;
        lastItemPicked = "qqlcoisa";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetItemQqlCoisa(int count)
    {
        this.inventoryData.itemQqlCoisaCount = count;
    }
    #endregion

    #region STICK
    public int GetStickCount()
    {
        return this.inventoryData.Stick;
    }

    public int SpendStick(int count = 1)
    {
        this.inventoryData.Stick -= count;
        return this.inventoryData.Stick;
    }

    public void AddStick(int count = 1)
    {
        this.inventoryData.Stick += count;
        lastItemPicked = "stick";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetStick(int count)
    {
        this.inventoryData.Stick = count;
    }
    #endregion
    #region ROCK
    public int GetRockCount()
    {
        return this.inventoryData.Rock;
    }

    public int SpendRock(int count = 1)
    {
        this.inventoryData.Rock -= count;
        return this.inventoryData.Rock;
    }

    public void AddRock(int count = 1)
    {
        this.inventoryData.Rock += count;
        lastItemPicked = "rock";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetRock(int count)
    {
        this.inventoryData.Rock = count;
    }
    #endregion

    #region METAL
    public int GetMetalCount()
    {
        return this.inventoryData.Metal;
    }

    public int SpendMetal(int count = 1)
    {
        this.inventoryData.Metal -= count;
        return this.inventoryData.Metal;
    }

    public void AddMetal(int count = 1)
    {
        this.inventoryData.Metal += count;
        lastItemPicked = "metal";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetMetal(int count)
    {
        this.inventoryData.Metal = count;
    }
    #endregion

    #region FOOD
    public int GetFoodCount()
    {
        return this.inventoryData.Food;
    }

    public int SpendFood(int count = 1)
    {
        this.inventoryData.Food -= count;
        return this.inventoryData.Food;
    }

    public void AddFood(int count = 1)
    {
        this.inventoryData.Food += count;
        lastItemPicked = "food";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetFood(int count)
    {
        this.inventoryData.Food = count;
    }
    #endregion

    #region AXE
    public int GetAxeCount()
    {
        return this.inventoryData.Axe;
    }

    public int SpendAxe(int count = 1)
    {
        this.inventoryData.Axe -= count;
        return this.inventoryData.Axe;
    }

    public void AddAxe(int count = 1)
    {
        this.inventoryData.Axe += count;
        lastItemPicked = "axe";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetAxe(int count)
    {
        this.inventoryData.Axe = count;
    }
    #endregion

    #region SWORD
    public int GetSwordCount()
    {
        return this.inventoryData.Sword;
    }

    public int SpendSword(int count = 1)
    {
        this.inventoryData.Sword -= count;
        return this.inventoryData.Sword;
    }

    public void AddSword(int count = 1)
    {
        this.inventoryData.Sword += count;
        lastItemPicked = "sword";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetSword(int count)
    {
        this.inventoryData.Sword = count;
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
    public int itemQqlCoisaCount, Stick, Rock, Metal, Food, Axe, Sword;
}
