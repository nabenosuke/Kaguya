using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject firstSelect;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject firstMenu;
    [SerializeField] private GameObject secondMenu;

    private IMenu titleScript;
    private ISubMenu subMenu;
    private Canvas canvas;
    private Selectable firstSelectable;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        firstSelectable = firstSelect.GetComponent<Selectable>();
        titleScript = title.GetComponent<IMenu>();
        subMenu = GetComponent<ISubMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.enabled)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Close();
            }
        }
    }

    void IMenu.Open()
    {
        canvas.enabled = true;
        if (subMenu != null)
        {
            subMenu.Initialize();
        }
        firstSelectable.Select();
    }

    public void Close()
    {
        titleScript.Open();
        canvas.enabled = false;
    }

    public void OpenFirstMenu()
    {
        Close();
        //切り替えにアニメーションつけてもいいかも
        firstMenu.GetComponent<IMenu>().Open();
    }
    public void OpenSecondMenu()
    {
        Close();
        //切り替えにアニメーションつけてもいいかも
        secondMenu.GetComponent<IMenu>().Open();
    }
}
