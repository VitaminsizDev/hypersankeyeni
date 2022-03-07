using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Cinemachine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.up;
    public GameObject segmentPrefab;
    public GameObject dirtPrefab;
    public float movespeed = 1f;

    public List<Transform> _segments;
  

    public LayerMask ground;
    public TextMeshProUGUI textMesh;

    private int yemeksayisi = 1;

    public List<Vector3> sonadimlar = new List<Vector3>();
    public GameObject elmasprefab;
    public GameObject hedef;


    public TextMeshProUGUI elmastext;

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public GameObject paeffect;
    Vector2 lastdir;
    public AudioSource audioSource;
    public List<AudioClip> elmaYeme;
    public List<AudioClip> elmasyeme;
    public List<AudioClip> zarar;
    public List<AudioClip> complate;
    private void Awake()
    {
        Eventler.resetgame += reset;
        _segments = new List<Transform>();
        _segments.Add(this.transform);

        cam1.Priority = 1;
        cam2.Priority = 0;
        lastdir = Vector2.zero;

      //  updateText(PlayerPrefs.GetInt("Elmas"));
    }

    private void reset()
    {
        _direction = Vector2.up;
        transform.LookAt(this.transform.position + new Vector3(_direction.x, 0f, _direction.y));
        yemeksayisi = 1;
        textMesh.SetText(yemeksayisi.ToString());
        sonadimlar = new List<Vector3>();
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        transform.position = new Vector3(0, 0.5f, -2);
        inputdur = false;
        died = false;
        cam1.Priority = 1;
        cam2.Priority = 0;
        oyunbitti = false;
        lastdir = Vector2.zero;
    }

    private void Start()
    {
        //for (int i = 1; i < 4; i++)
        //{
        //    GameObject segment = PoolManager.instance.ReuseObject(segmentPrefab, Vector3.zero, Quaternion.identity);
        //    segment.transform.position = _segments[_segments.Count - 1].position;
        //    segment.SetActive(true);
        //    _segments.Add(segment.transform);
        //}
        updateText(PlayerPrefs.GetInt("Elmas"));
    }
    bool died;
    bool oyunbitti;
    private void Update()
    {
        if (!GameManager.instance.basla)
        {
            return;
        }
        if (oyunbitti)
        {
            return;
        }
        if (died)
        {
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Eventler.resetgame();
            //}
            return;
        }
        MyInput();

        if (move)
        {
            Move();

        }
        else
        {

        }

    }
    bool move = true;
    private void Move()
    {

      
        move = false;


        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].DOMove(_segments[i - 1].position, movespeed);
        }

        lastdir = _direction;
        transform.LookAt(this.transform.position + new Vector3(_direction.x, 0f, _direction.y));


        this.transform.DOMove(new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, this.transform.position.y, Mathf.Round(this.transform.position.z) + _direction.y), movespeed).OnComplete(() => move = true);
     
        sonadimlar.Add(this.transform.position);
        kontrol();

    }
    private void kontrol()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f, ground))
        {

        }
        else
        {
            GameObject a = PoolManager.instance.ReuseObject(dirtPrefab, new Vector3(transform.position.x, -1, transform.position.z), Quaternion.identity);
            a.SetActive(true);
            if (_segments.Count > 1)
            {

                _segments[_segments.Count - 1].gameObject.SetActive(false);
                _segments.RemoveAt(_segments.Count - 1);

                yemeksayisi--;
                textMesh.SetText(yemeksayisi.ToString());
            }
            else
            {
                died = true;
                GameManager.instance.gameover();
                //  _segments[_segments.Count - 1].gameObject.SetActive(false);
                //  _segments.RemoveAt(_segments.Count - 1);
               

            }

        }
    }
    bool inputdur = false;
    private void MyInput()
    {
        if (inputdur)
        {
            return;
        }
        if (Input.touchCount > 0)
        {
            Touch parmak = Input.GetTouch(0);
            if (parmak.deltaPosition.y > 30f )
            {
                _direction = Vector2.up;
            }
            else if (parmak.deltaPosition.x < -30f && lastdir != Vector2.right)
            {
                _direction = Vector2.left;
            }
            else if (parmak.deltaPosition.x > 30f && lastdir != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A) && lastdir != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && lastdir != Vector2.left)
        {
            _direction = Vector2.right;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Yemek"))
        {
            int a = other.GetComponent<Yemek>().yemekcount;
            if (other.GetComponent<Yemek>().isjewel)
            {
                updateText(a);
                Elmas(a);
                PlayerPrefs.SetInt("Elmas", elmas);
                other.GetComponent<Yemek>().kapan();
                return;
            }

            if (a > 0)
            {
                GameObject effect = PoolManager.instance.ReuseObject(paeffect, transform.position, Quaternion.identity);
                var main = effect.GetComponent<ParticleSystem>().main;
              
                effect.SetActive(true);
                effect.GetComponent<ParticleSystem>().Play();
                if (a>1)
                {
                    main.startColor = Color.yellow;
                }
                else
                {
                    main.startColor = Color.green;
                }
                Grow(a);
            }
            else
            {
                GameObject effect = PoolManager.instance.ReuseObject(paeffect, transform.position, Quaternion.identity);
                var main = effect.GetComponent<ParticleSystem>().main;
                main.startColor = Color.red;
                effect.SetActive(true);
                effect.GetComponent<ParticleSystem>().Play();
                shrink(-1 * a);
            }
            other.GetComponent<Yemek>().kapan();
        }
        else if (other.CompareTag("FinishGame"))
        {
            cam1.Priority = 0;
            cam2.Priority = 1;
            _direction = Vector2.up;
            inputdur = true;
            other.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag("ComplateGame"))
        {
          
            int a = other.GetComponent<Yemek>().yemekcount;
            Elmas(a,true);
            updateText(a);
            PlayerPrefs.SetInt("Elmas", elmas);
            oyunbitti = true;
            other.GetComponent<Yemek>().kapan();
            GameManager.instance.fnishgame();
            
        }

    }


    private void Elmas(int yemek = 1,bool bitis=false)
    {
        if (bitis)
        {

            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.clip = complate[UnityEngine.Random.Range(0, complate.Count)];
            audioSource.Play();
        }
        else
        {
            elmasses();
        }
        
        for (int i = 0; i < yemek; i++)
        {
            GameObject a = PoolManager.instance.ReuseObject(elmasprefab, transform.position, Quaternion.identity);
            a.transform.localScale = Vector3.one * 2;
            a.SetActive(true);
            Vector3 zipla = new Vector3(UnityEngine.Random.RandomRange(-2, 2), 0f, UnityEngine.Random.RandomRange(-2, 2));
            Sequence seq = DOTween.Sequence();
            seq.Append(a.transform.DOJump(a.transform.position + zipla, 1, 2, 1.5f));
            seq.Join(a.transform.DORotate(new Vector3(0, 180f, 0f), 1f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Incremental));
            seq.Append(a.transform.DOScale(Vector3.zero, 1f));
            seq.OnComplete(() => { a.SetActive(false); });
        }
    }

    public void elmasses()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.clip = elmasyeme[UnityEngine.Random.Range(0, elmasyeme.Count)];
        audioSource.Play();
    }

    private void Grow(int yemek = 1)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.clip = elmaYeme[UnityEngine.Random.Range(0, elmaYeme.Count)];
        audioSource.Play();
        for (int i = 0; i < yemek; i++)
        {
            GameObject segment = PoolManager.instance.ReuseObject(segmentPrefab, Vector3.zero, Quaternion.identity);

            segment.transform.position = sonadimlar[sonadimlar.Count - _segments.Count];

            segment.SetActive(true);
            segment.transform.localScale = Vector3.zero;
            segment.transform.DOScale(Vector3.one * 0.9f, 0.5f).SetEase(Ease.OutBounce);
            _segments.Add(segment.transform);

            yemeksayisi++;
            textMesh.SetText(yemeksayisi.ToString());
        }
    }
    private void shrink(int yemek = 1)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.clip = zarar[UnityEngine.Random.Range(0, zarar.Count)];
        audioSource.Play();
        for (int i = 0; i < yemek; i++)
        {


            if (_segments.Count > 1)
            {
                _segments[_segments.Count - 1].gameObject.SetActive(false);
                _segments.RemoveAt(_segments.Count - 1);
                yemeksayisi--;
                textMesh.SetText(yemeksayisi.ToString());
            }
            else
            {
                died = true;
                break;
            }

        }
    }

    int elmas = 0;
    private void updateText(int sayi = 1)
    {
        elmas += sayi;
      
        GameManager.instance.elmas = elmas;
        elmastext.transform.DOPunchScale(Vector3.one, 1f, 10, 1).SetLoops(1, LoopType.Yoyo);
        elmastext.SetText(elmas.ToString());
    }


}
