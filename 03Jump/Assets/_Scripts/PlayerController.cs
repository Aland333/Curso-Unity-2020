using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody))] // Al añadir esto estamos forzando a que donde esté este script tenga un RigidBody, se ha de poner fuera de la clase
public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb; //Creamos una variable de tipo Rigidbody
    [SerializeField] float jumpForce=10f;
    [SerializeField] private float gravityMultiplier;
    public bool isOnGround = true; //es una variable booleana que tendra en cuenta si está tocando en suelo
    public bool _gameOver = false;
    [Range(0, 1)] public float audioVolume = 1f;
    private Animator _animator; //Creamos una variable para guardar un Animator
    private float speedMultiplier = 1; //para guardar el multiplicador de la velocidad, que lueho haremos que dependa de el tiempo
    
    [SerializeField] private AudioClip jumpSound, crashSound; //Guardamos clips de audio
    private AudioSource _audio; //Para guardar un AudioSource, Necesitamos un AudioSource para reproducir sonidos, no nos vale solo con tener los AudioClip
    
    
    [SerializeField] private ParticleSystem explosion; //lo que vamos a hacer es que crear una variable de tipo ParticleSystem de acceso
    //serializefield o también hubiesemos podido hacer la variable publica, de esta forma desde el inspector de Unity podemos arrastrar una sistema de
    //partículas, para que se guarde en la variable explosion. Es importante que dicho sistema de partículas ya sea hijo de el player
    [SerializeField] private ParticleSystem dirtSplatter;
    
    
    private const string SPEED_MULTIPLIER = "SpeedMulty";
    private const string SPEED_F = "Speed_f";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string DEATH_B = "Death_b"; //Para acceder al booleano que activa la animación de muerte
    private const string DEATH_TYPE_INT = "DeathType_int"; //Para acceder a el entero que se encarga de decidir qué animación de muerte usamos
    

    public bool GameOver { get => _gameOver; } //el valor de la variable privada _gameOver, es el mismo que el de la variable pública GameOver, esto es como
    //un getter de java, el equivalente es poner 
    /*
     * {
     *    return _gameOver
     * }
     */

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Hacemos que a la variable playerRb se le asigne el Rigidbody que tiene el gameObject, esta variable pasa a ser
        // utilizable el resto de el código, 
        Physics.gravity = gravityMultiplier * new Vector3(0,-9.81f, 0); //accedemos a el menu de las físicas, en concreto a la gravedad que es un vector3, luego esta la cambiamos
        //mutliplicándola por la variable gravityMultiplier que nosotros hemos creado.
        _animator = GetComponent<Animator>(); //asginamos a la variable _animator el Animator includi en el gameObject que tiene este Script
        _animator.SetFloat(SPEED_F,1 ); //ponemos el valor de el parámetro "Speed_f" en 1
        _audio = GetComponent<AudioSource>(); //Para acceder al AudioSource
        dirtSplatter.Play();

    }

    // Update is called once per frame
    void Update()
    {

        speedMultiplier += Time.deltaTime/10; //la variable va aumentando según pasa el tiempo
        _animator.SetFloat(SPEED_MULTIPLIER, 1+(speedMultiplier*0.1f)); //a el valor de el parámetro "SpeedMulty" que está en el animator, se le asigna un float que depende
        // de Time.time que es el tiempo que ha pasado desde que se ha iniciado el juego
        
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && GameOver!= true) //si pulsamos el espacio y si la variable encargada de registrar que está tocando el suelo está
        //en true
        {
            playerRb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);  //al RigidBody le aplicamos una fuera hacia arriba de 1 * 1000 newton. Recordamos que la ecuación de la fuerza es
            // f = m * a
            /*
             *Si en el código anteior ponemos una coma, podemos eleguir entre 4 tipos de ForceMode:
             * .Acceleration: se le aplica de forma  una acelación independientemente de la masa
             * .Force: Igual que antes pero teniendo en cuenta la masa
             * 
             *     en los anteriores se le aplicado de forma paulatiba la fuerza hasta conseguir la aceleración, por lo tanto el número para conseguir
             *     la misma altura es mucho mayor que si usamos impulso
             * 
             * .Impulse: Se le aplica un impulso, es decir una fuerza durante un instante de tiempo
             * .VelocityChange: igual que impulse pero sin tener en cuenta la masa
             *
             *    Lo normal es impulso, pero hay que tener en cuenta que necesitamos un valor mucho menor 10 de tipo impulso llega a casi la misma altura que 500
             *     de tipo Force
             * 
             */

            isOnGround = false;
            
            _animator.SetTrigger(JUMP_TRIGGER); //accedemos al animator y activas el trigger o disparador para poner en marcha la animación de el salto
            _audio.PlayOneShot(jumpSound, audioVolume); //Reproduce una vez el clip de audio jumpSound, con un valor de volumen dado por la variable audioVolume
            dirtSplatter.Stop();
        }
        
        
        
    }

    private void OnCollisionEnter(Collision other) //si gameObject entra en contacto con otro que tenga collider, pero ojo, en este momento no estamos haciendo
    //distinción, puede ser que esté en contacto con el suelo o cualquier otra cosa
    {
        if(other.gameObject.CompareTag("Ground") && !GameOver){ //como es una colisión, para ver la etiqueta de el el otro, hemos de acceder a su gameObject y luego 
            //usar el método .CompareTag("nombreTag")
        isOnGround = true; //ponemos a true la variable, de esta forma podremos volver a saltar
        dirtSplatter.Play();
        }else if (other.gameObject.CompareTag("Obstacle")) //si se choca con  un objeto que tiene la etiqueta Obstacle
        {
            _gameOver = true; //ponemos el booleano de gameOver a true
            Debug.Log("Game over"); //mostramos un mensaje por consola
            
            _animator.SetBool(DEATH_B, true); //activamos  la animación de muerte
            _animator.SetInteger(DEATH_TYPE_INT, Random.Range(1,3)); //Existe 2 animaciones, una asignada a el valor 1 y otra a el 2, al hacer
            //Random.Range(1,3)  lo que hace es devolver un número random entre 1 y 2, ya que el último número no lo tiene en cuenta
            explosion.Play();
            _audio.PlayOneShot(crashSound,1); //Reproduce una vez el clip crashSound con un valor de volumen dado por la variable audioVolume
            dirtSplatter.Stop();
            Invoke("RestartGame", 1);
        }
        
        
    }

    void RestartGame()
    {
        speedMultiplier = 1; //cambiamos le valor de la  variable a 1, estamos reiniciando el juego con todos los valores de inicio
        SceneManager.LoadScene("Prototype 3"); //Carga la escena de nombre Prototype 3
    }


}
