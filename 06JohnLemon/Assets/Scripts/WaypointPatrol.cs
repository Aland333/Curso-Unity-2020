using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Para poder acceder a la maya de navegación


[RequireComponent(typeof(NavMeshAgent))] //Obligamos a este script a que tenga un NavMeshAgent
public class WaypointPatrol : MonoBehaviour
{

    private NavMeshAgent navMeshAgent; //Para almacenar un  NavMeshAgent
    [SerializeField] private Transform[] wayPoints; //Creamos un Array para que recoga los puntos de ruta, hemos de añadirlos a mano desde el inspector
    private int currentWaypointIndex; // Para indicar el índice el waypoint almacenado en el array anterior
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); //De esta forma accedemos a la componente NavMeshAgent que ha de tener el GameObject a el cual se le ha asignado este script
        navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position); //Manda el objeto que tiene este script a la posición dada por el valor currentWaypointIndex
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length; //Cuando llega a el punto va a el siguiente, luego para asegurarnos que una vez que ha recorrido todos no va 
            //a un índice que no existe, lo que hacemos es usar % el numero de elementos, esto significa coger el resto, asi por ejemplo si hay 5, lo que hacemos es 1 /5 el resto es 1, 2/5
            // en resto es 2, así hasta que 5/5 el resto es 0 y por lo tanto hace 1,2,3,4,0

            navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
        }
    }
}
