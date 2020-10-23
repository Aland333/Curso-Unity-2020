using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private Rigidbody _rigidbody; //Para acceder al rigidbody de el objeto que tenga este script
    [SerializeField] private float moveForce =1; //Para controlar la fuerza
    private GameObject player; //El enemigo va a perseguir a el jugador, y por tanto ha de tener acceso a el gameobject asociado a el jugador
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Accedemos a el rigidbody de este script
        player = GameObject.Find("Player"); //En player lo que hacemos el buscar el GameObject de nombre "Player" y lo guardamos en la variable player de este
        //script
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized; //si recordamos de el instituto el vector que une 2 puntos es la 
        //la resta de el vector posición de donde queremos ir, menos el vector posición desde donde partimos. Luego, el módulo de dicho vector varia
        // según la distancia, y como nosotros queremos que la fuerza aplicada sea indepediente de la distancia ente los objetos, hemos de normalizar
        // el vector dirección de los 2 objetos, para ello usamos .normalized
        
        _rigidbody.AddForce(moveForce*lookDirection, ForceMode.Force); //Hemos de aplicar la fuerza en la dirección guardada en la variable direction y modificaremos
        //como de fuerte es mediante la variable moveForce. Usamos el modo de fuerza .Force que es el que tiene en cuenta las masas de los objetos

        if (this.transform.position.y < -10) //si la posición de este enemigo tiene un valor en y de menos 10
        {
            Destroy(gameObject); //Destruye este gameObject
            
        }
    }
}
