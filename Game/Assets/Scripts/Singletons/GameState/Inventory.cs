using System.IO;
using UnityEngine;
using SPStudios.Tools;
 
using MessageType = SPStudios.Tools.UnityMessageForwarder.MessageType;

public class Inventory : ISingleton
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public delegate void OnLoadInventory();
    public OnLoadInventory onLoadInventoryCallback;

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

            if (onLoadInventoryCallback != null)
                onLoadInventoryCallback.Invoke();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No inventory data found, first run?");
            
            NewGame();
        }
    }

    public void NewGame()
    {
        this.inventoryData = new InventoryData();

        if (onLoadInventoryCallback != null)
            onLoadInventoryCallback.Invoke();
    }

    public string GetLastItemPicked()
    {
        return lastItemPicked;
    }

    public int GetCount(string item)
    {        
        if (item == "stick")
            return GetStickCount();

        if (item == "rock")
            return GetRockCount();
        
        if (item == "metal")
            return GetMetalCount();

        if (item == "meat")
            return GetMeatCount();

        if (item == "catana")
            return GetCatanaCount();

        if (item == "knife")
            return GetKnifeCount();

        if (item == "axe")
            return GetAxeCount();
        
        if (item == "monsterGeneratorItem" && GetMonsterGeneratorItem())
            return 1;

        if (item == "hiddenGeneratorItem" && GetHiddenGeneratorItem())
            return 1;

        if (item == "buildableGeneratorItem" && GetBuildableGeneratorItem())
            return 1;

        if (item == "diary" && GetDiary())
            return 1;

        return 0;
    }

    public void AddItem(string item)
    {        
        if (item == "stick")
            AddStick();

        if (item == "rock")
            AddRock();
        
        if (item == "metal")
            AddMetal();

        if (item == "meat")
            AddMeat();

        if (item == "catana")
            AddCatana();

        if (item == "knife")
            AddKnife();

        if (item == "axe")
            AddAxe();

        if (item == "monsterGeneratorItem")
            AddMonsterGeneratorItem();

        if (item == "hiddenGeneratorItem")
            AddHiddenGeneratorItem();

        if (item == "buildableGeneratorItem")
            AddBuildableGeneratorItem();

        if (item == "diary")
            AddDiary();
    }

    public int SpendItem(string item, int count = 1)
    {        
        if (item == "stick")
            return SpendStick(count);

        if (item == "rock")
            return SpendRock(count);
        
        if (item == "metal")
            return SpendMetal(count);

        if (item == "meat")
            return SpendMeat(count);

        if (item == "catana")
            return SpendCatana(count);

        if (item == "knife")
            return SpendKnife(count);

        if (item == "axe")
            return SpendAxe(count);

        if (item == "monsterGeneratorItem") {
            SpendMonsterGeneratorItem();

            return 0;
        }

        if (item == "hiddenGeneratorItem") {
            SpendHiddenGeneratorItem();

            return 0;
        }

        if (item == "buildableGeneratorItem") {
            SpendBuildableGeneratorItem();

            return 0;
        }

        if (item == "diary") {
            SpendDiary();

            return 0;
        }

        return 0;
    }

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

    #region MEAT
    public int GetMeatCount()
    {
        return this.inventoryData.Meat;
    }

    public int SpendMeat(int count = 1)
    {
        this.inventoryData.Meat -= count;
        return this.inventoryData.Meat;
    }

    public void AddMeat(int count = 1)
    {
        this.inventoryData.Meat += count;
        lastItemPicked = "meat";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetMeat(int count)
    {
        this.inventoryData.Meat = count;
    }
    #endregion

    #region CATANA
    public int GetCatanaCount()
    {
        return this.inventoryData.Catana;
    }

    public int SpendCatana(int count = 1)
    {
        this.inventoryData.Catana -= count;
        return this.inventoryData.Catana;
    }

    public void AddCatana(int count = 1)
    {
        this.inventoryData.Catana += count;
        lastItemPicked = "catana";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetCatana(int count)
    {
        this.inventoryData.Catana = count;
    }
    #endregion

    #region KNIFE
    public int GetKnifeCount()
    {
        return this.inventoryData.Knife;
    }

    public int SpendKnife(int count = 1)
    {
        this.inventoryData.Knife -= count;
        return this.inventoryData.Knife;
    }

    public void AddKnife(int count = 1)
    {
        this.inventoryData.Knife += count;
        lastItemPicked = "knife";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetKnife(int count)
    {
        this.inventoryData.Knife = count;
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

    #region GENERATOR_ITEMS
    #region MONSTER
    public bool GetMonsterGeneratorItem()
    {
        return this.inventoryData.hasMonsterGeneratorItem;
    }

    public void SpendMonsterGeneratorItem()
    {
        this.inventoryData.hasMonsterGeneratorItem = false;
    }

    public void AddMonsterGeneratorItem()
    {
        this.inventoryData.hasMonsterGeneratorItem = true;
        lastItemPicked = "monsterGeneratorItem";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetMonsterGeneratorItem(bool hasItem)
    {
        this.inventoryData.hasMonsterGeneratorItem = hasItem;
    }
    #endregion
    #region HIDDEN
    public bool GetHiddenGeneratorItem()
    {
        return this.inventoryData.hasHiddenGeneratorItem;
    }

    public void SpendHiddenGeneratorItem()
    {
        this.inventoryData.hasHiddenGeneratorItem = false;
    }

    public void AddHiddenGeneratorItem()
    {
        this.inventoryData.hasHiddenGeneratorItem = true;
        lastItemPicked = "hiddenGeneratorItem";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetHiddenGeneratorItem(bool hasItem)
    {
        this.inventoryData.hasHiddenGeneratorItem = hasItem;
    }
    #endregion
    #region BUILDABLE
    public bool GetBuildableGeneratorItem()
    {
        return this.inventoryData.hasBuildableGeneratorItem;
    }

    public void SpendBuildableGeneratorItem()
    {
        this.inventoryData.hasBuildableGeneratorItem = false;
    }

    public void AddBuildableGeneratorItem()
    {
        this.inventoryData.hasBuildableGeneratorItem = true;
        lastItemPicked = "buildableGeneratorItem";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetBuildableGeneratorItem(bool hasItem)
    {
        this.inventoryData.hasBuildableGeneratorItem = hasItem;
    }
    #endregion
    #endregion

    #region DIARY
    public bool GetDiary()
    {
        return this.inventoryData.hasDiary;
    }

    public void SpendDiary()
    {
        this.inventoryData.hasDiary = false;
    }

    public void AddDiary()
    {
        this.inventoryData.hasDiary = true;
        lastItemPicked = "diary";

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SetDiary(bool hasItem)
    {
        this.inventoryData.hasDiary = hasItem;
    }
    #endregion

    void WhenQuit()
    {
        if(RuntimeStuff.SaveOnQuit()) this.SaveData();
    }

    public void SaveData()
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
    public int Stick, Rock, Metal, Meat, Catana, Knife, Axe;
    public bool hasMonsterGeneratorItem, hasHiddenGeneratorItem, hasBuildableGeneratorItem, hasDiary;
}
