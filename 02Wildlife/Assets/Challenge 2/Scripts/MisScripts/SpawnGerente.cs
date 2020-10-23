using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGerente : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] pelotas;
     private int indicePelotas;
    [SerializeField] private float[] posiciones = new float []{-20,-8, 4};
     private int indicePosiciones;
    [SerializeField] private float tiempoRespawn = 1f;
    
    void Start()
    {
        
        InvokeRepeating("Spawner", 1, tiempoRespawn);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawner()
    {
        indicePelotas = Random.Range(0, 3);
        indicePosiciones = Random.Range(0, 3);
        Debug.Log("Hemos expaneado la bola en el indice" +  indicePelotas);
        
        
        Instantiate(pelotas[indicePelotas], new Vector3(posiciones[indicePosiciones], 30,0 ), transform.rotation);
        
    }
}
