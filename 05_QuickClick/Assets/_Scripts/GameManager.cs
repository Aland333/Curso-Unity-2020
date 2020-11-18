using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; //Importamos el paquete textMeshPro
using UnityEngine;
using UnityEngine.UI; //Para poder usar los botones
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random; //Para acceder a el control de las escenas


public class GameManager : MonoBehaviour
{

    public enum GameState //Creamos un enemurador, esto es como una serie de estados
    {
        loading,
        inGame,
        gameOver
        
    }

    public GameState gameState; //creamos un objeto de el enumerador GameState, este solo podrá tener 3 valores, loading, inGame o gameOver

    [SerializeField] private List<GameObject> targetPrefabs; //Utilizamos una lista de GameObject, la diferencia entre lista y array es que la lista es dinámica, puede aumentar su 
    //tamaño según se añadan nuevos valores o disminuir su propio tamaño, reorganizarlo, etc.
    [SerializeField] private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText; //Creamos una variable de tipo TextMeshProUGUI que será la encargada de acceder a el gameobject de tipo Text

    private int _score; //Creamos una variable encargada de tener el valor de la puntuación, haqcemos esto porque vamos a usar un set y get con la variable score

    [SerializeField] private Button restartButton; //Creamos una variable de tipo Button que almacenará un botón, hemos de añadirlo desde el editor

    private int numberOfLives = 3;
    
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

    [SerializeField] private GameObject titleScreen; //creamos una variable de tipo GameObject donde desde Unity nos encargamos de arrastrar el panel de la pantalla de título

    [SerializeField] private List<GameObject> lives; //Creamos una lista, gracias a la cual vamos a poder referenciar los 3 corazones de las vidas

    /// <summary>
    /// Función que llama a el inicio de el juego
    /// </summary>
    /// <param name="difficulty">Número entero que indica el grado de dificultad</param>
    public void Start()
    {
        ShowMaxScore(); //Para mostrar la puntuación máxima
    }

    public void StartGame(float difficulty)
    {
        gameState = GameState.inGame; //Ponemos el estado de el juego en inGame
        spawnRate /= difficulty; //una forma de controlar la dificultad es que esta afecte a la rapidez de el spawn de objetos, en este caso cuanto myor la dificultad más rápido saldran

        numberOfLives -= (int)difficulty - 1; //Asignamos el número de vidas según la dificultad

        for (int i = 0; i < numberOfLives; i++)   //de forma natural las vidas no se van a ver, y cuando se inicia el juego, se verán tantas vidas, como tenemos
        {
            lives[i].SetActive(true);
        }
        
        
        StartCoroutine(spawnTarget()); //Empieza la coorutina
        score = 0; //ponemos la puntación a 0
        UpdateScore(score);
        gameOverText.gameObject.SetActive(false);  //como al empezar el juego no queremos que aparezca el texto de gameOver hacemos que esté false
        restartButton.gameObject.SetActive(false); //Al empezar no queremos ver el botón, podemos ahorrarnos esto desactivando el botón desde Unity y ya está
        titleScreen.gameObject.SetActive(false);
        
    }

    IEnumerator spawnTarget() //Como lo que queremos es spawnear los gameObject de forma constante usamos una coorutina
    {
        while (gameState == GameState.inGame) //Mientras el juego esté en estado inGame
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
    
    /// <summary>
    /// Se encarga de mostrar en la pantalla de título la puntuación máxima almacenada
    /// </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0); //PlayerPrefs, es un objeto de Unity que se encarga de guardar de forma locac algunos valores, simples como
        // enteros, float o strings. En este caso ponemos definmos la variable "MAX_SCORE" con un valor por defecto de 0. Dicho valor es el la primera vez que se usa, una vez que se
        //actualice nunca volverá a ser 0
        scoreText.text = "Max Score = \n " + maxScore; //mostramos la puntuación máxima
    }


    public void GameOver() //este método se encarga de activar el texto de el game over
    {

        numberOfLives--; //restamos una vida

        if (numberOfLives >= 0)
        {
            Image hearthImage = lives[numberOfLives].GetComponent<Image>(); //creamos una varible de tipo imagen, en dicha variable guardamos de la lista lives, dentro de esta en la posición
            // asignada a el valor numberOfLives y de esta cogemos su imagen

            var tempColor = hearthImage.color; //creamos una variable de tipo var, que lo que esto significa que es una variable de cualquier tipo que cuando el programa llegue a este punto
            // se transformará en la variable correspodiente, en este caso es una variable de tipo Color
            tempColor.a = 0.3f; // .a de un color hace referencia a su opacidad, en este caso la ponemos en 0.3
            hearthImage.color = tempColor; //a la imagen guardado en hearthImage le cambiamos el color para que sea igual a la de tempColor
        
            //Si por algun casual intentásemos cambiarle el color directamente, sin pasar por una variable temporal, tenemos un error de compilación.
        }



        if (numberOfLives <= 0) //si no tenemos vidas, o son menores que 0, esto no haría falta en principio, pero lo ponemos por si hay algún tipo de búg al perder 2 vidas a la vez o quese yo
        {
            SetMaxScore(); //para cambiar la puntuacion máxima
            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true); //Hacemos que se vea el botón
        }

    }
    
    /// <summary>
    /// Función encargada de poner la puntación máxima
    /// </summary>
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE", score);
        }
    }

     public void RestartGame() //creamos la función que ejecutará el botón cuando es pulsado, recordamos que como se va acceder desde fuera, ha de ser público
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //accedemos el SceneManager y dentro de este cargamos: accedemos a el nombre de la scena actual y la cargamos
    }
    
}
