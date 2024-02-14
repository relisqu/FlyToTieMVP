using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text Text;

    void Start()
    {
#if UNITY_ANDROID

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#endif
    }

    // Update is called once per frame
    private int previousFps;

    public void Update()
    {
        if (showWarning) return;
        var curFps = (int)(1f / Time.unscaledDeltaTime);
        Text.text = "FPS: " + curFps;
        if (curFps < 31)
        {
            StartCoroutine(ShowWrongFps(curFps));
        }
    }


    private bool showWarning;

    public IEnumerator ShowWrongFps(int curFps)
    {
        showWarning = true;
        Text.text = "Warning: " + curFps;
        yield return new WaitForSeconds(0.5f);
        showWarning = false;
    }
}