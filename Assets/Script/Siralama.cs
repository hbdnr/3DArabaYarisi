using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siralama : MonoBehaviour
{
    //sahnede oluşturulan ardaşık objelerin değerlerini turan değişken
    public int yonBilgi = 1;
    AracSiralamaYonetici aracSiralama;
    GenelAyarlar genelAyarlar;
    // Start is called before the first frame update
    void Start()
    {
        aracSiralama = GameObject.FindWithTag("OyunKontrol").GetComponent<AracSiralamaYonetici>();
        //AracSiralamaYonetici scriptinde bulunan BenSuAraba metoduna değerler gönderildi 
        aracSiralama.BenSuAraba(gameObject, yonBilgi);

        //GenelAyarlar scriptine erişerek BenSuAraba fonksiyonuna bağlı olunan objenin (İsim) gönderilmesi  
        genelAyarlar = GameObject.FindWithTag("OyunKontrol").GetComponent<GenelAyarlar>();
        genelAyarlar.BenSuAraba(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        //çarpılan objenin tagı GidisYonuObje mi?
        if (other.CompareTag("GidisYonuObje"))
        {
            //objenin isim değerini yonBilgi değişkenine dönüştürerek ata
            yonBilgi = int.Parse(other.transform.gameObject.name);
            //Listenin yeni değerlere göre güncellenmesini sağlayan ArabaSiralamaGuncelle metodunun çağırılması 
            aracSiralama.ArabaSiralamaGuncelle(gameObject, yonBilgi); 
        }
    }
}
