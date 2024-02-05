using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Units
{
    public class UnitSpawn : MonoBehaviour
    {
        [SerializeField]private Transform FirstCircle;
        [SerializeField]private Transform SecondCircle;
        [SerializeField]private float InnerSpeed;
        [SerializeField]private float OuterSpeed;
        private void Update()
        {
            FirstCircle.RotateAround(FirstCircle.position, Vector3.forward, InnerSpeed*Time.deltaTime); 
            SecondCircle.RotateAround(SecondCircle.position, Vector3.forward, OuterSpeed*Time.deltaTime); 
        }

        private IEnumerator Start()
        {
            
            FirstCircle.DOScale(FirstCircle.transform.localScale.x*0.9f, 1.9f).SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(0.4f);
            SecondCircle.DOScale(SecondCircle.transform.localScale.x*0.9f, 1.9f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}