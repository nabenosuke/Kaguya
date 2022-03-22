using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSE : MonoBehaviour
{
    [SerializeField] [Header("カーソル移動SE")] private AudioClip moveSE;
    [SerializeField] [Header("決定SE")] private AudioClip selectSE;
    [SerializeField] [Header("キャンセルSE")] private AudioClip cancelSE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            GManager.instance.PlaySE(cancelSE);
        }
        if (Input.GetButtonDown("Submit"))
        {
            GManager.instance.PlaySE(selectSE);
        }
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            GManager.instance.PlaySE(moveSE);
        }

        {

        }
    }
}
