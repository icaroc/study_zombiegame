using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    float contadorTempo = 0;
    public float tempoGerar = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;

        if(contadorTempo >= tempoGerar){
            Instantiate(Zumbi, transform.position, transform.rotation);
            contadorTempo = 0;
        }

    }
}
