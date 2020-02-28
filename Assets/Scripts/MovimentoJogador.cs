using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacaoJogador (LayerMask FloorMask)
    {
        // facing quando se movimenta
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, FloorMask)){
            Vector3 posicaoMira = impacto.point - transform.position;
            posicaoMira.y = transform.position.y;
            
            Rotacionar(posicaoMira);
        }
    }
}
