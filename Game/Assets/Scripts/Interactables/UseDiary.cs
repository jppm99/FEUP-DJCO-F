using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseDiary : MonoBehaviour
{
    public GameObject diaryPages;
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject closeButton;
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
        pageImage = page.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H) && !diaryOpened)
            openDiary();

        //else if (Input.GetKeyDown(KeyCode.Escape) && diaryOpened)
        //    closeDiary();

        if (currentPage >= pages.Length - 1)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
            closeButton.SetActive(true);

        }
        else if (currentPage <= 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            closeButton.SetActive(false);

        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            closeButton.SetActive(false);
        }
    }

    public void openDiary()
    {
        currentPage = 0;
        diaryOpened = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        diaryPages.SetActive(true);
        pageImage.sprite = pages[currentPage];

    }

    public void closeDiary()
    {
        // Time.timeScale = 1;
        diaryOpened = false;
        diaryPages.SetActive(false);
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    public void increasePage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            GetComponents<FMODUnity.StudioEventEmitter>()[2].Play();
        }

        pageImage.sprite = pages[currentPage];

    }

    public void decreasePage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            GetComponents<FMODUnity.StudioEventEmitter>()[2].Play();
        }

            pageImage.sprite = pages[currentPage];

    }
}
