using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol9 : MonoBehaviour
{
    dusmanKontrol9 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman9").GetComponent<dusmanKontrol9>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
