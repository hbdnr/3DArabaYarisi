using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraGecisKontrol : MonoBehaviour
{
    public GameObject[] Kameralar;
    int aktifkam = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E))
        {
            KameraKapat();
            aktifkam++;
            if (aktifkam > Kameralar.Length-1)
            {
                aktifkam = 0;
            }
        }
        Kameralar[aktifkam].SetActive(true);
    }

    public void KameraKapat()
    {
        foreach (var item in Kameralar)
        {
            item.SetActive(false);
        }
    }
}
