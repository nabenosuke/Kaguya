using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject item;
    private IItem iItem;
    private string playerTag = "Player";

    void Start()
    {
        iItem = item.GetComponent<IItem>();
    }

    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            iItem.ItemGet();
        }
    }
    /*
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == playerTag)
            {
                isOn = false;
            }
        }
        */
    #endregion
}
