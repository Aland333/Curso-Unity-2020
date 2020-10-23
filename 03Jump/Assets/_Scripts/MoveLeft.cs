using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    [SerializeField] float speed =10f;
    private PlayerController _playerController; //creamos una variable pero esta va a ser de tipo PlayerController, es decir, una instancia de el script
    //PlayerController que nosotros mismos hemos creado. Por convección el nombre de las propiedades externas c# referentes a Unity empiezan por barra baja _
    // Unos ejemplos de estas propiedades puede ser el Rigidbody, un collider, una instancia de otro script etc.
    
    // Start is called before the first frame update

    private void Start()
    {
        _playerController = GameObject.Find("Player").
            GetComponent<PlayerController>();
        //Usando la clase GameObject, busca aquel de nombre "Player" el cual es un gameObject, ahora de ese gameObject accedemos a sus componentes, 
        // más en concreto a la componente PlayerController
        
        //Despues de GameObject. tenemos varios tipos de Find:
        //Find("nombre_del_gameobject") -> Busca el objeto que tenga el nombre indicado
        //
        //FindWithTag("Nombre_del_tag") -> Busca el objeto que tenga la etiqueta indicada
        //
        //FindObjectOfType(type(nombre del componente a buscar ) -> Busca entre todos los objetos aquel que tenga la componente que le hemos indicado
        // la componente puede ser un script u otra cosa
        //




    }


    // Update is called once per frame
    void Update()
    {
        if(_playerController.GameOver == false)
        {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
