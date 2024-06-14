//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class SimpYokEtme : MonoBehaviour
{
    private Animator mAnimator;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 hiz = rb.velocity;
        if (hiz.x < 1 && hiz.y < 1 && hiz.z < 1)
        {
            mAnimator.SetTrigger("twerkOn");
            //Invoke("twerkOffFunc", 3f);

        }
        else
        {
            twerkOffFunc();
        }
    }

    void twerkOffFunc()
    {
        mAnimator.SetTrigger("twerkOff");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Çarpýþma algýlandýðýnda
        if (other.tag == "BossHitWall")
        {
            Destroy(gameObject);
        }
    }
}
