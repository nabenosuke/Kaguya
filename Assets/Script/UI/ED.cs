using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ED : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        switch (GManager.instance.characterID)
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
                Debug.Log("例外が発生しました(キャラアイコン)");
                break;
        }
        animator.SetFloat("AnimSpeed", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            GManager.instance.InitializeGame();
            SceneManager.LoadScene("Title");
        }
    }
}
