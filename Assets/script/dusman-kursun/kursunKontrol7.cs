using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol7 : MonoBehaviour
{
    dusmanKontrol7 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman7").GetComponent<dusmanKontrol7>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon() * 1000);


    }



}
