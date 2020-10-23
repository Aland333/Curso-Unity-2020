using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //Cuando un objeto entre en trigger de el objeto que lleva este Script
    {
        if(other.CompareTag("Projectile")) //si el otro objeto con el que choca tiene el Tag Projectile
        {
        Destroy(other.gameObject); //destruye lo otro
        Destroy(this.gameObject); //destruye el GameObject a el cual le está asignado este script
        }
    }
        
}
