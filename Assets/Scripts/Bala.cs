using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    public AudioClip SomDeMorte;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao){
        
        if(objetoDeColisao.tag == "Inimigo"){
            Destroy(objetoDeColisao.gameObject);
            ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        }

        Destroy(gameObject);

    }
}
