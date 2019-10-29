using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol3 : MonoBehaviour
{
    dusmanKontrol3 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman3").GetComponent<dusmanKontrol3>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}