using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol4 : MonoBehaviour
{
    dusmanKontrol4 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman4").GetComponent<dusmanKontrol4>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
