//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EntranceDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
           Destroy(gameObject);  
    }
}
