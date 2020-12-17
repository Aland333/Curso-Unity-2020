using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Para poder acceder a la maya de navegación


[RequireComponent(typeof(NavMeshAgent))] //Obligamos a este script a que tenga un NavMeshAgent
public class WaypointPatrol : MonoBehaviour
{

    private NavMeshAgent navMeshAgent; //Para almacenar un  NavMeshAgent
    [SerializeField] private Transform[] wayPoints; //Creamos un Array para que recoga los puntos de ruta, hemos de añadirlos a mano desde el inspector
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); //De esta forma accedemos a la componente NavMeshAgent que ha de tener el GameObject a el cual se le ha asignado este script
        navMeshAgent.SetDestination(wayPoints[0].position); //Manda el objeto que tiene este script a la posición 0 guardada en el array wayPoints
    }


}
