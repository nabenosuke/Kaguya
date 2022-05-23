using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSystem : MonoBehaviour
{
    [SerializeField] GameObject[] bosses;
    [SerializeField] Text bossName;
    // Start is called before the first frame update
    void Start()
    {
        bosses[(GManager.instance.characterID - 1) % 3].SetActive(true);
        bossName.text = GManager.instance.bossNameList[(GManager.instance.characterID - 1) % 3];
    }

    // Update is called once per frame

}
