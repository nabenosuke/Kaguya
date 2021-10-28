using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    private Text playerText = null;
    private string oldName = null;

    // Start is called before the first frame update
    void Start()
    {
        playerText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            playerText.text = " " + GManager.instance.characterName;
        }
        else
        {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldName != GManager.instance.characterName)
        {
            playerText.text = GManager.instance.characterName;
            oldName = GManager.instance.characterName;
        }
    }
}
