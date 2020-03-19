using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel
{

    public GameObject Jogador;
    public int Dano = 20;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float temporEntrePosicoes = 4;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        AleatorizarZumbis();

        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        
        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 15)
        {
            Vagar();
        }
        else if(distancia > 2.5)
        {            
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            animacaoInimigo.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += temporEntrePosicoes;
        }

        bool chegou = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if(chegou == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao ()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador()
    {
        Jogador.GetComponent<ControlaJogador>().TomarDano(Dano);
    }

    void AleatorizarZumbis ()
    {       
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
    }

}
