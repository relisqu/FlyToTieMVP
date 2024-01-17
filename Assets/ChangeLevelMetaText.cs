using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ChangeLevelMetaText : MonoBehaviour
{
    [SerializeField] private List<int> LevelList;
    [SerializeField] private TMPro.TMP_Text Text;
    void Start()
    {
        PlayerData.ChangedLevel += ChangeText;
        ChangeText();
    }

    private void OnDestroy()
    {
        PlayerData.ChangedLevel -= ChangeText;
    }

    private void ChangeText()
    {
        foreach (var level in LevelList)
        {
            if (level > PlayerData.СurrentLevel)
            {
                Text.SetText("New content opens on level "+level+"!");
                break;
            }
            Text.SetText("New content in the next update! Stay tuned!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
