using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearItem : MonoBehaviour
{

    [SerializeField] private AudioClip clearBGM;
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
                GManager.instance.PlaySE(clearBGM);
                isClear = true;
                Debug.Log("ゲームクリア");
                GManager.instance.isClear = true;
            }
        }
    }
}
