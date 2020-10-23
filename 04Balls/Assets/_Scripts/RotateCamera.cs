using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed=10f; //Variable para controlar la velocidad de rotación de la cámara
    
    private float horizontalInput; //variable para guardar el input horizontal
    

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal"); //asignamos la variable con el Input de Unity asociado a el movimiento Horizontal
        transform.Rotate(Vector3.up, horizontalInput*rotationSpeed*Time.deltaTime); //movemos la cámara alrededor de  un vector hacia arriba, y 
        //dicho movimiento depende de: El input horizontal, le velocidad con al que gira y el tiempo 
        
        

    }
}
