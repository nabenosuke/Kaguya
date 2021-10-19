using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearItem : MonoBehaviour
{
    public AudioClip gameClear = null;

    private StageControler stageControler = null;
    private bool isClear = false;
    // Start is called before the first frame update
    void Start()
    {
        //stageControler = GameObject.Find("StageController").GetComponent<StageControler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isClear)
            {
                isClear = true;
                Debug.Log("ゲームクリア");
                GManager.instance.PlaySE(gameClear);
                GManager.instance.isClear = true;
            }
        }
    }
}
