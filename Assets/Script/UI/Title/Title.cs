using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour, IMenu
{
  [SerializeField] private GameObject firstSelect;
  [SerializeField] private GameObject characterSelect;
  [SerializeField] private GameObject systemMenu;

  private Canvas canvas;
  private Selectable selectable;
  // Start is called before the first frame update
  void Start()
  {
    canvas = GetComponent<Canvas>();
    selectable = firstSelect.GetComponent<Selectable>();
    selectable.Select();
  }

  // Update is called once per frame
  void Update()
  {

  }
  void IMenu.Open()
  {
    canvas.enabled = true;
    selectable.Select();
  }

  void Close()
  {
    canvas.enabled = false;
  }

  public void OpenCharacterSelect()
  {
    Close();
    //切り替えにアニメーションつけてもいいかも
    characterSelect.GetComponent<IMenu>().Open();
  }

  public void OpenSystemMenu()
  {
    Close();
    //切り替えにアニメーションつけてもいいかも
    systemMenu.GetComponent<IMenu>().Open();
  }
}
