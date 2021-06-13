using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlaySound", 5, 4);
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
        isDead = true;
    }

    public void PlaySound()
    {
        if(!isDead)
            GetComponents<FMODUnity.StudioEventEmitter>()[2].Play();
    }

    public void PlayAttack()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[3].Play();
    }
}
