using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtKapan : MonoBehaviour
{
    private void OnEnable()
    {
        Eventler.resetgame += reset;
    }
    private void OnDisable()
    {
        Eventler.resetgame -= reset;
    }

    private void reset()
    {
        gameObject.SetActive(false);
    }
}
