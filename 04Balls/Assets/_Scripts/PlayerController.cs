using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _rigidbody; //Para acceder al rigidbody de el objeto que tenga este script
    [SerializeField] private float moveForce =1; //Para controlar la fuerza
    private float forwardInput; //para asignar el input
    [SerializeField] private GameObject focalPoint; //Para encontrar el GameObject focalPoint apartir de el cual vamos a obtener el movimiento, se lo arrastraremos
    //en Unity
    [SerializeField] private bool hasPowerUp; //las variables booleanas por defectos son false
    [SerializeField] private float powerUpForce; //Para controlar la fuerza con el que el powerUp ejerce la repulsión
    [SerializeField] private float powerUpTime;
    [SerializeField] private GameObject[] powerUpIndicators;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); //Guardamos el acceso a el rigidbody en la componente _rigidBody
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical"); //Guardamos el acceso a el Input vertical en la variable forwwardInput
        _rigidbody.AddForce(focalPoint.transform.forward*moveForce*forwardInput ); //la fuerza que hará que el jugador o en este caso la pelota se mueva
        // va a depender de el vector forward de el focalpoint que recordamos que es el punto al rededor de el cual giramos la cámara, es decir
        // el giró de la cámara influye en la posición que la bola entienda como ir para a delante. También unfluira el input, y el valor de la fuerza
        // que le hemos dado. Recordamos que si no ponemos nada, la puerza será de tipo Force, es decir dependerá de el tiempo que hemos estado aplicando
        // la fuerza y también de la masa de la bola.

        foreach (GameObject indicador in powerUpIndicators)
        {
            indicador.transform.position = this.transform.position + 0.5f * Vector3.down; //la posición de los gameobject de los anillos es la posición de el jugador, pero un poco 0.5 unidades para abajo
        }

        if (this.transform.position.y < -10)
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) 
        {

            hasPowerUp = true;  //Cambiamos la variable a de tener un powerUp a true
            Destroy(other.gameObject); // destruimos el otro objeto con el que el jugador ha chocado
            StartCoroutine(PowerUpCountdowm()); //Se empieza la corutina PowerUpCountdown

        }
    }

    private void OnCollisionEnter(Collision collision) //cuando haya una colisión
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp) //si la colisión es con algo con el tag "Enemy" y si el booleano hasPowerUp es true
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); // Accedemos a los componentes de el gameObject de el cual
            //tenemos una colision y destro de este accedemos al Rigidbody, guardamos dicho Rigidbody en la variable enemyRigidbody

            Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position; //Creamos una dirección, esta es la posición
            //de el enemigo, menos la posición de el jugador
            enemyRigidbody.AddForce(awayFromPlayer*powerUpForce, ForceMode.Impulse); //usamos el ForceMode.Impulse, porque queremos hacer toda la fuerza en 
            //un instante, la dirección se ha obtenido anteriormente y el valor de la fuerza depende la variable que hemos creado

            Debug.Log("se activa la colision");
        }
    }


    IEnumerator PowerUpCountdowm()
    {
        for(int i = 0; i< powerUpIndicators.Length; i++)
        {
            powerUpIndicators[i].gameObject.SetActive(true); //activamos el anillo 
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length); //una vez que haya pasado el tiempo dividido en el número de anillos guardados
            powerUpIndicators[i].gameObject.SetActive(false); //desactivamos el anillo
            
        }
  
        hasPowerUp = false; //Pone al variable encargada de registrar si tiene el powerUp a false, es decir anula el efecto de le powerUp
        

    }
}
    