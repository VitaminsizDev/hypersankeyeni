using DG.Tweening;
using UnityEngine;
using TMPro;
using System;

public class Yemek : MonoBehaviour
{
    public int yemekcount = 1;
    public bool canvasgozuk = false;
    public TextMeshProUGUI textMesh;
    public bool isjewel = false;
    public Color renk;
    public Transform model;
    BoxCollider boxCollider;
    public bool finishelmas;
    private void Start()
    {
        Eventler.resetgame += geriacil;
        boxCollider = GetComponent<BoxCollider>();
        model.DOMoveY(1f, 1).SetLoops(-1, LoopType.Yoyo);
        model.DORotate(new Vector3(0, 180f, 0f), 1f, RotateMode.Fast).SetLoops(-1,LoopType.Incremental);
        if (isjewel)
        {
            if (canvasgozuk)
            {
                textMesh.gameObject.SetActive(true);
                if (yemekcount > 0)
                {
                    textMesh.color = renk;
                    textMesh.SetText("+" + yemekcount);
                }

            }
            else
            {
                if (textMesh != null)
                {
                    textMesh.gameObject.SetActive(false);
                }

            }
            return;
        }
        if (canvasgozuk)
        {
            textMesh.gameObject.SetActive(true);
            if (yemekcount > 0)
            {
                textMesh.color = Color.green;
                textMesh.SetText("+" + yemekcount);
            }
            else
            {
                textMesh.color = Color.red;
                textMesh.SetText("-" + -1*yemekcount);
            }
        }
        else
        {
            if (textMesh!=null)
            {
                textMesh.gameObject.SetActive(false);
            }
        
        }


      
      
    }
    public void kapan()
    {
        model.gameObject.SetActive(false);
        boxCollider.enabled = false;
        if (canvasgozuk)
        {
            textMesh.gameObject.SetActive(false);
        }
    }
    private void geriacil()
    {
        model.gameObject.SetActive(true);
        boxCollider.enabled = true;
        if (canvasgozuk)
        {
            textMesh.gameObject.SetActive(true);
        }
    }
}
