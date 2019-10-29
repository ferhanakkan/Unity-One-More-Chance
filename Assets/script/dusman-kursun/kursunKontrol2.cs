using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol2 : MonoBehaviour
{
    dusmanKontrol2 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman2").GetComponent<dusmanKontrol2>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
