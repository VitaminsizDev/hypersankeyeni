using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class MainMenuManager : MonoBehaviour
{

    public Image fadecanvas;

    public GameObject anamenu;
    public GameObject skinmenu;

    public Material snake;

    public TextMeshProUGUI elmas;

    public AudioClip satinalmabasarali;
    public AudioClip satinalmabasarasiz;
    public AudioSource source;
    private void Awake()
    {
        fadecanvas.DOFade(0, 0);
        if (!PlayerPrefs.HasKey("Elmas"))
        {
            PlayerPrefs.SetInt("Elmas",0);
        }
       
        if (!PlayerPrefs.HasKey("r"))
        {
            PlayerPrefs.SetFloat("r", snake.color.r);
            PlayerPrefs.SetFloat("b", snake.color.b);
            PlayerPrefs.SetFloat("g", snake.color.g);
        }
        else
        {
            snake.color = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
        }
        elmas.SetText(PlayerPrefs.GetInt("Elmas").ToString());
    }

    public void startgame()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(fadecanvas.DOFade(1, 1f));
        sq.OnComplete(()=>SceneManager.LoadScene(1));
    }   
    public void SkinScene()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(fadecanvas.DOFade(1, 1f));
        sq.AppendCallback(()=> { anamenu.SetActive(false);skinmenu.SetActive(true); });
        sq.Append(fadecanvas.DOFade(0, 1f));
        
    } 
    public void MenuMenu()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(fadecanvas.DOFade(1, 1f));
        sq.AppendCallback(()=> { anamenu.SetActive(true);skinmenu.SetActive(false); });
        sq.Append(fadecanvas.DOFade(0, 1f));
        
    }
 

}
