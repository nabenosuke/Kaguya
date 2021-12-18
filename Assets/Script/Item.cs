using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("輝き何個分か")] public int kagayaki;
    [SerializeField] private AudioClip getSE;
    [SerializeField] private AudioClip oneUPSE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GManager.instance != null)
            {
                GManager.instance.kagayaki += this.kagayaki;
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
    }
}
