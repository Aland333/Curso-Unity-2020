using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonigoteController : MonoBehaviour
{

    private Animator _animator; //Para almacenar el animator
    private const string MOVE_HANDS = "Move Hands"; //Creamos una constante de tipo Strign que ha de tener exactamente el mismo nombre que tiene el parámetro
    // en la pestaña parameters de Unity
    private const string MOVE_X = "Move X";
    private const string MOVE_Y = "Move Y";
    private float moveX;
    private float moveY;
    private bool isMovingHands = false; //creamos un bool encargado de gestionar el movimiento de manos

    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>(); //accedemos a el animator y lo guardamos en la variable _animator
        _animator.SetBool(MOVE_HANDS, isMovingHands); //a el parametro de la pestaña parameters asociado a la constante MOVE_HANDS, es decir el parametro
        // de nombre "Move Hands" se le pasa el valor booleando guardado en la variable isMovingHands
        _animator.SetFloat(MOVE_X, moveX );
        _animator.SetFloat(MOVE_Y, moveY );
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Mathf.Sqrt(moveX * moveX + moveY * moveY)> 0.01)
        {
            _animator.SetBool("isMoving", true);
            
            _animator.SetFloat(MOVE_X, moveX );
            _animator.SetFloat(MOVE_Y, moveY );
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) //si se pulsa el espacio
        {

            isMovingHands = !isMovingHands; //cambiamos el valor booleando 
            _animator.SetBool(MOVE_HANDS, isMovingHands); //a el parametro de la pestaña parameters asociado a la constante MOVE_HANDS, es decir el parametro
            // de nombre "Move Hands" se le pasa el valor booleando guardado en la variable isMovingHands
      

        }
        
    }
}
