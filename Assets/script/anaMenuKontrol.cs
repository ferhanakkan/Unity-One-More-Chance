using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anaMenuKontrol : MonoBehaviour
{

    GameObject leveller;
    GameObject kilitlerKapali;
    GameObject kilitlerAcik;
    GameObject sesackapa;

    public Text hazirlayan;


    bool ackapa = true;
    bool ackapahazirliyan = true;
    bool muzik = true;


    public AudioClip musicClip;
    public AudioSource audioSource;

    int sesKontrol = 1;


    void Start()
    {

        sesackapa = GameObject.Find("sesackapa");
        audioSource.clip = musicClip;
        audioSource.Play();


        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            sesKontrol = 1;


        }
       
         else
        {
          
            sesKontrol = PlayerPrefs.GetInt("ses");


            if(sesKontrol==1)
            {
                Debug.Log("seskont==1");
                sesackapa.transform.GetChild(0).gameObject.SetActive(true);
                sesackapa.transform.GetChild(1).gameObject.SetActive(false);

            }

            else if(sesKontrol==0)
            {

                Debug.Log("seskont==0");
                sesackapa.transform.GetChild(0).gameObject.SetActive(false);
                sesackapa.transform.GetChild(1).gameObject.SetActive(true);
                audioSource.mute = !audioSource.mute;

            }

        }
    



        hazirlayan.gameObject.SetActive(false);

        leveller = GameObject.Find("leveller");
        kilitlerKapali = GameObject.Find("kilitlerkapali");
        kilitlerAcik = GameObject.Find("kilitleracik");


      


      
        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);

        }

        for (int i = 0; i < kilitlerAcik.transform.childCount; i++)
        {
            kilitlerAcik.transform.GetChild(i).gameObject.SetActive(false);
            kilitlerKapali.transform.GetChild(i).gameObject.SetActive(false);

        }

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;

        }


    }

    public void butonSec(int gelenButton)
    {


        if (gelenButton==1)
        {
            SceneManager.LoadScene(1);
        }


        else if(gelenButton==2)
        {

            if (ackapa)
            {


                for (int i = 0; i < kilitlerAcik.transform.childCount; i++)
                {
                    kilitlerKapali.transform.GetChild(i).gameObject.SetActive(true);
                    leveller.transform.GetChild(i).gameObject.SetActive(true);

                }

                for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
                {
                   
                    kilitlerKapali.transform.GetChild(i).gameObject.SetActive(false);
                    kilitlerAcik.transform.GetChild(i).gameObject.SetActive(true);


                }

                ackapa = false;
            }

            else if(!ackapa)
            {

                for (int i = 0; i < kilitlerAcik.transform.childCount; i++)
                {
                    kilitlerAcik.transform.GetChild(i).gameObject.SetActive(false);
                    kilitlerKapali.transform.GetChild(i).gameObject.SetActive(false);
                    leveller.transform.GetChild(i).gameObject.SetActive(false);

                }
                ackapa = true;


            }


        }

        else if (gelenButton == 3)
        {
            Application.Quit();
         
        }

        else if (gelenButton == 4)
        {   


            if (ackapahazirliyan)
            {
                hazirlayan.gameObject.SetActive(true);
                ackapahazirliyan = false;
            }

            else if(!ackapahazirliyan)
            {
                hazirlayan.gameObject.SetActive(false);
                ackapahazirliyan = true;
            }
        }

        else if (gelenButton == 5)
        {

           
            if(sesKontrol==1)
            {
                sesKontrol = 0;
                PlayerPrefs.SetInt("ses", sesKontrol);

                sesackapa.transform.GetChild(1).gameObject.SetActive(true);
                sesackapa.transform.GetChild(0).gameObject.SetActive(false);
                audioSource.mute = !audioSource.mute;

            }

            else if(sesKontrol==0)
            {
                sesKontrol = 1;
                PlayerPrefs.SetInt("ses", sesKontrol);

                sesackapa.transform.GetChild(0).gameObject.SetActive(true);
                sesackapa.transform.GetChild(1).gameObject.SetActive(false);
                audioSource.mute = !audioSource.mute;

            }


        }



    }

    public void levellerButton(int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }



}
