using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator; //creamos una variable para acceder a el animator de el gameobject
    private Rigidbody _rigidbody;//creamos una variable para acceder a el Rigidbody de el gameobject
    private Quaternion rotation = Quaternion.identity; //creamos una variable para rotación de el personaje, dicha rotacion se obtiene mediante Quaterniones, usamos .identity para que sea el
                                                       // valor unidad, luego le daremos el valor que buscamos
    [SerializeField]  private float turnSpeed = 20f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();//Accedemos a el animator
        _rigidbody = GetComponent<Rigidbody>();//Accedemos a el Rigidbody
    }
    
    void FixedUpdate() //Cambiamos Update por FixedUpdate. FixedUpdate es un estado que se ejecuta antes de el Update normal y está ligado con la tasa de refresco de la física, por lo tanto
    //como dentro de este estamos trabajando con físicas (movimientos, rigidbody, etc) si usamos update estamos malgastando cálculos de frames que no se hacen, ya que el que nos mueve el personaje
    // es el método OnAnimatorMove(). Por ello ponemos FixedUpdate() y así el movimiento se calcula de forma correcta y no abrá diferencias de valores de frame a frame. Por ejemplo, si usamos un update
    //este es cada frame, suponiendo que nos va a 180 frames, el movimiento devido a la animacion es de la física que se ejecuta 50 frames, por tanto hay una discrepancia y perdemos valores y capacidad
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize(); //Existe un problema y es que el movimiento es vectot con 2 componentes X-Z si pulsamos  solo para X el vector tiene de módulo 1 y lo mismo para Z, pero si pulsamos
        // los 2 a la vez el módulo no es 1, y por tanta para hacer que siempre sea 1 hemos de normalizar.

        bool hasHorizontalInput =
            !Mathf.Approximately(horizontal, 0f); // creamos una variable booleana que se encarga de recoger si hay movimiento, para ello usamos
        // Mathf.Approximately(horizontal, 0f) que lo que hace es devolver un true si es aproximadamente 0, y por ello lo negamos con ! 
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput; //el boolean isWalking encargado de gestionar la animacion de caminar será true si hay movimiento horizontal o vertical
        
        _animator.SetBool("IsWalking", isWalking); //El boolean que hemos creado en el animator de nombre "IsWalking" será true o false según lo sea la variable isWalking que hemos creado
        //en este script

        Vector3 desireForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f); //hasta ahora tenemos configurado en movimiento
        //simple siendo un vector el que activa la animación, pero lo que no tenemos es que por ejemplo cuando el personaje está mirando apara adelante y pulsamos la derecha, se activa la animación
        //de movimiento, pero lo hace de forma que esa animación sigue estándo hacia la direccion adelante de el personaje, para solucionarlo, creamos un Vector3 con el método .RotateToWard()
        //el cual dentro tenemos que indicar, primero la dirección desde donde partimos, en este caso, la posición hacia adelante de el personaje, la posición donde finalizamos, en este caso es
        // el vector dado por la variable movement que hemos configurado, luego la velocidad que gira desde la primera posición hasta la segunda, recordamos que multiplicamos po Time.deltaTime
        // para que depende el tiempo y no de los frames. El último parámetro es la máxima maginitud de el delta
        
        rotation =  Quaternion.LookRotation(desireForward); //Los Quaternion son matrices encargadas de guardar rotaciones, su obtención es compleja, basta con saber lo que hacen.
        //En este caso lo que hacemos es usar un método .LookRotation(vector) que lo hace es calcular el Quaternion para que se haga la rotación hacía dicho vector


    }

    private void OnAnimatorMove() //Sobreescribimos un método que se llama cuando hay un cambio en la animación
    {
        // S = S0 + velocidad * tiempo    -> sin embargo ahora la velocidad es la velocidad de la animación
        _rigidbody.MovePosition(_rigidbody.position +  movement * _animator.deltaPosition.magnitude); //_rigidbody.position es la posición inicial. movement es la dirección de el movimiento y
        // _animator.deltaPosition.magnitude es la cantidad de movimiento debida a la 
        _rigidbody.MoveRotation(rotation); //Hacemos lo mismo pero con ratación, en este caso solo necesitamos el Quaternion
    }
}
