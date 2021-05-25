using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseDiary : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject sanityBar;
    public GameObject diaryPages;
    public GameObject page;

    private bool diaryOpened;
    private int currentPage = 0;
    public Sprite[] pages;


    // Start is called before the first frame update
    void Start()
    {
        diaryOpened = false;
        diaryPages.SetActive(false);
        healthBar.SetActive(true);
        sanityBar.SetActive(true);

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
        page.GetComponent<Image>().sprite = pages[currentPage];

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

    void changePage(int page)
    {

    }
}
