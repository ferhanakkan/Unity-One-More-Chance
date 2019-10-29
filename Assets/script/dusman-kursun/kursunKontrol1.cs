using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kursunKontrol1 : MonoBehaviour
{
    dusmanKontrol1 dusman;
    Rigidbody2D fizik;

    void Start()
    {
        dusman = GameObject.FindGameObjectWithTag("dusman1").GetComponent<dusmanKontrol1>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(dusman.getYon()*1000);


    }



}
