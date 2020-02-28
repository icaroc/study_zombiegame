﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    void awake ()
    {
        meuAnimator = GetComponent<Animator>();
    }

    public void Atacar (bool estado)
    {
        meuAnimator.SetBool("Atacando", estado);
    }

    public void Movimentar (float valorMovimento)
    {    
        meuAnimator.SetFloat("Movendo", valorMovimento);
    }
}