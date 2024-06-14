using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro myTextMesh; // 3D Text nesnesini tutacak deðiþken

    public EndlessRunnerManager Manager;

    void Start()
    {
        myTextMesh.text = "Toplanan: " + Manager.simpSayisi;
        UpdateText();
    }

    void Update()
    {
        UpdateText();
    }
    void UpdateText()
    {
        // Eðer myTextMesh atanmýþsa, metin içeriðini güncelle
        if (myTextMesh != null)
        {
            myTextMesh.text = "Toplanan: " + Manager.simpSayisi + "/15";
        }
    }
}
