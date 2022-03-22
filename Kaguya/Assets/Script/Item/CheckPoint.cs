using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, IItem
{
    [Header("コンティニュー番号")] [SerializeField] int checkPointNum;
    // Start is called before the first frame update
    void Start()
    {

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
        }
        Destroy(this.gameObject);
    }
}
