using System.Collections.Generic;
using UnityEngine;

public static class RuntimeStuff
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Create new singleton objects
        new Inventory();


        // Set target FPS for the game
        int target_fps = 120;
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
     * returns true on success and false if there is already a pair with the same key
     */
    public static bool AddSingleton<T>(T value) where T : ISingleton
    {
        string id = "" + typeof(T);

        if(Singleton_registry.ContainsKey(id)){
            return false;
        }

        Singleton_registry.Add(id, (ISingleton)value);
        return true;
    }

}
