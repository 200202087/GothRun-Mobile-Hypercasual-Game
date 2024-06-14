using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class EndlessRunnerManager : MonoBehaviour
{
    public EndlessRunnerCharacterController PlayerController;
    public MobileHeroInput MobileController;
    public Transform character;
    public Transform hedefKutu;
    public GameObject humanPrefab;
    public float humanSpawnDistance = 1f;
    public float humanFollowDistance = 1f;
    public float collisionDistance = 1.5f;
    private float slowTimer;
    public bool ucmaBasladi;
    private bool simplerDuruyor;
    private bool oyunBitisi;
    public int simpSayisi = 0;
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;

    private List<Transform> humans = new List<Transform>();
    private bool hasCollided = false;
    

    void Start()
    {
        SpawnHumans();
        slowTimer = 0;
        ucmaBasladi = false;
        simplerDuruyor = false;
        oyunBitisi = false;
    }

    void Update()
    {
        
            MoveHumans();
            if (!hasCollided)
            {
                CheckCollisions();
            }


    }

    void SpawnHumans()
    {
        Vector3 spawnPosition = character.position + 2*Vector3.back;
        for (int i = 0; i < 1; i++)
        {
            GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
            humans.Add(newHuman.transform);
            ApplyPhysicsMaterial(newHuman.transform);
            ApplyRigidbodyConstraints(newHuman.transform);
            spawnPosition += new Vector3(humanSpawnDistance, 0, 0);
            simpSayisi += 1;
        }
    }

    public float maxSpeed = 5f;

    void MoveHumans()
    {

        
        if (oyunBitisi == false)
        {
            for (int i = 0; i < humans.Count; i++)
            {
                Transform human = humans[i];

                // Hedef pozisyonunu belirle (karakterin konumuna doðrudan yönel)
                Vector3 targetPosition = character.position;
                

                // Hedefe doðru yönlendir
                Vector3 moveDirection = (targetPosition - human.position).normalized;
                

                // Hýzý belirle
                float speed = maxSpeed; // Sabit bir hýzda hareket etsinler

                // Hareket et
                human.GetComponent<Rigidbody>().velocity = moveDirection * speed;

                // Yavaþça dönmek için
                float rotationSpeed = 5f;
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                human.rotation = Quaternion.Lerp(human.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }
        }
        
        if (simplerDuruyor == true)
        {
            print("walladuruyor");
            oyunBitisi = true;
            slowTimer += Time.deltaTime;
            if (slowTimer > 3)
            {
                print("billa duruyor");
                simplerDuruyor = false;
                slowTimer = 0;
            }
        }
        
        else if (ucmaBasladi == true)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer > 3)
            {
                print("yuruyolar");
                for (int i = 0; i < humans.Count; i++)
                {
                    Transform human = humans[i];
                    //Vector3 simpKosu = new Vector3(0, 0, 6);
                    //human.GetComponent<Rigidbody>().velocity = simpKosu;
                    Vector3 targetPosition2 = hedefKutu.position;
                    Vector3 moveDirection2 = (targetPosition2 - human.position).normalized;
                    human.GetComponent<Rigidbody>().velocity = moveDirection2 * 6;
                    float rotationSpeed = 6f;
                    Quaternion toRotation = Quaternion.LookRotation(moveDirection2, Vector3.up);
                    human.rotation = Quaternion.Lerp(human.rotation, toRotation, Time.deltaTime * rotationSpeed);

                }
                slowTimer = 0;
            }
        }
             
    }

    void CheckCollisions()
    {
        Collider[] colliders = Physics.OverlapSphere(character.position, collisionDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Human"))
            {
                Transform hitHuman = collider.transform;
                if (!humans.Contains(hitHuman))
                {
                    hasCollided = true; // Bayraðý ayarla
                    MoveToFollowPosition(hitHuman, humans);
                    SpawnNewHuman();
                    Destroy(collider.gameObject); // Çarptýðýmýz insaný yok et
                    hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                    audioSource.PlayOneShot(clip);


                }
                else
                {
                    hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                }
            }
            else if (collider.CompareTag("SuperHuman"))
            {
                Transform hitHuman = collider.transform;
                if (!humans.Contains(hitHuman))
                {
                    hasCollided = true; // Bayraðý ayarla
                    MoveToFollowPosition(hitHuman, humans);
                    SpawnNewHuman();
                    SpawnNewHuman();
                    Destroy(collider.gameObject); // Çarptýðýmýz insaný yok et
                    hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                    audioSource.PlayOneShot(clip);

                }
                else
                {
                    hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                }
            }
            else if (collider.CompareTag("SuperKokos")) // Kadýn karakterlere çarpýldýðýnda
            {
                hasCollided = true; // Bayraðý ayarla
                Destroy(collider.gameObject); // Çarptýðýmýz kadýný yok et
                RemoveHuman();
                RemoveHuman();// Arkadaki insan sayýsýný azalt
                hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                audioSource.PlayOneShot(clip2);

            }
            else if (collider.CompareTag("Woman")) // Kadýn karakterlere çarpýldýðýnda
            {
                hasCollided = true; // Bayraðý ayarla
                Destroy(collider.gameObject); // Çarptýðýmýz kadýný yok et
                RemoveHuman(); // Arkadaki insan sayýsýný azalt
                hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                audioSource.PlayOneShot(clip2);

            }
            else if (collider.CompareTag("obstacle2")) // Kadýn karakterlere çarpýldýðýnda
            {
                
                print("simpler duruyor");
                simplerDuruyor = true;
                hasCollided = true; // Bayraðý ayarla
                maxSpeed = 0;
                hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
                
            }
            else if (MobileController.instay2) // Kadýn karakterlere çarpýldýðýnda
            {
                print("ucmanoktasinagirdi");
                ucmaBasladi = true;
                hasCollided = true; // Bayraðý ayarla
                //print("simpler havada durdu");
                //for (int i = 0; i < humans.Count; i++)
                //{
                    //Transform human = humans[i];
                    //Vector3 movementSTOP = new Vector3(0, 0, 0);
                    //human.GetComponent<Rigidbody>().velocity = movementSTOP;
                //}
               
                hasCollided = false; // Ýkinci bir çarpýþma için bayraðý sýfýrla
            }

        }
    }



    void MoveToFollowPosition(Transform character, List<Transform> characterList)
    {
        if (characterList.Count > 1 && oyunBitisi == false)
        {
            Transform previousCharacter = characterList[characterList.Count - 2];
            Vector3 followPosition = previousCharacter.position - (previousCharacter.forward * humanFollowDistance);
            character.position = followPosition;
        }
    }

    void RemoveHuman()
    {
        if (humans.Count > 0)
        {
            Transform lastHuman = humans[humans.Count - 1];
            humans.RemoveAt(humans.Count - 1);
            Destroy(lastHuman.gameObject);
            if (simpSayisi > 0)
            {
                simpSayisi -= 1;
            }
        }
    }

    int girdi = 31;
    void SpawnNewHuman()
    {
        
        int humanCount = humans.Count;
        if (humanCount ==0)
        {
            SpawnHumans();
        }
        else if (humanCount < 30)
        {
            if (humanCount % 8 == 0)
            {
                // Yan yana 7 insan varsa, yeni insanýn önden spawn olmasýný istiyorsanýz:
                Vector3 firstHumanPosition = humans[humanCount - 8].position;
                Vector3 spawnPosition = firstHumanPosition + new Vector3(0, 0, humanSpawnDistance);
                GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
                humans.Add(newHuman.transform);
                ApplyPhysicsMaterial(newHuman.transform);
                ApplyRigidbodyConstraints(newHuman.transform);
                simpSayisi += 1;
            }
            else
            {
                // Yan yana 7 insan yoksa, bir önceki insanýn bir saðýna spawn olmasýný istiyorsanýz:
                Vector3 lastHumanPosition = humans[humanCount - 1].position;
                Vector3 spawnPosition = lastHumanPosition + new Vector3(humanSpawnDistance, 0, 0);
                GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
                humans.Add(newHuman.transform);
                ApplyPhysicsMaterial(newHuman.transform);
                ApplyRigidbodyConstraints(newHuman.transform);
                simpSayisi += 1;
            }
        }
        else if (humans.Count > 30)
        {
            if ((humanCount % 8 == 0) && girdi == 31)
            {
                // Yan yana 7 insan varsa, yeni insanýn arkadan spawn olmasýný istiyorsanýz (tek seferlik index sýfýrlamak için)
                Vector3 firstHumanPosition = humans[0].position;
                Vector3 spawnPosition = firstHumanPosition + new Vector3(0, 0, -humanSpawnDistance);
                GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
                humans.Add(newHuman.transform);
                ApplyPhysicsMaterial(newHuman.transform);
                ApplyRigidbodyConstraints(newHuman.transform);
                simpSayisi += 1;
                girdi += 1;
            }
            else if ((humanCount % 8 == 0) && girdi != 31)
            {
                // Yan yana 7 insan varsa, yeni insanýn arkadan spawn olmasýný istiyorsanýz:
                Vector3 firstHumanPosition = humans[humanCount - 8].position;
                Vector3 spawnPosition = firstHumanPosition + new Vector3(0, 0, -humanSpawnDistance);
                GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
                humans.Add(newHuman.transform);
                ApplyPhysicsMaterial(newHuman.transform);
                ApplyRigidbodyConstraints(newHuman.transform);
                simpSayisi += 1;
            }
            else
            {
                // Yan yana 7 insan yoksa, bir önceki insanýn bir saðýna spawn olmasýný istiyorsanýz:
                Vector3 lastHumanPosition = humans[humanCount - 1].position;
                Vector3 spawnPosition = lastHumanPosition + new Vector3(humanSpawnDistance, 0, 0);
                GameObject newHuman = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
                humans.Add(newHuman.transform);
                ApplyPhysicsMaterial(newHuman.transform);
                ApplyRigidbodyConstraints(newHuman.transform);
                simpSayisi += 1;
            }
        }
        
    }


    void ApplyPhysicsMaterial(Transform humanTransform)
    {
        Collider humanCollider = humanTransform.GetComponent<Collider>();
        if (humanCollider != null)
        {
            PhysicMaterial material = new PhysicMaterial();
            material.frictionCombine = PhysicMaterialCombine.Maximum;
            material.bounceCombine = PhysicMaterialCombine.Maximum;

            // Bu iki özellikle collider'larýn iç içe geçmesini azaltabilirsiniz.
            material.dynamicFriction = 0.2f;
            material.staticFriction = 0.2f;

            humanCollider.material = material;
        }
    }

    void ApplyRigidbodyConstraints(Transform humanTransform)
    {
        Rigidbody humanRigidbody = humanTransform.GetComponent<Rigidbody>();
        if (humanRigidbody != null)
        {
            humanRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
