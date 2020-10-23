using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] enemies;//Creamos una colección de enemigos, de esta forma en Unity podremos arrastrar tantos Gameobject como queramos
    //estamos usando un array, cuya dimensión una vez instanciado es fija, pero gracias a que estamos usando Unity, podemos eleguir desde Unity el tamaño
    //de dicho array, haciendo que sea más versatil.

    private int animalIndex; //índice el animal que aparece en el array
    [SerializeField]private float spawnRangeX = 14;
    private float spawnPosZ;
    [SerializeField]private float startDelay = 2f; //el tiempo hasta que empieza a spawnear cosas
    [SerializeField][Range(0.1f, 2f)]private float spawnInterval = 1.5f;// tiempo que pasa este spawneao

    private void Start()
    {
        spawnPosZ = this.transform.position.z; //De esta forma guardamos en esta variable la posicion Z de el spawner. Ahora imaginemos que en lugar
        //de tenerlo en una variable, tenemos transform.position.z en el update, al hacer en cada momento va a estar comprobando la posición en z de el
        //gameObject lo cual consume recursos, por ello al guardarlo en una variable al empezar, siemplemente tiene que hacer referencia a esta variable
        //y se ahora muchos recursos.
        InvokeRepeating("SpawRandomAnimal", startDelay,spawnInterval); //cuando empieza el script, despues de un delay dado por
        //la variable starDelay (en este caso 2 segundos) se va a empezar llamar a el método SpawRandomAnimal cada spawnInterval segundos es decir en este
        // caso cada 1.5 segundos. OJO estamos haciendo esto en el método Start(), ya no necesitamos el update()
        
    }

    

    private void SpawRandomAnimal() //este método es el que se encarga de spawnear animales
    {
        //Generar la posición donde va a aparecer el siguiente enemigo
            
        animalIndex = Random.Range(0, enemies.Length); //como 0 y enemis:length son enteros el aleatorio también es un entero
        float xRand = Random.Range(-spawnRangeX, spawnRangeX); //Como spawnRangeX es un float el número aleatorio que se genera también es un float
            
        Vector3 spawnPos = new Vector3(xRand,0,spawnPosZ);
        //Creamos un vector3 cuya x es un valor aleatorio entre +- spawnrangeX  (en el caso predeterminado -20 y 20), en y =0 y en z en la variable
        //que hemos guardado antes la posición de el gameObject que tiene este script
            
        Instantiate(enemies[animalIndex], spawnPos, enemies[animalIndex].transform.rotation);
        //instaciamos el gameObject guardado en el Array enemies en concreto en la posición animalIndex, lo instanciamos en el lugar donde está el
        // gameObject asociado a este script con la rotación que ya lleva de seríe dicho gameObject
        
    }
}
