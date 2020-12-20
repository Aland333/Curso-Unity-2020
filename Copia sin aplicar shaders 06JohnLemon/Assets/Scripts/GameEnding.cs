using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f; //El tiempo que pasa hasta que se desvanece la imagen de victoria
    [SerializeField] private float displayImageDuration = 1f; //Tiempo que durará la imagen de fin de partida.
    [SerializeField] private GameObject player; // Para arrasstrar desde el inspector un GameObject que será el jugador, es decir John Lemon
    private bool isPlayerAtExit = false; //Creamos una variable booleana que se encarga de gobernar si el jugador ha llegado a la salida
    private bool isPlayerCaught = false; // una variable para controlar si han detectado a el jugador o no 
    [SerializeField] private CanvasGroup exitBackgroundImageCanvasGroup; //Para acceder a el CanvasGroup encargado de controlar la opaciodad de la imagen de fin de partida
    [SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup; //Para acceder a el CanvasGroup encargado de la imagen de fin de partida
    private float timer;
    [SerializeField] private AudioSource exitAudio, caughtAudio; //Para guardar los audios de salida y de cuando te pillan
    private bool hasAudioPlayed; //para ver si se ha reproducido un audio

    public void OnTriggerEnter(Collider other) //Cuando se detecte un collision de tipo trigger con otra cosa (Other)
    {
        if (other.gameObject == player)  //Si el el gameObject con el que hemos chocado es el que hemos guardado en la variable player. Es importante diferenciar entre GameObject
        // que es un tipo de objeto definido por Unity, que gameObject que es una variable local de el GameObject que permite acceder a la instancia de dicho GameObject
        {
            isPlayerAtExit = true; //El jugador ha llegado a la salida, lo ponemos en true
        }
    }

    public void Update() //Usamos el Update() porque se va a ir desvaneciendo a lo largo de el tiempo
    {
        if (isPlayerAtExit) //si ha llegado a la salida
        {
           EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio); //llamamos a la función EndLevel pasándole el parámetro exitBackgroundImageCanvasGroup, y un false
            //para que no reinicie la partida, luego le pasamos el audio asignado a la salida de el nivel
        }
        else if (isPlayerCaught) //si han pillado a el jugador
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio); //llamamos a la función EndLevel pasándole el parámetro caughtBackgroundImageCanvasGroup, y un true
            //para que reinicie la partida, luego le pasamos el audio asignado a cuando pillan a el jugador
        }
    }
    /// <summary>
    /// Lanza la imagen de el fin de la partida
    /// </summary>
    /// <param name="imageCanvasGroup"> Imagen de fin de partida correspondiente </param>
    /// <param name="doRestart"> Indica si hay que reiniciar la partida o no</param>
    /// <param name="audioSource">Indica el audio que hay que reproducir</param>
    
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {


        if (!hasAudioPlayed) //si todavia no se ha reproducide el audio, este if lo hacemos porque la función EndLevel() es llamada a cada frame, y para evitar que se reproduzca el audio cada
        //frame usamos esta comprobación
        {
            audioSource.Play(); //reproduce el audio
            hasAudioPlayed = true; //pon la variable que contra si se ha reproducido el audio a true
        }
        
        timer += Time.deltaTime; //Hacemos que el tiempo sea independiente de los frames para que sea un tiempo normal en segundos, ojo timer es float, su valor va ir cambiando con decimales
        // no va a pasar de 0 a 1,2,3, etc. Va a ir recorriendo los decimales entre estos.
        imageCanvasGroup.alpha = timer / fadeDuration; //Usamos la variable .alpha que es la encargada  de controlar la opacidad o transparencia, siendo 0 totalmente
        //transparente y 1 totalmente visible. Lo que hacemos es dividir el timer entre el tiempo que tarda en desvanecerse. Antes de llegar a la salida estaba en 0, y por tanto
        // no se veía nada, ahora según va pasando el tiempo lo que va sucediendo es que va apareciendo la imagen. pese a que con este código puede tener un valor mayor que 1, unity lo dejará en 1

        if (timer > fadeDuration + displayImageDuration) //Cuando haya pasado el tiempo que tarda en aparecer la imagen + el tiempo que permanece en pantalla
        {
            if (doRestart == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //en caso de que tengamos un true, reiniciamos la escena actual. Recordamos que hemos de añadir las escenas llendo a
                // file -> build setting
            }
            else
            {
                Application.Quit();
            }
        }
        
    }
    /// <summary>
    /// Es una función que será llamada desde otro script que servirá para poner la variabe isPLayerCaught en true
    /// </summary>
    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
