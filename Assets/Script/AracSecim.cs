using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AracSecim : MonoBehaviour
{
    public GameObject[] Arabalar;
    public Text arabaAdi;
    public Text arabaHiz;
    public Text arabaManevra;
    public ParticleSystem gecisEfekti;
    int aktifArac=0;


    // Start is called before the first frame update
    void Start()
    {
        Arabalar[aktifArac].SetActive(true);
        AracBilgi();
    }


    // Update is called once per frame
    void Update()
    {
        index();
        Arabalar[aktifArac].SetActive(true);

        
    }

    public void Ileri()
    {
        arabaAdi.text = "";
        Arabalar[aktifArac].SetActive(false);
        aktifArac++;
        index();
        AracBilgi();
        gecisEfekti.Play();
    }
    public void Geri()
    {
        arabaAdi.text = "";
        Arabalar[aktifArac].SetActive(false);
        aktifArac--;
        index();
        AracBilgi();
        gecisEfekti.Play();
    }
    void index()
    {
        if (aktifArac < 0)
            aktifArac = Arabalar.Length - 1;
        if (aktifArac > Arabalar.Length - 1)
            aktifArac = 0;
    }

    void AracBilgi()
    {
        arabaAdi.text ="Adı: "+ Arabalar[aktifArac].gameObject.GetComponent<AracBilgileri>().aracAdi;
        arabaHiz.text = "Hız: " + Arabalar[aktifArac].gameObject.GetComponent<AracBilgileri>().aracHiz.ToString();
        arabaManevra.text = "Manevra: " + Arabalar[aktifArac].gameObject.GetComponent<AracBilgileri>().aracDonus.ToString();
    }

    public void Basla()
    {
        PlayerPrefs.SetInt("secilen", aktifArac);
        SceneManager.LoadScene("Level1");
    }
}
