using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour
{

    [SerializeField] private GameObject perro;

    private float contador= 1;

    [SerializeField] private float tiempoEspera = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador>tiempoEspera)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(perro, transform.position, transform.rotation);
                contador = 0;
            }
        }
        
    }
}
