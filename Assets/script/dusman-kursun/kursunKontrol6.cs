using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol6 : MonoBehaviour
{
    dusmanKontrol6 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman6").GetComponent<dusmanKontrol6>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
