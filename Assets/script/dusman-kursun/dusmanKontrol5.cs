using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class dusmanKontrol5 : MonoBehaviour
{

    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayaci = 0;
    bool ileriMiGeriMi = true;
    GameObject karakter;
    RaycastHit2D ray;
    public LayerMask layermask;
    int hiz = 5;

    public Sprite arkaTaraf;
    public Sprite onTaraf;
    SpriteRenderer spriteRenderer;

    public GameObject kursun;
    float atesZamani = 0;


    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player");
        gidilecekNoktalar = new GameObject[transform.childCount];
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        beniGorduMu();



        if (ray.collider.tag == "Player")
        {
            if ((karakter.transform.position.y - transform.position.y) < 15 && (karakter.transform.position.x - transform.position.x) < 25 && (karakter.transform.position.y - transform.position.y) > -20 && (karakter.transform.position.x - transform.position.x) > -20)
            {
                Debug.Log("beni gördü");
                hiz = 8;
                spriteRenderer.sprite = onTaraf;
                atesEt();

            }
        }

        else
        {
            Debug.Log("beni görmedi");
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;
        }

        noktalaraGit();
    }


    void atesEt()
    {
        atesZamani += Time.deltaTime;
        if (atesZamani > Random.Range(0.5f, 1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;

        }


    }


    void beniGorduMu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.red);
    }

    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;

        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ileriMiGeriMi = false;
            }

            else if (aradakiMesafeSayaci == 0)
            {

                ileriMiGeriMi = true;
            }

            if (ileriMiGeriMi)
            {
                aradakiMesafeSayaci++;
            }

            else
            {
                aradakiMesafeSayaci--;
            }
        }


    }

    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif

}


#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol5))]
[System.Serializable]
class dusmanKontrol5Editor : Editor
{

    public override void OnInspectorGUI()
    {
        dusmanKontrol5 script = (dusmanKontrol5)target; // ust classtaki değişkenlere ulasmamı sağlar
        if (GUILayout.Button("URET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}
#endif
