using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel
{
   
    Vector3 direcao;
    public LayerMask FloorMask;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    private void Start()
    {
        
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate(){

        // movimento pelo rigid body
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        // rotação para o mouse
        meuMovimentoJogador.RotacaoJogador(FloorMask);

    }

    public void TomarDano (int dano) {
        
        statusJogador.Vida -= dano;

        scriptControlaInterface.AtualizarSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        
        if(statusJogador.Vida <= 0) 
        {
            Morrer();    
        }
    }

    public void Morrer ()
    {
        scriptControlaInterface.GameOver();
    }

}
