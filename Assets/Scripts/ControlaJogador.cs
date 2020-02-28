using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
   
    Vector3 direcao;
    public float Velocidade = 10;
    public int Vida = 100;
    public LayerMask FloorMask;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;

    private void Start()
    {
        Time.timeScale = 1;
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);

        if(Vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("motel_scene");
            }
        }

    }

    void FixedUpdate(){

        // movimento pelo rigid body
        meuMovimentoJogador.Movimentar(direcao, Velocidade);

        // rotação para o mouse
        meuMovimentoJogador.RotacaoJogador(FloorMask);

    }

    public void TomarDano (int dano) {
        
        Vida -= dano;

        scriptControlaInterface.AtualizarSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        
        if(Vida <= 0) {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }

}
