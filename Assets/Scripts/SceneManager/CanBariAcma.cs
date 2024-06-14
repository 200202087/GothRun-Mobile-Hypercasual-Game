//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanBariAcma : MonoBehaviour
{
    private bool canbariacma = true;
    public Slider canBari;
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);

        if (canbariacma)
        {
            canBari.gameObject.SetActive(true);
            canbariacma = false;
        }
    }
}
