//using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
//using UnityEngine.UI;

public class EndlessRunnerCharacterController : MonoBehaviour
{
    public float speed; // Karakterin h�z�
    public float sideSpeed = 8f; // Yan hareket h�z�
    private Rigidbody rb;
    private float slowTimer;
    private bool inslow;
    private bool instay;
    public bool instay2;
    private bool kosmaBitis;
    public GameObject Sphere;
    public GameObject Cube;
    public float gitmehizi;
    private Animator mAnimator;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed, 0, 0); // Karakteri ba�lang��ta ileri do�ru hareket ettir
        inslow = false;
        instay = false;
        instay2 = false;
        kosmaBitis = false;
        slowTimer = 0;
        speed = 5f;
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {   

        // Klavye giri�ini kontrol et
        float horizontalInput = Input.GetAxis("Horizontal");

        // Karakteri yan hareket ettir
        if(kosmaBitis == false)
        {
            Vector3 movement = new Vector3(horizontalInput * sideSpeed, 0, speed);
            rb.velocity = movement;

            // Karakterin ekrandan ��kmas�n� engelle (iste�e ba�l�)
            float newX = Mathf.Clamp(transform.position.x, -7f, 7f); // E�er sahnenizde s�n�rlar varsa ayarlayabilirsiniz
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        

        if(inslow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer > 1) 
            {
                speed = 5f;
                slowTimer = 0;
                inslow = false;
            }
        }
        else if(instay)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer > 3)
            {
                Vector3 movementUP = new Vector3(0, 2, 0);
                rb.velocity = movementUP;
                //print("girdi");
                //Sphere.transform.position = Vector3.MoveTowards(Sphere.transform.position, Cube.transform.position, gitmehizi);
                slowTimer = 0;
                instay = false;
            }

        }
        else if (instay2)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer > 3)
            {
                mAnimator.SetTrigger("DanceOff");
                Vector3 movementFLY = new Vector3(0, 0, 5);
                rb.velocity = movementFLY;
                //print("ucuyor");
                //Sphere.transform.position = Vector3.MoveTowards(Sphere.transform.position, Cube.transform.position, gitmehizi);
                slowTimer = 0;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obstacle")
        {
            inslow = true;
            speed = 2f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "obstacle2")
        {
            //print("obstacle2");
            mAnimator.SetTrigger("DanceOn");
            instay = true;
            kosmaBitis = true; 
            Vector3 movementSTOP = new Vector3(0, 0, 0);
            rb.velocity = movementSTOP;
            speed = 0f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "ucmasNoktasi")
        {
            //print("ucmasNoktasi");
            instay2 = true;
            Vector3 movementSTOP = new Vector3(0, 0, 0);
            rb.velocity = movementSTOP;
            speed = 0f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "BossHitWall")
        {
            Destroy(gameObject);
        }
    }

}
