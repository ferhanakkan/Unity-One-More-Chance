using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canVer : MonoBehaviour
{
    public Sprite []animasyonKareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonKareleriSayaci = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if(zaman>0.1f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            transform.localScale = new Vector3(2, 2, 2);

            if (animasyonKareleri.Length==animasyonKareleriSayaci)
            {
                animasyonKareleriSayaci = animasyonKareleri.Length - 1;
                transform.localScale = new Vector3(2, 2, 2);
            }
            zaman = 0;
        }
    }
}
