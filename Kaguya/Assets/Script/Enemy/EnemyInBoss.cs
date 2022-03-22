using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "StageArea")
        {
            Destroy(gameObject);
        }
    }
}
