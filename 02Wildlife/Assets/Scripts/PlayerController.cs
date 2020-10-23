using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed = 10f;
    private float horizontalInput; //Variable para controlar el movimiento horizontal
    private float verticalInput; //variable para controlar el movimiento vertical
    [SerializeField] private float xRange = 15f; //variable para poner un límite en x
    [SerializeField] private float zRange = 15f;
    [SerializeField] private GameObject projectilePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movmiento de el granjero
        horizontalInput = Input.GetAxis("Horizontal"); //Para que la variable dependa de el input asociado en el inputManager a "Horizontal" es decir 'a' y 'd'
        verticalInput = Input.GetAxis("Vertical"); //Lo mismo pero para el movmimento vertical
        transform.Translate( horizontalInput* Vector3.right*Time.deltaTime * speed); //el movimiento va a depender de el botón pulsado multiplicado
        //por la dirección multiplicado por la la velocidad( hay que usar Time.deltaTime para que depende el tiempo y no de los framos) multiplicado por la
        //variable que nos da la velocidad
        transform.Translate( verticalInput* Vector3.forward*Time.deltaTime * speed); //lo mismo pero para el hacia delante, que es un valor de vector
        // (0,0,1)
        CheckInBounds(xRange,zRange); //Un método para comprobar los límites
        
        //Acciones de el personaje
        if (Input.GetKeyDown(KeyCode.Space))//cuando se pulsa la tecla barraespaciadora y se mantiene devuelve un true y un false si no
        {

            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation); //instanciamos el objeto projectilePrefab, en la
            //posición de el gameobject donde está este script, con la rotación ya definida en el prefab asociado a projectilePrefab

        } 
    }

    public void CheckInBounds(float x, float z)
    {
        if (transform.position.x < -x) //si se sale a izquierdas
        {
            transform.position = new Vector3(-x, transform.position.y, transform.position.z); //fija su posición en x para que no pueda seguir saliendo
            
        }        
        if (transform.position.x > x) //si se sale a dereches
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            
        }
        if (transform.position.z < z-15) //si se sale por abajo
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z-15);
            
        }    
        if (transform.position.z > z) //si se sale por arriba
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
            
        }  
        
        
        
    }
}
