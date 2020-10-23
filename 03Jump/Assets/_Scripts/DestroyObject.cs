using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //Cuando un objeto entre en trigger de el objeto que lleva este Script
    {
            
            Destroy(other.gameObject); //destruye el gameObject de lo otro que ha habido colision

    }
        
}

