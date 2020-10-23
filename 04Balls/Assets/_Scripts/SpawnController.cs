using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab; //Para pasarle por Unity el prefab que se va a instanciar
    private float spawnRange = 9; //un float para limitar la posición aleatoria donde se puede general el enemigo
    public int enemyCount;
    public int enemyWave = 1;
    [SerializeField] private GameObject powerUpPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
       SpawnEnemyWave(enemyWave);
    }

    private void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length; //Busca en escena los Objetos de tipo Enemy, los almacena en un array del cual cogemos su longitud
        if (enemyCount == 0)
        {
            
            enemyWave++; //Aumentamos el valor de la variable enemyWave encargada de llebar un registro de el número de oleada en el que nos encontramos
            SpawnEnemyWave(enemyWave); //spawneamos tantos enemigos, como el número de oleada que nos encontramos
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation); //spawneamos un powerUp, solo spawneamos uno para hacer que la dificultad de el juego
            //vaya subiendo

        }
    }
    
    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Devuelve un vector3 con la posion generada</returns>

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange); //Genera una posición en x aleatoria
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);//Genera una posición en z aleatoria
        Vector3 ramdonPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return ramdonPosition;

    }

    /// <summary>
    /// Método que genera un número determinado de enemigos en pantalla
    /// </summary>
    /// <param name="numberOfEnemys"> Número de enemigos a crear</param>
    
    private void SpawnEnemyWave(int numberOfEnemys)
    {
        for (int i = 0; i < numberOfEnemys; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); //instancia el GameObject guardado en enemyPrefab, en la
            //posicion genera por el método GenerateSpawnPosition(), y con la rotación que tiene el prefab de base
            
        }

        
    }
  
}
