using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public Text TextoTempoSobrevivencia;
    public Text TextoSobrevivenciaMaxima;
    private float tempoPotuacaoSalva;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;

        tempoPotuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + "m" + segundos + "s";
        SalvarPontuacaoMaxima(minutos, segundos);
        
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("motel_scene");
    }

    void SalvarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPotuacaoSalva)
        {
            tempoPotuacaoSalva = Time.timeSinceLevelLoad;
            TextoSobrevivenciaMaxima.text = string.Format("RECORDE: {0}m{1}s", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPotuacaoSalva);
        }
        if(TextoSobrevivenciaMaxima.text == "SEM RECORDE!")
        {
            min = (int)tempoPotuacaoSalva / 60;
            seg = (int)tempoPotuacaoSalva % 60;
            TextoSobrevivenciaMaxima.text = string.Format("RECORDE: {0}m{1}s", min, seg);
        }
    }
}

