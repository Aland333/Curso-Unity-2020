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

    private int _score; //Creamos una variable encargada de tener el valor de la puntuación, haqcemos esto porque vamos a usar un set y get con la variable score

    private int score //Score será en encargado de comprobar la puntuación que tenemos
    {    
        
        set //set se encarga de poner un valor
        {
            _score = Mathf.Clamp(value, 0, 99999);  //el valor de _score ha de estar entre 0 y 99999, por lo tanto si detecta que es un número negativo se pondra a el 0 y si es uno
            // mayor a 99999 se podnrá en 99999
        }

        get
        {
            return _score; //devolvemos el valor de _score
        }
        
    }

    [SerializeField] private TextMeshProUGUI gameOverText; //Creamos una variable de tipo TextMeshProUGUI para almacenar el texto de el game over, dicho texto lo asignamos mediante Unity
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnTarget()); //Empieza la coorutina
        score = 0; //ponemos la puntación a 0
        UpdateScore(score);
        gameOverText.gameObject.SetActive(false);  //como al empezar el juego no queremos que aparezca el texto de gameOver hacemos que esté false
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


    public void GameOver() //este método se encarga de activar el texto de el game over
    {
        gameOverText.gameObject.SetActive(true);
        
    }
}
