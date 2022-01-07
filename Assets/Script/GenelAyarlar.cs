using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using TMPro;

public class GenelAyarlar : MonoBehaviour
{
    public GameObject[] Arabalar;
    public GameObject baslangicNoktasi;
    public GameObject[] yzBaslangicNoktalari;
    public GameObject[] yzAraclari;

    public List<GameObject> olusanAraba = new List<GameObject>();
    public TextMeshProUGUI sayacText;
    float sure = 3f;
    public AudioSource sayacSes;
    void Start()
    {

        sayacSes = GetComponent<AudioSource>();
        sayacText.text = sure.ToString();


        GameObject arabam = Instantiate(Arabalar[PlayerPrefs.GetInt("secilen")],baslangicNoktasi.transform.position,baslangicNoktasi.transform.rotation);
        //transform find objenin alt çocuklarını yakalar
        
        //eğer bir objeyi sürekli olarak bulman gerekiyorsa tag yapısı daha iyi ancak 1 defa buacaksan sadece gameobject find yapısınıda kullanabilirsin
        GameObject.Find("Main Camera").GetComponent<KameraKontrol>().transforms[0] = arabam.transform.Find("poz");
        GameObject.Find("Main Camera").GetComponent<KameraKontrol>().transforms[1] = arabam.transform.Find("pivot");
        GameObject.Find("OyunKontrol").GetComponent<KameraGecisKontrol>().Kameralar[1] = arabam.transform.Find("kameralar/icgorunum").gameObject;
        GameObject.Find("OyunKontrol").GetComponent<KameraGecisKontrol>().Kameralar[2] = arabam.transform.Find("kameralar/ongorunum").gameObject;

        for (int i = 0; i < 2; i++)
        {
            //dizi boyutundan 1 eksik olarak değer üretilecek (range kuralı)
            int randomDeger = Random.Range(0, yzAraclari.Length);
            //yzAraclari dizisinin randomDeger 'inci arabası, tanımlanan yzBaslangicNoktalarindan i 'inci pozisyon ve rotasyonda oluşturulacak
            GameObject olusanArac = Instantiate(yzAraclari[randomDeger], yzBaslangicNoktalari[i].transform.position, yzBaslangicNoktalari[i].transform.rotation);
            //hangi aracın hangi güzergahı kullanacağı sorunu oluşan noktanın indisi alınarak YZController scriptindeki yzBaslangicIndex değişeknine atandı. 
            olusanArac.GetComponent<YZController>().yzBaslangicIndex = i;
        }

    }

    //gelen objelerin listeye atanması
    public void BenSuAraba(GameObject gameObject)
    {
        olusanAraba.Add(gameObject);
    }

    bool islemYap = true;
    
    private void Update()
    {

       /* sure -= Time.deltaTime;

        sayacText.text = Mathf.Round(sure).ToString();

        Debug.Log("sure  " + sure);
        if (sure < 0)
        {
            Debug.Log("başla");
        }*/

        if (islemYap)
        {

            Debug.Log("süre:   " + sure);

            //süre değeri hesaplandı ve değişkene yuvarlama işleminden sonra atandı
            sure -= Time.deltaTime;
            sayacText.text = Mathf.Round(sure).ToString();
            //ses dosyasının yürütülmesi
            sayacSes.Play();
            if (sure < 0)
            {
                //
                foreach (var item in olusanAraba)
                {
                    //obje ismi MyCar ise kontrolü sonlandıracak
                    if (item.gameObject.name == "MyCar")
                        item.GetComponentInParent<CarUserControl>().enabled = true;
                    //değilse sürüş yeteneğini kapatacak.
                    else
                        item.GetComponentInParent<CarAIControl>().m_Driving = true;
                }
                islemYap = false;
                sayacText.enabled = false;
            }
        }
        
        
    }
}
