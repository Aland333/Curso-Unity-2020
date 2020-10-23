using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs; //para añadir los prefabs que va a spawnear
    [SerializeField] private Vector3 spawnPos; //Para guardar la posición donde se va a spawnear
    private int indice; //para navear por el array
    [SerializeField] private float startDelay = 2; //el tiempo desde que empieza el juego hasta que empieza a spawnear cosas
    [SerializeField] private float repeatRate = 2; //el tiempo que tarda en volver a spawnear cosas
    private PlayerController _playerController; 
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").
            GetComponent<PlayerController>();
        
        
       InvokeRepeating("SpawnObstacle", startDelay, repeatRate); //llama a el método SapwnObstacle, despues startDelay segundos de juego
       //y vuelve a llamar a este método cada repeatRate segundos
        
       
    }

    void SpawnObstacle() //método encargado de instanciar los prefabs y por tanto de spawnear
    { 
        if(!_playerController.GameOver) //si gameOver es true
        {
        indice = Random.Range(0, obstaclePrefabs.Length); //seleccionamos de forma aleatoria un prefab
        spawnPos = this.transform.position; //la posición de spawn será donde este el gameObject al que está asignado este script
        Instantiate(obstaclePrefabs[indice], spawnPos, obstaclePrefabs[indice].transform.rotation); // spawnea el prefab que se haya obtenido aleatoriamente
        //mediante el indice, en la posición dada pos spawnPos, con la rotación de el propio prefab
        }
     
    }
}
