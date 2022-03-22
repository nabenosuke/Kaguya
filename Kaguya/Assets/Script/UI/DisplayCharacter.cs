using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacter : MonoBehaviour
{
    [SerializeField] GameObject[] characterIcon;
    // Start is called before the first frame update
    void Start()
    {
        characterIcon[GManager.instance.characterID - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
