using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float force = 20;
    [SerializeField] private bool usePhysicsEngine = false;
    private Rigidbody _rigidbody;

    private float verticalInput, horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        

        if (usePhysicsEngine == true)
        {
            
        
        //Si se utiliza la física, hay que tener en cuenta aceleraciones, rozamientos, gravedad ,etc. Recordamos la fórmula básica f = m *a. Estemos trabajando
        //con fuerzas y no valores absolutos, como sí lo hariamos si trabajamos con transform
        //AddForce sobre el rigidbody
        //AddTorque (fuerza de torsión) sobre el rigidbody
        
        _rigidbody.AddForce(Vector3.forward * Time.deltaTime * force * verticalInput);      //vector 3 forward es en la dirección positiva de las x
        _rigidbody.AddTorque(Vector3.up * Time.deltaTime * force * horizontalInput); //vector3 up es al rededor de el eje y, es decir, la bola gira sobresimisma pero el giro
            //no acelerará la bola porque ese giro no es contra el suelo
        
        }
        else
        {
            
        
            //Si no se utiliza la física, en esta ocasión movemos el objeto en bloque, el movimiento en si no se verá afectado por rozamiento, gravedad, etc
            //aun que si una vez que esté en movimiento si se verá afectado por estas cosas. Por ejemplo si al chocar el objeto rota, la dirección
            // de el desplazamiento se puede ver modificada, una solución a esto es limitar los giros o el desplazamiento en algún sentido
        // Translate sobre el transform -> Para mover 
        // Rotate sobre el transform -> Para rotar
        
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed*horizontalInput);
        
        
        
        }

        if (Mathf.Abs(transform.position.x) >= 17 || Mathf.Abs(transform.position.z) >= 17) //En caso de que la posición sea mayor que valor absoluto en  x
         //o en Z reducimos su velocidad a 0
        
        {
            
            _rigidbody.velocity = Vector3.zero;
            if (transform.position.x >= 17) //si es mayor que 17 en x
            {
                transform.position = new Vector3(17, transform.position.y, transform.position.z); //hacemos que su posición en x sea 17 y por tanto
                //no pueda pasar de esta posición
                
                
                //repetimos el proceso para cada lado límite
            }if (transform.position.x <= 17)
            {
                transform.position = new Vector3(-17, transform.position.y, transform.position.z);
                
                
            }if (transform.position.z >= 17)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 17);
                
                
            }if (transform.position.z <= -17)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -17);
                
                
            }

        }
        

    }
}
