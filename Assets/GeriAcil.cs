using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeriAcil : MonoBehaviour
{
    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        Eventler.resetgame += restgame;
    }
    BoxCollider col;
    private void restgame()
    {
        col.enabled = true;
    }
}
