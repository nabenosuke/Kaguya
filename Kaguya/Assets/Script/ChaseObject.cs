﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 defaultLocalPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + defaultLocalPos;
    }
}
