using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private static Player _player;

    public Collider2D collider;
    
    // Time for death animation to play out
    public float lifetimeAfterDetachment = 1;

    // Displacement of the unit relative to bottom unit of player
    // after attachment
    public Vector3 offsetOnAttachment;

    // Determines if the unit currently attached
    public bool attached = false;
    // Determines if the unit was dropped due to damage
    public bool dropped = false;

    private Unit _unitAbove;
    private Unit _unitBelow;
    
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
        transform.position = _player.bottomUnit.transform.position + offsetOnAttachment;

        _unitAbove = _player.bottomUnit;
        _unitAbove._unitBelow = this;
        _player.bottomUnit = this;
    }
    
    public void Detach()
    {
        dropped = true;
        attached = false;
        
        if (_unitBelow != null)
            _unitBelow.Detach();
        _player.bottomUnit = _unitAbove;

        transform.SetParent(null, true);

        _detachmentTime = Time.time;
        collider.enabled = false;
    }
    
    // Using triggers to minimize effect on physics due to collisions.
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Ignore dropped items (redundant as of now due to collider disabling at drop)
        if (dropped == true)
        {
            return;
        }
        
        // Trying to attach unattached unit
        if (!attached && col.gameObject.GetComponentInChildren<Unit>())
        {
            Attach();
        }
        // Checking for hit for an attached unit
        else if (attached && col.gameObject.GetComponent<Obstacle>())
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
