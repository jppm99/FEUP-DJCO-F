using System.IO;
using UnityEngine;
using SPStudios.Tools;
 
using MessageType = SPStudios.Tools.UnityMessageForwarder.MessageType;

public class GameState : ISingleton
{
    private string dataFile = "game_data.json";
    private string dataPath;
    private GameStateData gameStateData;
    private GameManager gameManager;

    public GameState()
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
            this.gameStateData = (JsonUtility.FromJson<GameStateData>(data));
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No inventory data found, first run?");
            this.gameStateData = new GameStateData();
        }
    }

    public void NewGame()
    {
        this.gameStateData = new GameStateData();
    }

    private void WhenQuit()
    {
        this.SaveData();
    }

    public void UpdateData()
    {
        if(this.gameManager == null) this.gameManager = RuntimeStuff.GetSingleton<GameManager>();

        this.gameStateData.hasData = true;

        // Player
        Vector3 pos, rot;
        (pos, rot, this.gameStateData.health, this.gameStateData.sanity) = this.gameManager.GetPlayerInfo();
        this.gameStateData.playerLocation = FloatArrFromV3(pos);
        this.gameStateData.playerRotation = FloatArrFromV3(rot);
    }

    public (bool, Vector3, Vector3, float, float) GetPlayerInfo()
    {
        return (
            this.gameStateData.hasData, 
            V3FromFloatArr(this.gameStateData.playerLocation), 
            V3FromFloatArr(this.gameStateData.playerRotation), 
            this.gameStateData.health, 
            this.gameStateData.sanity
            );
    }

    public void SaveData()
    {
        this.UpdateData();

        string data = JsonUtility.ToJson(this.gameStateData);

        File.WriteAllText(dataPath, data);

        Debug.Log("Game data saved to: " + dataPath);
    }

    private float[] FloatArrFromV3(Vector3 v)
    {
        float[] r = new float[3];
        r[0] = v[0];
        r[1] = v[1];
        r[2] = v[2];
        return r;
    }
    private Vector3 V3FromFloatArr(float[] a)
    {
        if(a == null) return new Vector3();
        return new Vector3(a[0], a[1], a[2]);
    }

    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<GameState>(this);
    }
}

[System.Serializable]
class GameStateData
{
    public bool hasData;
    public float sanity, health;
    public float[] playerLocation, playerRotation;
    //! da p fazer isto??
    public float[][] monsters;
}
