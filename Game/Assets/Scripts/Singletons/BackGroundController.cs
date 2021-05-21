using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject mainMenuBackGround;
    public GameObject midGameMenuBackGround;
    public GameObject endGameMenuBackGround;

   
    public void disableAll()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(false);
    }

    public void showMainMenu()
    {
        mainMenuBackGround.SetActive(true);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(false);
    }

    public void showMidGameMenu()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(true);
        endGameMenuBackGround.SetActive(false);
    }

    public void showEndGameMenu()
    {
        mainMenuBackGround.SetActive(false);
        midGameMenuBackGround.SetActive(false);
        endGameMenuBackGround.SetActive(true);
    }
}
