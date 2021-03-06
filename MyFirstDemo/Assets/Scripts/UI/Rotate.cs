﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotateSpeed = 300;

    void Update()
    {
        transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
