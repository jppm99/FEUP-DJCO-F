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
    private Image pageImage;

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
        pageImage = page.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H) && !diaryOpened)
            openDiary();

        else if (Input.GetKeyDown(KeyCode.Escape) && diaryOpened)
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
        pageImage.sprite = pages[currentPage];

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

    public void increasePage()
    {
        if (currentPage < pages.Length - 1)
            currentPage++;

        pageImage.sprite = pages[currentPage];
    }

    public void decreasePage()
    {
        if (currentPage > 0)
            currentPage--;

        pageImage.sprite = pages[currentPage];
    }
}
