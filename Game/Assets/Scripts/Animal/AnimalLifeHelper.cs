using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLifeHelper: MonoBehaviour
{ 
    public void CallDieCollectable()
    {
        GetComponentInParent<AnimalLife>().Die();
    }
}
