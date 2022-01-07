using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform[] transforms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //kameranın duracağı pozisyon
        transform.position = transforms[0].position;
        //kameranın sürekli bakacağı pozisyon
        transform.LookAt(transforms[1].position);
    }
}
