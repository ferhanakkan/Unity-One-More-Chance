using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol8 : MonoBehaviour
{
    dusmanKontrol8 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman8").GetComponent<dusmanKontrol8>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
