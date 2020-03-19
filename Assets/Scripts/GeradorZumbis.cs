using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    float contadorTempo = 0;
    public float tempoGerar = 1;
    public LayerMask LayerZumbi;
    private float distanciaGeracao = 3;
    public float DistanciaJogadorGeracao = 20;
    private GameObject jogador;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, jogador.transform.position) > DistanciaJogadorGeracao)
        {
            contadorTempo += Time.deltaTime;

            if(contadorTempo >= tempoGerar)
            {
            StartCoroutine(GerarNovoZumbi());
            contadorTempo = 0;
            }
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaGeracao);
    }

    IEnumerator GerarNovoZumbi ()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
