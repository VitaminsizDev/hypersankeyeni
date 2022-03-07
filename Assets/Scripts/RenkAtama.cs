using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RenkAtama : MonoBehaviour
{
    public string key;
    public MainMenuManager mainMenu;
    public int para;
    public Color renk;
    public GameObject yazi;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (key=="")
        {

        }
        else
        {
            text.SetText(para.ToString());
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetInt(key, 0);
                //PlayerPrefs.SetFloat(key+"r", renk.r);
                //PlayerPrefs.SetFloat(key+"b", renk.b);
                //PlayerPrefs.SetFloat(key+"g", renk.g);

            }
            if (PlayerPrefs.GetInt(key) > 0)
            {
                yazi.SetActive(false);
            }
            else
            {
                yazi.SetActive(true);
            }
        }
      
    }

public void satinal()
    {
        if (key=="")
        {

            mainMenu.source.clip = mainMenu.satinalmabasarali;
            mainMenu.source.Play();
            mainMenu.snake.color = renk;


            PlayerPrefs.SetFloat("r", renk.r);
            PlayerPrefs.SetFloat("b", renk.b);
            PlayerPrefs.SetFloat("g", renk.g);
        }
        else
        {
            if (PlayerPrefs.GetInt(key) == 0)
            {
                if (PlayerPrefs.GetInt("Elmas") >= para)
                {
                    mainMenu.source.clip = mainMenu.satinalmabasarali;
                    mainMenu.source.Play();
                    PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") - para);
                    PlayerPrefs.SetInt(key, 1);

                    mainMenu.snake.color = renk;

                    yazi.SetActive(false);
                    PlayerPrefs.SetFloat("r", renk.r);
                    PlayerPrefs.SetFloat("b", renk.b);
                    PlayerPrefs.SetFloat("g", renk.g);
                }
                else
                {
                    mainMenu.source.clip = mainMenu.satinalmabasarasiz;
                    mainMenu.source.Play();
                }
            }
            else
            {
                mainMenu.source.clip = mainMenu.satinalmabasarali;
                mainMenu.source.Play();
                mainMenu.snake.color = renk;


                PlayerPrefs.SetFloat("r", renk.r);
                PlayerPrefs.SetFloat("b", renk.b);
                PlayerPrefs.SetFloat("g", renk.g);
            }
        }
       
    
    }
}
