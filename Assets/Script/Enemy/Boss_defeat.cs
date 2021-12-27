using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_defeat : MonoBehaviour
{
    [SerializeField] private AudioClip se;
    private float fallSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {

        GManager.instance.PlaySE(se);
        GManager.instance.isClear = true;

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
    }
}
