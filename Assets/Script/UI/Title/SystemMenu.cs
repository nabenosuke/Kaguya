using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SystemMenu : MonoBehaviour, ISubMenu
{
    private BaseMenu baseMenu;
    [SerializeField] private GameObject[] systemPages;
    [SerializeField] private Text currentPageText;
    private int systemPageNum;
    private int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        baseMenu = GetComponent<BaseMenu>();
        systemPageNum = systemPages.Length;
        currentPageText.text = "(" + (currentPage + 1) + "/" + systemPageNum + ")";
    }

    public void Initialize()
    {
        currentPage = 0;
        UpdatePage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdatePage()
    {
        systemPages[currentPage].SetActive(true);
        currentPageText.text = "(" + (currentPage + 1) + "/" + systemPageNum + ")";
    }

    public void ForwardPage()
    {
        systemPages[currentPage].SetActive(false);
        currentPage++;
        if (currentPage < systemPageNum)
        {
            UpdatePage();
        }
        else
        {
            currentPage = 0;
            baseMenu.Close();
        }
    }

    public void BackPage()
    {
        systemPages[currentPage].SetActive(false);
        currentPage--;
        if (currentPage >= 0)
        {
            UpdatePage();
        }
        else
        {
            currentPage = 0;
            baseMenu.Close();
        }
    }
}
