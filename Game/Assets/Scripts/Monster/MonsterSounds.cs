using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    bool isDead = false;
    bool firstSound = false;
    public float howlTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlaySound", 5, howlTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayStep()
    {
        firstSound = true;
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
    }

    public void PlayDeath()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();
        isDead = true;
    }

    public void PlaySound()
    {
        if(!isDead && firstSound)
            GetComponents<FMODUnity.StudioEventEmitter>()[2].Play();
    }

    public void PlayAttack()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[3].Play();
    }
}
