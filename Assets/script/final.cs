using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class final : MonoBehaviour
{
    Rigidbody2D fizikFin;
    Rigidbody2D fizikBayrak;
    Vector3 vecFin;
    Vector3 vecBayrak;


    int yurumeAnimSayacFin = 0;
    public Sprite[] yurumeAnimFin;
    float yurumeAnimZamanFin = 0;
    SpriteRenderer spriteRenderer;

    public AudioClip audioClip;
    public AudioSource audioSource;
    int sesKontrol = 1;
    float horizontalFin;
    bool yoket = false;
    bool bayrakAnim = false;

  
    GameObject sesackapa;
    GameObject bayrak;
    GameObject tcYazi;
    public Text sonbir;
    public Text soniki;
  

    float bayrakX;
    float yavaslatma=0;
    float anaMenuyeDonZaman = 0;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;
        audioSource.clip = audioClip;
        audioSource.Play();




        soniki.text = "";
        sonbir.text = "";

        sesackapa = GameObject.Find("sesackapa");
        bayrak = GameObject.FindWithTag("bayrak");
        tcYazi = GameObject.FindWithTag("tcyazi");

        tcYazi.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();

        fizikFin = GetComponent<Rigidbody2D>();
        fizikBayrak =bayrak.GetComponent<Rigidbody2D>();



        sesKontrol = PlayerPrefs.GetInt("ses");
        sesackapa.transform.gameObject.SetActive(true);




        if (sesKontrol == 1)
        {
            Debug.Log("seskont==1");
            sesackapa.transform.GetChild(0).gameObject.SetActive(true);
            sesackapa.transform.GetChild(1).gameObject.SetActive(false);

        }

        else if (sesKontrol == 0)
        {

            Debug.Log("seskont==0");
            sesackapa.transform.GetChild(0).gameObject.SetActive(false);
            sesackapa.transform.GetChild(1).gameObject.SetActive(true);
            audioSource.mute = !audioSource.mute;

        }

    }


   void FixedUpdate()
    {   if(!yoket)
        {
            karakterHareketFin();
            AnimasyonFin();
        }
        else
        {
            vecFin = new Vector3(0, fizikFin.velocity.y, 0);
            fizikFin.velocity = vecFin;
        }


        if(bayrakAnim)
        {
           if((bayrak.transform.position.y)<=1.13f)
            {
                Time.timeScale = 0.3f;
                bayrak.transform.Translate(Vector3.up * Time.deltaTime);
                Debug.Log("test");
                sonbir.text = "TEBRİKLER HAKKINIZ OLAN BELEDİYE BAŞKANLIĞINI ALDINIZ, HERŞEY  GÜZEL OLACAK...";
                soniki.text = "SON";
                yavaslatma += 0.03f;
                tcYazi.SetActive(true);
                sonbir.color = new Color(0, 0, 0, yavaslatma);
                soniki.color = new Color(0, 0, 0, yavaslatma);
             
                anaMenuyeDonZaman += Time.deltaTime;

            }



        }


    }

    void karakterHareketFin()
    {

        vecFin = new Vector3(3, fizikFin.velocity.y, 0);
        fizikFin.velocity = vecFin;



    }

    void AnimasyonFin()
    {

      
            yurumeAnimZamanFin += Time.deltaTime;
            if (yurumeAnimZamanFin > 0.05f)
            {
                spriteRenderer.sprite = yurumeAnimFin[yurumeAnimSayacFin++];
                if (yurumeAnimSayacFin == yurumeAnimFin.Length)
                {
                    yurumeAnimSayacFin = 0;
                }
                yurumeAnimZamanFin = 0;
            }
            transform.localScale = new Vector3(1, 1, 1);


    }



    public void button(int gelen)
    {
        if(gelen==0)
        {
            SceneManager.LoadScene(0);
        }

        if (gelen == 1)
        {

            if (sesKontrol == 1)
            {
                sesKontrol = 0;
                PlayerPrefs.SetInt("ses", sesKontrol);

                sesackapa.transform.GetChild(1).gameObject.SetActive(true);
                sesackapa.transform.GetChild(0).gameObject.SetActive(false);
                audioSource.mute = !audioSource.mute;

            }

            else if (sesKontrol == 0)
            {
                sesKontrol = 1;
                PlayerPrefs.SetInt("ses", sesKontrol);

                sesackapa.transform.GetChild(0).gameObject.SetActive(true);
                sesackapa.transform.GetChild(1).gameObject.SetActive(false);
                audioSource.mute = !audioSource.mute;

            }

        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "kapi")
        {
            Time.timeScale = 0.4f;
            Debug.Log("kapı temas");
            spriteRenderer.sprite = null;
            yoket = true;
            bayrakAnim = true;
           


        }

    }


    }
