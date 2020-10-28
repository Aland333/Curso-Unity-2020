using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody _rigidbody; //Como vamos a tgrabajar con físicas necesitamos acceder a el rigidbody de los objetos, por ello creamos una variable de tipo
    //Rigidbody
    [SerializeField] private float maxForce = 17f,  //creamos variables para controlar: la fuerza, el torque, el limite de el spawneo
                                    minForce = 13f,
                                    maxTorque = 10, 
                                    minTorque = -10,
                                    rangeX = 4,
                                    rangeY = -6;

    private GameManager gameManager; //Creamos una variable de tipo GameManager
    [SerializeField]private int pointValue; //Creamos una variable de tipo int que se encarga de indicar el valor de puntos de cada objeto, para modificar dicho valor
    //vamos a el prefab de objeto en Unity, luego en el inspector accedemos a el script y modificamos la variable de la forma que querámos
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Accedemos al rigidbody de le objeto y lo guardamos en la variable _rigidbody
        
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse); //Añadimos una fuerza, dicha fuerza viene dada por el vector generado por RandomForce()
        //y la fuerza aplicada es de tipo impulso
        
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse); //Añadimos un torque, dicho torque en cada uno de los
        //ejes x,y,z viene dado por el método RandomTorque), y el torque se hace de tipo Impulso

        transform.position = RandomSpawnPosition(); //la posición de inicio viene dada por el método RandomSpawnPosition()

        gameManager = FindObjectOfType<GameManager>(); // Busca un GameObject que tenga el script GameManager y lo guarda en la variable gameManager
    }
    
    /// <summary>
    /// Genera un vector aleatorio en 3 dimensiones que usaremos para la fuerza hacia arriba
    /// </summary>
    /// <returns> Fuerza aleatoria para arriba entre minForce y maxForce</returns>

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Genera un valor aleatorio que usaremos para el torque
    /// </summary>
    /// <returns>Valor aleatorio entre minTorque y maxTorque</returns>
    private float RandomTorque()
    {
        return Random.Range(minTorque, maxTorque);
    }

    /// <summary>
    /// Genera un vector3 que usaremos para la spawneo de el objeto
    /// </summary>
    /// <returns>Un vector3 cuya posición en x varía entre -rangeX y rangeX, su posicicón en y es rangeY y posicion en z= 0</returns>
    
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-rangeX,rangeX), rangeY);
    }

    private void OnMouseDown() //Hemos cread una función que tiene en cuenta el evento de hacer click
    {
        Destroy(gameObject); //Cuando hacemos click destruye el gameobject
        gameManager.UpdateScore(pointValue); //se llama a la función UpdateScore y se le pasa un valor almacenado en pointValue
    }

    private void OnTriggerEnter(Collider other) //Cauando entre en un trigger
    {
        if (other.CompareTag("KillZone")) //en este caso en el gameobject que tiene el tag KillZone
        {
            Destroy(gameObject); //destruimos el gameobject
            if (pointValue > 0) //si da más de 0 puntos, es decir si no es una bomba
            {
                gameManager.UpdateScore(-10);
            }
            
        }
    
    }
}
