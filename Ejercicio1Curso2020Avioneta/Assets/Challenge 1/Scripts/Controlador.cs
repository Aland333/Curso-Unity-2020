using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField][Tooltip("Velocidad de el vuelo constante de el avion")] private float velocidadVuelo = 20f;
    [SerializeField] [Tooltip("velocidad con la que rota verticalmente")]private float velocidadVertical = 10f;
    [SerializeField] [Tooltip("Velocidad a la que rota horizontalmente")]private float velocidadHorizontal = 10f;
    private float inputVertical;
    private float inputHorizontal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputVertical = Input.GetAxis("Vertical"); //para moverse verticalmente según pulsemos el botón w o s
        inputHorizontal = Input.GetAxis("Horizontal");//para moverse horizontalmente según pulsemos el botón a o s
        
        transform.Rotate(inputVertical * velocidadVertical * Time.deltaTime * Vector3.right );//con esta línea de código controlamos la velocdad con la
        //que rota el avión respescto al eje de las x es decir veriticalmente
        transform.Rotate(inputHorizontal * velocidadHorizontal * Time.deltaTime * Vector3.up);//con esta línea de código controlamos la velocdad con la
        //que rota el avión respescto al eje de las y es decir horizontalmente
        transform.Translate(velocidadVuelo * Time.deltaTime * Vector3.forward);//con esta línea de código nos aseguremos que lleve una velocidad
        //constante en el eje Z


    }
}
