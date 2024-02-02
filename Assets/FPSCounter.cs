using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text Text;

    void Start()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 60;
#endif
    }

    // Update is called once per frame
    private int previousFps;

    public void Update()
    {
        var curFps = (int)(1f / Time.unscaledDeltaTime);
        Text.text = "FPS: " + curFps;
    }
}