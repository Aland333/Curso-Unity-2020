using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    //establecemos un límite superior y otro inferior
    [SerializeField] private float topBound = 30f; 
    [SerializeField] private float downBound = -10f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound) //si nos salimos de los límites
        {
            
            Destroy(gameObject); //destroimos el gameObject al cual se le ha asigando este script.
            
        }
        
        if (transform.position.z < downBound) //cuando se salen por abajo
        {
            Debug.Log("Game over");
            Destroy(gameObject); //destroimos el gameObject al cual se le ha asigando este script.
            Time.timeScale = 0;

        }
    }
}
