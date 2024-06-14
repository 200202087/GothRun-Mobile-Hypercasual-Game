//using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class MobileHeroInput : MonoBehaviour
{
    public float speed; // Karakterin hýzý
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
    private float swipeSpeed = 0.005f;
    private Touch touch;
    private bool canbariacma = true;
    public Slider canBari;
    public Transform hedefKutu;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed, 0, 0); // Karakteri baþlangýçta ileri doðru hareket ettir
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
        if(Input.touchCount >0 && kosmaBitis == false)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved) 
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x  * swipeSpeed,
                    transform.position.y, transform.position.z);
            }
        }

        // Karakteri yan hareket ettir
        if(kosmaBitis == false)
        {
            Vector3 movement = new Vector3(0, 0, speed);
            rb.velocity = movement;

            // Karakterin ekrandan çýkmasýný engelle (isteðe baðlý)
            float newX = Mathf.Clamp(transform.position.x, -7f, 7f); // Eðer sahnenizde sýnýrlar varsa ayarlayabilirsiniz
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
                //Vector3 movementFLY = new Vector3(0, 0, 5);
                //rb.velocity = movementFLY;
                Vector3 targetPosition2 = hedefKutu.position;
                Vector3 moveDirection2 = (targetPosition2 - rb.position).normalized;
                rb.GetComponent<Rigidbody>().velocity = moveDirection2 * 6;
                float rotationSpeed = 6f;
                Quaternion toRotation = Quaternion.LookRotation(moveDirection2, Vector3.up);
                rb.rotation = Quaternion.Lerp(rb.rotation, toRotation, Time.deltaTime * rotationSpeed);
                //print("ucuyor");
                //Sphere.transform.position = Vector3.MoveTowards(Sphere.transform.position, Cube.transform.position, gitmehizi);
                slowTimer = 0;
                if (canbariacma)
                {
                    canBari.gameObject.SetActive(true);
                    canbariacma = false;
                }
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
