using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossCanBari : MonoBehaviour
{
    public Slider healthSlider; // Unity editöründen can barını bağlamak için
    private int maxHealth = 100;
    private int currentHealth;
    private Animator mAnimator;
    private bool isBossDefeated;
    public CinemachineVirtualCamera A_Camera;
    public CinemachineVirtualCamera B_Camera;
    public GameObject hero;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioSource audioSource;
    private bool girdiMiAcaba = false;

    FadeInOut fade;

    void Start()
    {
        hero = GetComponent<GameObject>();
        fade = FindObjectOfType<FadeInOut>();
        isBossDefeated = false;
        mAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        // Başlangıçta can barını güncelle
        UpdateHealthUI();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            healthSlider.gameObject.SetActive(false);
            isBossDefeated = true;
        }
        if (isBossDefeated && girdiMiAcaba == false)
        {
            girdiMiAcaba = true;
            A_Camera.Priority += 200;
            mAnimator.SetTrigger("bossDefeated");
            StartCoroutine(_ChangeSceneWin());
        }
    }

    public IEnumerator _ChangeSceneWin()
    {
        yield return new WaitForSeconds(3);
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("WinMenu");
    }

    public IEnumerator _ChangeSceneLose()
    {
        yield return new WaitForSeconds(6);
        audioSource.PlayOneShot(clip6);
        if (currentHealth > 0)
        {
            B_Camera.Priority += 200;
            mAnimator.SetTrigger("bossWon");
            yield return new WaitForSeconds(4);
            fade.FadeIn();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("LoseMenu");
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        
            
        if (other.gameObject.CompareTag("Player") && currentHealth > 0)
        {
                DecreaseHealth(30);
                mAnimator.SetTrigger("hit");
                audioSource.PlayOneShot(clip5);
                StartCoroutine(_ChangeSceneLose());
                

        }
        else if (other.gameObject.CompareTag("Human") && currentHealth > 0)
        {
                DecreaseHealth(10);
                mAnimator.SetTrigger("hit");
                audioSource.PlayOneShot(clip5);
        }      
    }

    void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        // Canın sıfırdan küçükse sıfır yap
        if (currentHealth <= 0)
        {
            healthSlider.gameObject.SetActive(false);
        }

        // Can barını güncelle
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        // Can barını güncelle
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
