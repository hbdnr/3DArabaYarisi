using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Önden veya Arkadan çekişin ayarlanması
public enum Axel
{
    On,
    Arka
}

//liste tanımlama, Sistemde dolaşmasını sağlayabilmek için srializable eklendi.
[Serializable]
public struct Whell
{
    public GameObject model;
    public WheelCollider wheelCollider;
    public Axel axel;
}


public class ArabaKontrol : MonoBehaviour
{
    //private olarak tanımlanan değişkenlerin inspector tarafından görüntülenmesini sağlar. 
    [SerializeField]
    private float maksimumHizlanma = 250f;
    [SerializeField]
    private float donusHassasiyeti = 1f;
    [SerializeField]
    private float maksimumDonusAcisi = 45f;
    [SerializeField]
    private List<Whell> whells;
    private float inputX, inputY;
    public Vector3 merkezKutle;

    public GameObject ArkaFar;
    public Material[] ArkaFarMaterialler;

    public GameObject[] onFarIsiklari;
    bool onFarAcikMi;

    // Start is called before the first frame update
    void Start()
    {
        onFarAcikMi = false;
        GetComponent<Rigidbody>().centerOfMass = merkezKutle;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            onFarAcikMi = !onFarAcikMi;
            onFarIsiklari[0].SetActive(onFarAcikMi);
            onFarIsiklari[1].SetActive(onFarAcikMi);
        }
        //Objenin dönüşlerinin anlık olarak sürekli tespit edilmesi
        HareketYonu();
        TekerlerinDonusu();
    }


    private void LateUpdate()
    {
        Move();
        Turn();
        Fren();
    }

    private void Move()
    {
        foreach (var whell in whells)
        {
            //ileri ve geri yönde hızlanmanın ayarlanması
            whell.wheelCollider.motorTorque = inputY * maksimumHizlanma * 400 * Time.deltaTime;
        }
    }

    void Turn()
    {
        foreach (var whell in whells)
        {
            if (whell.axel == Axel.On)
            {
                var _steerAngle = inputX * donusHassasiyeti * maksimumDonusAcisi;
                //daha yumuşak bir dönüş sağlamak
                whell.wheelCollider.steerAngle = Mathf.Lerp(whell.wheelCollider.steerAngle, _steerAngle, .1f);
            }


        }
    }

    private void HareketYonu()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    void TekerlerinDonusu()
    {
        foreach (var whell in whells)
        {
            Quaternion _rot;
            Vector3 _pos;
            //whell colliderın o anki pozisyonu
            whell.wheelCollider.GetWorldPose(out _pos, out _rot);
            //Debug.Log("pos:   " + _pos + " Rot:   " + _rot);
            whell.model.transform.position = _pos;
            whell.model.transform.rotation = _rot;


        }
    }

    public void Fren()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   //space tuşuna basıldığı için rendering modu transparent olan materyal tanımlandı.
            ArkaFar.GetComponent<MeshRenderer>().material = ArkaFarMaterialler[1];
            //bütün tekerler (wheel collider lı halleri) dolaşılacak
            foreach (var whell in whells)
                //brakeTorque değeri yüksek verildi bu sayede durma işlemi gerçekleşti
                whell.wheelCollider.brakeTorque = 7500;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {   //space tuşuna bırakıldığı için rendering modu opaque olan materyal tanımlandı.
            ArkaFar.GetComponent<MeshRenderer>().material = ArkaFarMaterialler[0];
            foreach (var whell in whells)
                //brakeTorque değeri tekrar sıfırlandı. Sıfırlanmazsa araba artık hareket etmez.
                whell.wheelCollider.brakeTorque = 0;
        }
    }
}
