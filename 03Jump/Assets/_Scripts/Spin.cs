using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float translateSpeed = 1; //Para darle una velocidad de desplazamiento
    public float rotateSpeed = 60; //Para darle una velocidad de rotación
    
    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.left * translateSpeed * Time.deltaTime; //Hemos de usar .localPosition, por que si no cada vez que vaya rotando el vector3.left va a cambiando.
        //Cuando usamos .localPosition, hacemos referencia a la posición de el objeto respecto a to do el juego y asi no tiene en cuenta sus propios ejes.
        //esta línea de código no usa ningún método lo que hace es que va sumando a la variable de posición unos valores
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime); //Vamos cambiando la rotación, según un vector, una velocidad y el tiempo. Es importante saber que cuando un 
        //objeto rota esa rotación también lo mueve en una dirección.
    }
}
