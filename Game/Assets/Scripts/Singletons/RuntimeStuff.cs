using System.Collections.Generic;
using UnityEngine;

public static class RuntimeStuff
{
    private static bool saveOnQuit = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Create new singleton objects
        new GameState();
        new Inventory();
        new CameraManager();

        // Set target FPS for the game
        int target_fps = 75;
        Application.targetFrameRate = target_fps > Screen.currentResolution.refreshRate ? Screen.currentResolution.refreshRate : target_fps;
    }

    private static Dictionary<string, ISingleton> Singleton_registry = new Dictionary<string, ISingleton>();
    
    /**
     * Gets a Singleton pointer from the registry of type T using its type
     */
    public static T GetSingleton<T>() where T : ISingleton
    {
        string id = "" + typeof(T);
        
        ISingleton obj = null;

        Singleton_registry.TryGetValue(id, out obj);

        return (T)obj;
    }

    /**
     * Adds a key/value pair to the Singleton registry
     */
    public static void AddSingleton<T>(T value) where T : ISingleton
    {
        string id = "" + typeof(T);

        if(Singleton_registry.ContainsKey(id)){
            Singleton_registry.Remove(id);
        }

        Singleton_registry.Add(id, (ISingleton)value);
    }

    public static bool SaveOnQuit()
    {
        return saveOnQuit;
    }
}
