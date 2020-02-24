using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour
{

    public GameObject Jogador;
    public float velocidade = 5;
    public int Dano = 20;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        Vector3 direcao = Jogador.transform.position - transform.position;

        Quaternion novaRotação = Quaternion.LookRotation(direcao);
        GetComponent<Rigidbody>().MoveRotation(novaRotação);

        if(distancia > 2.5)
        {            
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + direcao.normalized * velocidade * Time.deltaTime);

            GetComponent<Animator>().SetBool("Atacando", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Atacando", true);
        }
    }

    void AtacaJogador()
    {
        Jogador.GetComponent<ControlaJogador>().TomarDano(Dano);
    }

}
