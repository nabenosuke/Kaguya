using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour, ISubMenu
{
    [SerializeField] private GameObject characterIcon;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private int characterID;
    private Animator animator;
    private Selectable selectable;
    private bool isSelected;
    private bool isPreviousSelected;
    // Start is called before the first frame update
    void Start()
    {
        animator = characterIcon.GetComponent<Animator>();
        selectable = GetComponent<Selectable>();
    }

    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PointerEnter()
    {
        switch (characterID)
        {
            case 1:
                animator.SetTrigger("Honoka");
                Debug.Log("ほのか");
                break;
            case 2:
                animator.SetTrigger("Kotori");
                Debug.Log("test");
                break;
            case 3:
                animator.SetTrigger("Umi");
                break;
            case 4:
                animator.SetTrigger("Rin");
                break;
            case 5:
                animator.SetTrigger("Hanayo");
                break;
            case 6:
                animator.SetTrigger("Maki");
                break;
            case 7:
                animator.SetTrigger("Nico");
                break;
            case 8:
                animator.SetTrigger("Nozomi");
                break;
            case 9:
                animator.SetTrigger("Eli");
                break;
            default:
                break;
        }
        //animator.SetInteger("CharacterID", characterID);
        Debug.Log("選択中");
    }
    public void PointerExit()
    {
        Debug.Log("選択解除");
    }
    public void Selected()
    {
        Debug.Log("決定");
        animator.SetFloat("AnimSpeed", 1.0f);
        gameStart.GetComponent<GameStart>().StartGame();
        selectable.enabled = false;
    }
}
