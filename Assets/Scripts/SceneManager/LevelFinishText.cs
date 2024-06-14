using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public TextMeshPro myTextMesh; // 3D Text nesnesini tutacak de�i�ken

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
        // E�er myTextMesh atanm��sa, metin i�eri�ini g�ncelle
        if (myTextMesh != null)
        {
            myTextMesh.text = "Toplanan: " + Manager.simpSayisi + "/15";
        }
    }
}
