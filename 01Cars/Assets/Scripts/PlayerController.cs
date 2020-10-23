using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //Todas las clases heredan de MonoBehaviour que es como la clase superior en Unity, todo
    //el que hereda de MonoBehaviur es arrastable como componente
{
    //Propiedades
    //[HideInInspector] //Esta función lo que hace es que la variable no aparezca en el inspector
    [Range(0, 40)] //Con esta función limita el rango de valores entre 0 y 20
    [SerializeField] //al usar [SerializaField] también podemos acceder desde el editor de Unity. Se pueden usar tantos modificadores como queramos
    [Tooltip("Velocidad actual de el coche")]
    //este modifacores lo que hace es dar una información extra cuando en Unity dejamos en ratón encima
    private float speed = 15f; //Creamos una variable de tipo float y de nombre speed que se encargará de controlar la velocidad
        // al hacerla privada solo se podra acceder a ella desde este script.
    [Range(0, 90)] [SerializeField] [Tooltip("Velocidad de giro del coche")]private float turnSpeed= 75f;     //creamos una variable encargada de la velocidad de giro
    
    
    private float horizontalInput; //será el encargado de registrar el input horizontal, que varía de -1 para totalmente a la izquierda y +1 para toalmente
    //a la derecha
    private float verticalInput; 


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        

        horizontalInput = Input.GetAxis("Horizontal"); //el valor de la variable que hemos dado se encarga de recibirla accediendo al input, dentro de este a los ejes
        //y en concreto al "Horizontal"   dentro de estas comillas hay que poner el nombre exacto que aparece en Unity

        verticalInput = Input.GetAxis("Vertical");
        
        //posicion = posicionInicial + velocidad * tiempo
        transform.Translate( speed * Time.deltaTime * Vector3.forward * verticalInput );  //de mi propio transform accedemos a el método Translate() de este avanzamos
        //en la posición forward de un vector de 3 dimensiones, sería un avanzo en (0,0,1), ahora para controlar la velocidad usamos Time.deltaTime,
        //de esta forma pasa a ser de 1 vez por frame a 1 vez por segundo, luego lo multiplicamos por verticalInput que depende de el botón pulsado
        
        transform.Rotate(horizontalInput*turnSpeed*Time.deltaTime*Vector3.up);  //Esta código se encarga de controlar la velocidad de giro,lo hace según
        //la rotación, tenemos que usar Vector3.up porque giramos alrededor de el eje que va hacia arriba





    }
}
