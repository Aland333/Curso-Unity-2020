using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Este Script se encarga de configurar algo que persigue a otra cosa, en concreto lo configuraremos para que la cámara siga al jugador. Por ello la variable que necesita
    //este script será la de el GameObject al que va a seguir.
    
   public GameObject player; //creamos una variable de tipo GameObject, a la cual luego en Unity le asignaremos dicho GameObject que será el jugar o en este caso en
      //concreto, el coche que controla el jugador

    [SerializeField]
    private Vector3 offset = new Vector3(0,4,-7 ); //instanciamos el objeto  de tipo Vector3  que nos marcará la distancia a la que está la cámara. Se instancia el objeto
        //porque Vector3 no es una variable como las básicas que sabemos (int, boolean, string, etc) viene de una clase creada por Unity que nos marca un vector de 
        // 3 dimensiones, por ello decimos que instanciamos un objeto y no que damos un valor a variable. Cuando instanciamos un objeto lo hacemos de la siguiente forma
        // Vector3 nombre = new Vector3(x,y,z)        si no ponemos nada dentro de el paréntesis tenemos un vector3 vacio, lo hemos construido sin pasar parámetros


        private void Update() //Hay que tener en cuenta que poner private y no poner nada es lo mismo
      {
          this.transform.position = player.transform.position + offset; //La posición de la cámara va a ser la misma que la posición del jugador + la variable offset
      }
}


