using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPointsSetter : MonoBehaviour
{
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private Transform StartSprite;
    [SerializeField] private Transform EndSprite;

    void Start()
    {
        if (LineRenderer.positionCount <= 0) return;
        var lPos = LineRenderer.transform.position;
        StartSprite.position = LineRenderer.GetPosition(0) + lPos;
        EndSprite.position = LineRenderer.GetPosition(LineRenderer.positionCount - 1) + lPos;
    }

    // Update is called once per frame
    void Update()
    {
    }
}