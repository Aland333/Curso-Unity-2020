using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{

    [SerializeField] private Camera _camera; //Definimos una variable de tipo Camera, porque será apartir de esta donde obtengamos las coordenadas y demás necesario para seguir a el ratón
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //De esta forma pasamos la posición de el ratón a coordenadas de el mundo
        mousePos = new Vector3(mousePos.x, mousePos.y, 0); //El código de antes nos da la posición en relación de a la cámara, y como en nuestro juego la cámara está en -10z
        //tenemos que se tendrá en cuenta dicha posición, sin embargo la posición de nuestro juego siempre ha estado en z=0, por lo tanto lo hardcodeamos a dicha posición
        transform.position = mousePos;
    }
}
