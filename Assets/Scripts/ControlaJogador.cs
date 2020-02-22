using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
   
    public float Velocidade = 10;
    Vector3 direcao;
    public LayerMask FloorMask;
    public GameObject TextoGameOver;
    public bool vidaJogador = true;
    public int Vida = 100;

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        // transform.Translate(direcao * Velocidade * Time.deltaTime);

        // trocar a animação quando se move
        if(direcao != Vector3.zero){
            GetComponent<Animator>().SetBool("Movendo", true);
        }
        else{
            GetComponent<Animator>().SetBool("Movendo", false);
        }

        if(vidaJogador == false)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("motel_scene");
            }
        }

    }

    void FixedUpdate(){

        // movimento pelo rigid body
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao * Velocidade * Time.deltaTime));

        // facing quando se movimenta
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, FloorMask)){
            Vector3 posicaoMira = impacto.point - transform.position;
            posicaoMira.y = transform.position.y;
            
            Quaternion looking = Quaternion.LookRotation(posicaoMira);
            GetComponent<Rigidbody>().MoveRotation(looking);
        }

    }

    public void TomarDano () {
        
        Vida -= 25;
        
        if(Vida <= 0) {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
            vidaJogador = false;
        
        }
    }

}
