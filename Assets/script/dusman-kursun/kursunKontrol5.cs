using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol5 : MonoBehaviour
{
    dusmanKontrol5 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman5").GetComponent<dusmanKontrol5>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}