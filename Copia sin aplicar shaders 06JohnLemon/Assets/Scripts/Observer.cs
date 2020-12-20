using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))] // De esta forma lo obligamos a tener un CapsuleCollider
public class Observer : MonoBehaviour
{
    //La base de este script es verificar que el transform de el jugador está a cierta distancia o que cumple ciertas condiciones
    
    [SerializeField] private Transform player; //de esta forma desde el inspector le indicamos que es lo que ha de observar

    private bool isPlayerInRange = false;

    [SerializeField] private GameEnding gameEnding; //es una clase de Unity que nosotros hemos creado antes al crear un script c# con dicho nombre
    
    private void OnTriggerEnter(Collider other) //Cuando entre el trigger
    {
        if (other.transform == player) //si es el jugador
        {
            isPlayerInRange = true; //ponemos la variable a true
        }
    }

    private void OnTriggerExit(Collider other) //si sale de el trigger
    {
        if (other.transform == player) //si es el jugador
        {
            isPlayerInRange = false; //ponemos la variable a false
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up; //El vector de dirección es la resta de la posición de el jugador menos la posición de el gameobject que lleva este
            //script, luego le sumamos 1 hacia arriba porque el origen de John lemon son los pies y no el torso y de esta forma apuntamos a el torso.
            
            Ray ray = new Ray(transform.position, direction); //creamos un rayo o ray, lo vamos a utilizar para que a la hora de buscar a el jugador, controlar si hay una pared entre ellos o no
            // para crearlo le pasamos por parámetro 2 valores, la posición de inicio y hacia donde va dirigido, en esta ocasión empieza en la posición donde está asignado este script y va en
            // la dirección que hemos calculado con el vector direction
            
            Debug.DrawRay(transform.position, direction, Color.green, Time.deltaTime,true); //Dibuja un rayo que aparece si tenemos gizmos activado, dicho rayo se dibuja
            // a partir de la posición de este script, en la dirección que viene dada por la variable direction, de un color verde, se actualiza en tiempo real y tiene un dephyTest en true
            //que esto significa que por cada objeto que atraviesa se ve menos dicho rayo
            
            RaycastHit raycastHit; //creamos una variable de tipo RaycastHit

            if (Physics.Raycast(ray, out raycastHit)) //con esto lo que hacemos es que el motor de fisica lance el rayo que hemos creado, si choca con algo será true y si no será false. En esta 
            //ocasión al hacer .Raycast(ray, out raycastHit) lo que hacemos es que haga el rayCast con el rayo ray, y que con lo que choque se guarde en la varialbe raycastHit
            {
                if (raycastHit.collider.transform == player) //si el raycasthit ha tocado a un collider  y ese collider tiene por transform  la posición rotación de John Lemon, entonces hemos
                //chocado contra nuestro personaje
                {
                    gameEnding.CatchPlayer(); //gracias a que hemos guardado en la variable gameEnding el acceso a el script de dicho nombre, podemos hacer una llamada a sus funciones
                }
            }
        }
    }

    private void OnDrawGizmos() //OnDrawnGizmos es un método de Unity que lo que hace es permitirnos dibujar nuestros propios gizmos
    {
        Gizmos.color = Color.cyan; //todos los gizmos apartir de ahora serán color cyan
        Gizmos.DrawSphere(transform.position,0.1f); //dibujamos una esfera en la posición de el gameObject asignado a este script
        Gizmos.color = Color.red; //todos los gizmos a partir de ahora serán de color rojo
        Gizmos.DrawLine(transform.position, player.position); //dibujamos una linea que va desde la posición de el gameobject de este script a la posición de el jugador
    }
}
