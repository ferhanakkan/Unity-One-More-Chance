using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suAnimasyon : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonKareleriSayaci = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        zaman += Time.deltaTime;
        if (zaman > 0.05f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];


            if (animasyonKareleri.Length == animasyonKareleriSayaci)
            {
                animasyonKareleriSayaci = 0;
            }
            zaman = 0;
        }
    }
}
