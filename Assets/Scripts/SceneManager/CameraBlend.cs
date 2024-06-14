using UnityEngine;
using Cinemachine;

public class Switcheroo : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;
    public float time = 0.5f;
    public float time2 = 3f;

    void Update()
    {
        var brain = GetComponent<CinemachineBrain>();
        
        if (brain.IsLive(vcam1))
        {
            brain.m_DefaultBlend.m_Time = time;
        }
        else if (brain.IsLive(vcam2))
        {
            brain.m_DefaultBlend.m_Time = time2;
        }
    }
}