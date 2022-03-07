using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CanvasGroup finishpanel;
    public GameObject panel;
    public Slider slider;


    public CanvasGroup gamefinishpanel;
    public GameObject gamefinishpanel2;
    public TextMeshProUGUI elmastext;


    public TextMeshProUGUI geriyeSaymaText;
    public GameObject geriyeSaymaParent;


    Sequence geriyeSaymaSequence;
    public bool basla;


    public Transform finishjawel;
    public Transform player;
    public AudioSource ticksesi;
    public AudioSource gosesi;

    public Slider elmasjewel;
    public List<Transform> elmaslar = new List<Transform>();
    //List<Slider> sliders = new List<Slider>();
    List<sliderlar> sliderlars = new List<sliderlar>();

    public List<Image> oduller = new List<Image>();
    public List<Image> odullerFinish = new List<Image>();
    public GameObject canvasplayer;
    class sliderlar
    {
        public float sl;
        public bool kulanýldý;
        public Transform resim;
        public Image odul;
    }

    private void Awake()
    {
        instance = this;
        Startt();
        slider.maxValue = finishjawel.position.z - player.position.z;
        for (int i = 0; i < oduller.Count; i++)
        {
            oduller[i].transform.localScale = Vector3.zero;
            oduller[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < odullerFinish.Count; i++)
        {
            odullerFinish[i].transform.localScale = Vector3.zero;
            odullerFinish[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < elmaslar.Count; i++)
        {
            Slider s = Instantiate(elmasjewel, panel.transform);
            s.gameObject.GetComponent<RectTransform>().anchoredPosition = slider.transform.localPosition;
            s.maxValue = slider.maxValue;
            s.value = elmaslar[i].position.z;

            sliderlars.Add(new sliderlar { sl = s.value, kulanýldý = false, resim = s.transform.GetChild(2).GetChild(0).GetChild(0), odul = oduller[i] });
        }

    }
    public int elmas = 0;
    public void Startt()
    {
        geriyeSaymaSequence = DOTween.Sequence();
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.SetActive(true));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.transform.localScale = new Vector3(2, 2, 1));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaText.text = "3");
       geriyeSaymaSequence.AppendCallback(() => ticksesi.Play());

        //Assign Random COlor
        geriyeSaymaText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        geriyeSaymaSequence.Append(geriyeSaymaParent.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetEase(Ease.OutBack));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.transform.localScale = new Vector3(2, 2, 1));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaText.text = "2");
        geriyeSaymaSequence.AppendCallback(() => ticksesi.Play());
        geriyeSaymaText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        geriyeSaymaSequence.Append(geriyeSaymaParent.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetEase(Ease.OutBack));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.transform.localScale = new Vector3(2, 2, 1));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaText.text = "1");
        geriyeSaymaSequence.AppendCallback(() => ticksesi.Play());
        geriyeSaymaText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        geriyeSaymaSequence.Append(geriyeSaymaParent.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetEase(Ease.OutBack));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.transform.localScale = new Vector3(2, 2, 1));
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaText.text = "GO!");
         geriyeSaymaSequence.AppendCallback(() => gosesi.Play());
        geriyeSaymaText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        geriyeSaymaSequence.AppendInterval(1f);
        geriyeSaymaSequence.Append(geriyeSaymaParent.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetEase(Ease.OutBack));
        geriyeSaymaText.color = Color.white;
        geriyeSaymaSequence.AppendCallback(() => geriyeSaymaParent.SetActive(false));

        geriyeSaymaSequence.OnComplete(() =>
        {
            basla = true;
            canvasplayer.SetActive(true);
        });

        geriyeSaymaSequence.Pause();

        geriyeSaymaSequence.Play();
    }
    public void fnishgame()
    {
        basla = false;
        gamefinishpanel.gameObject.SetActive(true);
        gamefinishpanel.alpha = 0f;
        gamefinishpanel2.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(gamefinishpanel.DOFade(1, 1f));
        seq.Append(gamefinishpanel2.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBounce));
        seq.Append(slider.DOValue(player.transform.position.z + 1, 2f));
        seq.OnComplete(()=> {
            for (int i = 0; i < odullerFinish.Count; i++)
            {
                odullerFinish[i].gameObject.SetActive(true);
                odullerFinish[i].transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBounce);

            }
        });
        elmastext.SetText(elmas.ToString());
        canvasplayer.SetActive(false);
       
    }
    public void gameover()
    {
        basla = false;

        finishpanel.gameObject.SetActive(true);
        finishpanel.alpha = 0f;
        panel.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(finishpanel.DOFade(1, 1f));
        seq.Append(panel.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBounce));
        seq.Append(slider.DOValue(player.transform.position.z + 1, 2f));
        //seq.Join(DOTween.To(()=> value,(x)=> value=x, player.transform.position.z,2f));
        canvasplayer.SetActive(false);
    }
    bool gameoverbool = false;
    public void resetgame()
    {
        Startt();
        Eventler.resetgame();
        finishpanel.alpha = 0f;
        panel.transform.localScale = Vector3.zero;
        finishpanel.gameObject.SetActive(false);
        gamefinishpanel.gameObject.SetActive(false);
        slider.value = 0;
        for (int i = 0; i < odullerFinish.Count; i++)
        {
            odullerFinish[i].transform.localScale = Vector3.zero;
            odullerFinish[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < sliderlars.Count; i++)
        {
            sliderlars[i].kulanýldý = false;
            sliderlars[i].odul.transform.localScale = Vector3.zero;
        }
    }

    private void Update()
    {
        if (slider.value > 0)
        {
            for (int i = 0; i < sliderlars.Count; i++)
            {
                if (sliderlars[i].kulanýldý == false)
                {
                    if (slider.value >= sliderlars[i].sl)
                    {
                        sliderlars[i].kulanýldý = true;
                        sliderlars[i].resim.transform.DOPunchScale(Vector3.one * 1.5f, 0.5f, 10, 1).SetLoops(1, LoopType.Restart);
                        sliderlars[i].resim.transform.DOPunchRotation(Vector3.one * 1.5f, 0.5f, 10, 1).SetLoops(1, LoopType.Restart);
                        sliderlars[i].odul.gameObject.SetActive(true);
                        sliderlars[i].odul.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
                        player.GetComponent<Snake>().elmasses();
                    }
                }
            }
        }
    }
    public void loadmenu()
    {
        SceneManager.LoadScene(0);
    }
}
