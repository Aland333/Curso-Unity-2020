using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAlante : MonoBehaviour
{

    [SerializeField] private float velocidad = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidad * Vector3.forward * Time.deltaTime);
    }
}
