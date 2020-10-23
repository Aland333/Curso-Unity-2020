using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruccionObjetos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -40)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -0.02)
        {
            Destroy(gameObject);
            Debug.Log("Has perdido");
            //Time.timeScale = 0;
            
        }
    }
}
