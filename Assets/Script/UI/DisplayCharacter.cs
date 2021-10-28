using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(GManager.instance.characterID - 1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
