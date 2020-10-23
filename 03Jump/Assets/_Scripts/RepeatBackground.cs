using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] //Es obligatorio que tenga un BoxCollider

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; //para guarar la posición inicial
    private float repeatWidth; //para ver con cuanta anchura se ha de repetir
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //guardamos la posición inicial
        repeatWidth = GetComponent<BoxCollider>().size.x/2; // Gracias a que estamos usando un BoxCollider sobre un fondo geometricamente perfecto, el BoxCollider
        //se ajusta perfecto a dicho fondo, gracias a ello podemos acceder de forma exacta a la mitad de el fondo, pues va a ser la mitad de la anchura que nos 
        //indica el BoxCollider.
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.x - transform.position.x > repeatWidth) //Como sabemos exactamente la posición de la mitad de el fondo, que exactamente donde se repite
        // podemos indicar que en cuanto pase de esa mitad vuelva a el punto inicial
        {
            transform.position = startPos; //vuelve a el punto inicial
        }
    }
}
