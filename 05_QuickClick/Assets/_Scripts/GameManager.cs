using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> targetPrefabs; //Utilizamos una lista de GameObject, la diferencia entre lista y array es que la lista es dinámica, puede aumentar su 
    //tamaño según se añadan nuevos valores o disminuir su propio tamaño, reorganizarlo, etc.
    [SerializeField] private float spawnRate = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnTarget()); //Empieza la coorutina
    }

    IEnumerator spawnTarget() //Como lo que queremos es spawnear los gameObject de forma constante usamos una coorutina
    {
        while (true) //un bucle infinito
        {

            yield return new WaitForSeconds(spawnRate); //se espera el tiempo que le pasamos por spawnRate
            int index = Random.Range(0, targetPrefabs.Count); //selecciona la posicion de uno de los Gameobjects en la lista targetPrefabs
            Instantiate(targetPrefabs[index]); //instancia el GameObject seleccionado anteriormente
        }

    }
}
