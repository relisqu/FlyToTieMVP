using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] private float Time;

    private void OnEnable()
    {
        StartCoroutine(WaitDisable());
    }

    IEnumerator WaitDisable()
    {
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);
    }
}