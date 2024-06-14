using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiterr : MonoBehaviour
{
    public int TargetFPS = 60;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = TargetFPS;
    }
}
