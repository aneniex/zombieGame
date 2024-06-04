using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySettingsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SystemInfo.systemMemorySize <= 2400)
        {
            Debug.Log("QUALITY LEVEL 0");
            QualitySettings.SetQualityLevel(0);
        }
        else if(SystemInfo.systemMemorySize > 2400 &&  SystemInfo.systemMemorySize <= 4400)
        {
            Debug.Log("QUALITY LEVEL 1");
            QualitySettings.SetQualityLevel(1);
        }
        else
        {
            Debug.Log("QUALITY LEVEL 2");
            QualitySettings.SetQualityLevel(2);
        }
    }
}
