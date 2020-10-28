using System.Collections;
using System.Collections.Generic;
using TMPro; //Importamos el paquete textMeshPro
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> targetPrefabs; //Utilizamos una lista de GameObject, la diferencia entre lista y array es que la lista es dinámica, puede aumentar su 
    //tamaño según se añadan nuevos valores o disminuir su propio tamaño, reorganizarlo, etc.
    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText; //Creamos una variable de tipo TextMeshProUGUI que será la encargada de acceder a el gameobject de tipo Text
    private int score; //creamos una variable para la puntuación
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnTarget()); //Empieza la coorutina
        score = 0; //ponemos la puntación a 0
        UpdateScore(score);
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
    /// <summary>
    ///  Actualiza el valor de la puntuación y lo muestra por pantalla
    /// </summary>
    /// <param name="scoreToUp"> Número de puntos a añadir a la puntuación global </param>
    public void UpdateScore(int scoreToUp)
    {
        score = score + scoreToUp;
        scoreText.text = "Score: \n " + score;
    }
}
