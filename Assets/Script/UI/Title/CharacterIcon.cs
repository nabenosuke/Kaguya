using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField] private GameObject[] characterIcons;
    [SerializeField] private GameObject[] weponImages;
    private int charaID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIcon(int charaID)
    {
        this.charaID = charaID;
        foreach (GameObject icon in characterIcons)
        {
            icon.SetActive(false);
        }
        characterIcons[charaID - 1].SetActive(true);

        //武器表示
        foreach (var wepon in weponImages)
        {
            wepon.SetActive(false);
        }
        weponImages[GManager.instance.weponIDList[charaID - 1]].SetActive(true);
    }

    public void SetAnim()
    {
        characterIcons[charaID - 1].GetComponent<Animator>().SetFloat("AnimSpeed", 1.0f);
    }
}
