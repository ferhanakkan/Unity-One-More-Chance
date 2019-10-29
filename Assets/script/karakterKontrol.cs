using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;



public class karakterKontrol : MonoBehaviour
{
    
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Sprite[] olmeAnim;

    public Text canText;
    public Text altinText;
    public Image siyahArkaPlan;


     int can=100;

    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int altinSayac = 0;

    SpriteRenderer spriteRendere;
    Rigidbody2D fizik;

    public GameObject ekrem; //kayıt sistemi için kullannıyorum


    Vector3 ekremVec;
    Vector3 toprakVec;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraİlkPos;

    GameObject kamera;

    float horizontal = 0;
    int ikiKereZipla = 0;

    float olmeAnimZaman = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float siyahArkaPlanSayaci=0;
    float anaMenuyeDonZaman = 0;
   

    bool tetik = false;
    bool muzik = true;
    bool reklamvar = true;
    bool reklamizlendi = false;


    GameObject pauseAyar;
    GameObject reklamAyar;
    GameObject sesackapa;
    GameObject sonPozisyon;
    GameObject kontrolAckapa;
    GameObject yazi;
    GameObject tamambutton;


    public AudioClip audioClip;
    public AudioSource audioSource;

    int sesKontrol = 1;

 


    void Start()
    {

     

        sesackapa = GameObject.Find("sesackapa");
        sonPozisyon = GameObject.FindWithTag("sonpozisyon");
        kontrolAckapa = GameObject.Find("kontrol");
        audioSource.clip = audioClip;
        audioSource.Play();
        yazi = GameObject.FindWithTag("yazi");
        tamambutton = GameObject.FindWithTag("tamam");



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




        Time.timeScale = 1;
        spriteRendere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        pauseAyar = GameObject.Find("pauseMenusu");
        reklamAyar = GameObject.Find("reklam");



       


        for (int i = 0; i < reklamAyar.transform.childCount; i++)
        {
            reklamAyar.transform.GetChild(i).gameObject.SetActive(false);
            reklamAyar.transform.GetChild(i).GetComponent<Button>().interactable = false;

        }

   

        for (int i = 1; i < pauseAyar.transform.childCount; i++)
        {
            pauseAyar.transform.GetChild(i).gameObject.SetActive(false);

            pauseAyar.transform.GetChild(i).GetComponent<Button>().interactable = false;

        }

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);

        }


        kameraİlkPos = kamera.transform.position - transform.position;
        canText.text = "Can " + can;
        altinText.text = "Adalet Puanı "+altinSayac+" /20 " ;
        siyahArkaPlan.gameObject.SetActive(false);


    }


    void Update()
    {
       




        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        { 
            if (ikiKereZipla < 2)
            {
                fizik.AddForce(new Vector2(0, 300));
                ikiKereZipla++;
            }
        }


        if (muzik)
        {
            audioSource.Play();
            muzik = false;
        }


    }


    void FixedUpdate()
    {
        karakterHareket();
        Animasyon();

      



        if (can <= 0)
        {

            if (!tetik)
            {
                print("olme");

                olmeAnimZaman += Time.deltaTime;
                if (olmeAnimZaman > 0.05f)
                {
                    spriteRendere.sprite = olmeAnim[0];
                    spriteRendere.sprite = olmeAnim[1];


                }



            }

            for (int i = 0; i < kontrolAckapa.transform.childCount; i++)
            {
                kontrolAckapa.transform.GetChild(i).gameObject.SetActive(false);

                kontrolAckapa.transform.GetChild(i).GetComponent<Button>().interactable = false;

            }

            Time.timeScale = 0.4f;
            canText.text = "Can 0";
            canText.enabled = false;
            altinText.enabled = false;
            siyahArkaPlan.gameObject.SetActive(true);
            siyahArkaPlanSayaci += 0.03f;
            siyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            

            if (reklamvar)
            {
                if(!reklamizlendi)
                {
                    for (int i = 0; i < reklamAyar.transform.childCount; i++)
                    {
                        reklamAyar.transform.GetChild(i).gameObject.SetActive(true);
                        reklamAyar.transform.GetChild(i).GetComponent<Button>().interactable = true;

                    }

                }

                else if(reklamizlendi)
                {
                    reklamAyar.transform.GetChild(0).gameObject.SetActive(true);
                    reklamAyar.transform.GetChild(0).GetComponent<Button>().interactable = true;
                    reklamAyar.transform.GetChild(1).gameObject.SetActive(true);
                    reklamAyar.transform.GetChild(1).GetComponent<Button>().interactable = false;

                }
               


            }


          
        }

    }

    void LateUpdate()
    {
        kameraKontrol();
    }

    void karakterHareket()
    {
       horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 8, fizik.velocity.y, 0);
        fizik.velocity = vec;

       


    }

    void Animasyon()
    {

        if (can > 0)
        {

            if (ikiKereZipla == 0)

            {

                if (fizik.velocity.y < -0.40)
                {
                    spriteRendere.sprite = ziplamaAnim[1];
                    if (horizontal > 0)
                    {
                        transform.localScale = new Vector3(2, 2, 2);
                    }
                    else if (horizontal < 0)
                    {
                        transform.localScale = new Vector3(-2, 2, 2);
                    }

                }
                else
                {
                    if (horizontal == 0)
                    {
                        beklemeAnimZaman += Time.deltaTime;

                        if (beklemeAnimZaman > 0.05f)
                        {

                            spriteRendere.sprite = beklemeAnim[beklemeAnimSayac++];
                            if (beklemeAnimSayac == beklemeAnim.Length)
                            {
                                beklemeAnimSayac = 0;
                            }
                            beklemeAnimSayac = 0;
                        }
                        if(transform.localScale.x>0)
                        {
                            transform.localScale = new Vector3(2, 2, 2);
                        }

                        else if(transform.localScale.x<0)
                            {
                            transform.localScale = new Vector3(-2, 2, 2);
                        }
                    }

                    else if (horizontal > 0)
                    {
                        yurumeAnimZaman += Time.deltaTime;
                        if (yurumeAnimZaman > 0.05f)
                        {

                            Scene currentScene = SceneManager.GetActiveScene();
                            string sceneName = currentScene.name;

                            if (sceneName == "level1")
                            {
                                yazi.transform.gameObject.SetActive(false);
                                tamambutton.transform.gameObject.SetActive(false);
                                tamambutton.transform.GetComponent<Button>().interactable = false;
                            }




                            spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];
                            if (yurumeAnimSayac == yurumeAnim.Length)
                            {
                                yurumeAnimSayac = 0;
                            }
                            yurumeAnimZaman = 0;
                        }
                        transform.localScale = new Vector3(2, 2, 2);
                       
                    }


                    else if (horizontal < 0)
                    {
                        Scene currentScene = SceneManager.GetActiveScene();
                        string sceneName = currentScene.name;

                        if (sceneName == "level1")
                        {
                            yazi.transform.gameObject.SetActive(false);
                            tamambutton.transform.gameObject.SetActive(false);
                            tamambutton.transform.GetComponent<Button>().interactable = false;
                        }

                        yurumeAnimZaman += Time.deltaTime;
                        if (yurumeAnimZaman > 0.05f)
                        {
                            spriteRendere.sprite = yurumeAnim[yurumeAnimSayac++];

                            if (yurumeAnimSayac == yurumeAnim.Length)
                            {
                                yurumeAnimSayac = 0;
                            }
                            yurumeAnimZaman = 0;
                        }
                        transform.localScale = new Vector3(-2, 2, 2);
                    }
                }

            }
            else
            {
                if (fizik.velocity.y > 0)
                {
                    spriteRendere.sprite = ziplamaAnim[0];

                    if (horizontal > 0)
                    {
                        transform.localScale = new Vector3(2, 2, 2);

                    }
                    else if (horizontal < 0)
                    {
                        transform.localScale = new Vector3(-2, 2, 2);
                    }

                }
                else
                {
                    spriteRendere.sprite = ziplamaAnim[1];
                   
                    if (horizontal > 0)
                    {
                        transform.localScale = new Vector3(2, 2, 2);
                    }
                    else if (horizontal < 0)
                    {
                        transform.localScale = new Vector3(-2, 2, 2);
                    }
                }

                if (horizontal > 0)
                {
                    transform.localScale = new Vector3(2, 2, 2);
                }
                else if (horizontal < 0)
                {
                    transform.localScale = new Vector3(-2, 2, 2);
                }


            }
        }

    }




  

    void OnCollisionEnter2D(Collision2D col)
    {
        ikiKereZipla = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       

        if (col.gameObject.tag == "dusman")
        {
            can -= 10;
            canText.text = "Can " + can;
        }

        if (col.gameObject.tag == "testere")
        {
            can -= 8;
            canText.text = "Can " + can;
        }

        if (col.gameObject.tag == "bolumbitsin")
        {
            if (altinSayac == 20)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //SceneManager.LoadScene(4);
            }

        }

        if (col.gameObject.tag == "oyunbitsin")
        {
            if (altinSayac == 1)
            {
                SceneManager.LoadScene(4);
            }

        }

        if (col.gameObject.tag == "canver")
        {
            can += 10;
            if (can > 100)
            {
                can = 100;
            }
            canText.text = "Can " + can;
            col.GetComponent<PolygonCollider2D>().enabled = false;
            // Destroy(col.gameObject,3);
            col.GetComponent<canVer>().enabled = true;
        }


        if (col.gameObject.tag == "altin")
        {
            altinSayac++;
            altinText.text = "Adalet Puanı " + altinSayac + " /20 ";
            Destroy(col.gameObject);
        }


        if (col.gameObject.tag == "su")
        {
            can =0;
        }

        if (col.gameObject.tag == "alttaban")
        {
            can = 0;
        }

        if (col.gameObject.tag == "sonpozisyon")
        {
            ekremVec = ekrem.transform.position;
        }


        if (col.gameObject.tag == "kursun")
        {
            can--;
            canText.text = "Can " + can;
            Destroy(col.gameObject);
        }
   

    }

   


    void kameraKontrol()
    {
        kameraSonPos = kameraİlkPos + transform.position;
        //kamera.transform.position = kameraSonPos;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.1f);
    }




    public void butonSec(int gelenButton)
    {
        if (gelenButton == 0)
        {
            for (int i = 1; i < pauseAyar.transform.childCount; i++)
            {
                pauseAyar.transform.GetChild(0).GetComponent<Button>().interactable = false;
                pauseAyar.transform.GetChild(i).gameObject.SetActive(true);
                pauseAyar.transform.GetChild(i).GetComponent<Button>().interactable = true;
                Time.timeScale = 0;
                audioSource.Pause();

            }
        }

        if (gelenButton == 1)
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


        if (gelenButton == 2)
        {
            for (int i = 1; i < pauseAyar.transform.childCount; i++)
            {
                pauseAyar.transform.GetChild(0).GetComponent<Button>().interactable = true;
                pauseAyar.transform.GetChild(i).gameObject.SetActive(false);
                pauseAyar.transform.GetChild(i).GetComponent<Button>().interactable = false;
                Time.timeScale = 1;
                audioSource.UnPause();

            }

        }

        if (gelenButton == 3)
        {

            SceneManager.LoadScene(0);

        }

        if (gelenButton == 4)
        {


            ekrem.transform.position = ekremVec;

            for (int i = 0; i < kontrolAckapa.transform.childCount; i++)
            {
                kontrolAckapa.transform.GetChild(i).gameObject.SetActive(true);

                kontrolAckapa.transform.GetChild(i).GetComponent<Button>().interactable = true;

            }


            canText.text = "Can 100";
            can = 100;
            canText.enabled = true;
            altinText.enabled = true;
            siyahArkaPlan.gameObject.SetActive(false);
            siyahArkaPlanSayaci += 0.03f;
            siyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayaci);
            Time.timeScale = 1;
            for (int i = 0; i < reklamAyar.transform.childCount; i++)
            {
                reklamAyar.transform.GetChild(i).gameObject.SetActive(false);
                reklamAyar.transform.GetChild(i).GetComponent<Button>().interactable = false;

            }
            siyahArkaPlanSayaci = 0;
            reklamizlendi = true;

        }

        if (gelenButton == 5)
        {
            SceneManager.LoadScene(0);




        }

        if (gelenButton == 9)
        {
            yazi.transform.gameObject.SetActive(false);
            tamambutton.transform.gameObject.SetActive(false);
            tamambutton.transform.GetComponent<Button>().interactable = false;


        }






    }


 

}












