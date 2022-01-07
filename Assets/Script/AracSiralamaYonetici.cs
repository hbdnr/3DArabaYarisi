using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using TMPro;
using System.Linq;

//Listenin değerlerini alacağı class
public class araba
{
    public GameObject gelenAraba;
    public int yonBilgi;
    //Constructor yapıcı metod. İlk çalışan metottur.
    public araba (GameObject gelenAraba, int yonBilgi)
    {
        this.gelenAraba = gelenAraba;
        this.yonBilgi = yonBilgi;
    }
}

public class AracSiralamaYonetici : MonoBehaviour
{
    public List<araba> Arabalar = new List<araba>();
    //TMPro kütüphanesinin ekli olması gerekmektedir.
    public TextMeshProUGUI aracSira;
    public int siraNumarasi;
    //araba bilgisinin geleceği metod
    public void BenSuAraba(GameObject aracBilgi, int yonBilgi)
    {
        Arabalar.Add(new araba(aracBilgi, yonBilgi));

        if (Arabalar.Count == 3)
        {
            ArabaSiralama();
        }
    }

    //gelen değerlerin TextMeshPro objesine yazdırılması
    private void ArabaSiralama()
    {
        aracSira.text = "";
        //Arabaların yonBilgi değişkeninin değerine göre büyükten küçüğe göre sıralandırılması (Linq kütüphanesi gereklidir)
        Arabalar = Arabalar.OrderByDescending(h => h.yonBilgi).ToList();
        foreach (var item in Arabalar)
        {
            if (item.gelenAraba.name == "MyCar")
            {
                item.yonBilgi = 1;
            }
           
            aracSira.text += item.gelenAraba.name + " : " + item.yonBilgi + "/39"+ "<br>"+ "<br>";
        }
    }

    //Bilgilerin Sürekli olarak güncellenebilmesi için yeni yön değerlerinin (obje isimlerine göre) alınması
    public void ArabaSiralamaGuncelle(GameObject aracBilgi, int yonBilgi)
    {
        for (int i = 0; i < Arabalar.Count; i++)
        {
            if (Arabalar[i].gelenAraba == aracBilgi)
            {
                Arabalar[i].yonBilgi = yonBilgi;
            }
        }
        //alınan değerlere göre listenin tekrar sıralanması
        ArabaSiralama();
    }
}
