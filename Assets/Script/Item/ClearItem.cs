using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearItem : MonoBehaviour, IItem
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

    void IItem.ItemGet()
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
