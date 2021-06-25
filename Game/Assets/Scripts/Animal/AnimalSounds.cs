using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStep()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
    }

    public void PlayDeath()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();
    }

    public void PlaySound()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[2].Play();
    }
}
