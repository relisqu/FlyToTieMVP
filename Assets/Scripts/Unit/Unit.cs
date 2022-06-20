using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Collider2D collider;
    public float lifetimeAfterDetachment = 1;
    
    public Unit unitAbove;
    public Unit unitBelow;

    public Vector3 offsetOnAttachement;

    private static Player _player;

    public bool attached = false;
    public bool dropped = false;

    private void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
    }

    private float _detachmentTime;

    public void Attach()
    {
        attached = true;
        
        transform.SetParent(_player.transform, true);
        transform.position = _player.bottomUnit.transform.position + offsetOnAttachement;

        unitAbove = _player.bottomUnit;
        unitAbove.unitBelow = this;
        _player.bottomUnit = this;
    }
    
    public void Detach()
    {
        dropped = true;
        attached = false;
        
        if (unitBelow != null)
            unitBelow.Detach();
        _player.bottomUnit = unitAbove;

        transform.SetParent(null, true);

        _detachmentTime = Time.time;
        collider.enabled = false;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (dropped == true)
        {
            return;
        }
        
        if (!attached && col.gameObject.GetComponentInChildren<Unit>())
        {
            _player.Attach(this);
        }

        if (attached && col.gameObject.GetComponent<Obstacle>())
        {
            _player.Damage(this);
        }
    }

    private void Update()
    {
        if (dropped)
        {
            if (Time.time - _detachmentTime > lifetimeAfterDetachment)
            {
                Destroy(gameObject);
            }
        }
    }
}
