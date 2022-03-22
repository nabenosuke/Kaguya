using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kagayaki : MonoBehaviour, IItem
{
    [Header("輝き何個分か")] [SerializeField] private int kagayaki;
    [Header("スコア")] [SerializeField] private int score;
    [SerializeField] private AudioClip getSE;
    [SerializeField] private AudioClip oneUPSE;
    // Start is called before the first frame update
    void Start()
    {
        if (score == 0)
        {
            Debug.Log("Scoreを設定し忘れてるアイテムあり");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IItem.ItemGet()
    {
        GManager.instance.kagayaki += this.kagayaki;
        GManager.instance.score += this.score;
        GManager.instance.PlaySE(getSE);
        if (GManager.instance.kagayaki >= 125)
        {
            GManager.instance.continueNum += 1;
            GManager.instance.kagayaki -= 125;
            GManager.instance.PlaySE(oneUPSE);
        }
        Destroy(this.gameObject);
    }
}
