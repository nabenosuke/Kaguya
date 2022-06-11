using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, IItem
{
    [SerializeField] private GameObject flagW;
    [SerializeField] private GameObject flag;
    [SerializeField] private AudioClip se;
    [Header("コンティニュー番号")] [SerializeField]private  int checkPointNum;

    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.continuePointNum == checkPointNum){
            flagW.SetActive(false);
            flag.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IItem.ItemGet()
    {
        if (GManager.instance.continuePointNum < checkPointNum)
        {
            GManager.instance.continuePointNum = checkPointNum;
            flagW.SetActive(false);
            flag.SetActive(true);
            GManager.instance.PlaySE(se);
        }
        //Destroy(this.gameObject);
    }
}
