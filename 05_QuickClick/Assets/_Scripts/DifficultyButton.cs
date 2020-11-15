using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button _button; //Creamos una variable de tipo botón
    private GameManager gameManager; //creamos una variable de tipo GameManager
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //Buscamos el objeto de tipo GameManager y lo guardamos en la variable gameManager
        _button = GetComponent<Button>(); //Accedemos a la componente Button y la guardamos en button
        _button.onClick.AddListener(SetDifficulty); //añadimos una función a los botones, esta es que al hacer click 
     
    }

    void SetDifficulty()
    {
        Debug.Log("El botón" + gameObject.name + " ha sido pulsado.");
        gameManager.StartGame(); //llamamos a el método .StartGame() que está en el gameManager

    }
    
}
