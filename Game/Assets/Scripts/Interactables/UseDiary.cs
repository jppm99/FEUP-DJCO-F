using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDiary : MonoBehaviour
{
    private bool diaryOpened;

    public GameObject healthBar;
    public GameObject sanityBar;
    public GameObject diaryPages;


    // Start is called before the first frame update
    void Start()
    {
        diaryOpened = false;
        diaryPages.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H) && !diaryOpened)
            openDiary();

        else if (Input.GetKeyDown(KeyCode.H) && diaryOpened)
            closeDiary();
    }

    public void openDiary()
    {
        diaryOpened = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        healthBar.SetActive(false);
        sanityBar.SetActive(false);
        diaryPages.SetActive(true);
    }

    void closeDiary()
    {
        diaryOpened = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthBar.SetActive(true);
        sanityBar.SetActive(true);
        diaryPages.SetActive(false);
    }
}
