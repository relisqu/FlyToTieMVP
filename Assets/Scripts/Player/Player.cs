using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // top and bottom units always known for ease of attachment/detachment
    public Unit topUnit;
    public Unit bottomUnit;

    private void Start()
    {
        Unit starterUnit = FindObjectOfType<StarterUnit>().gameObject.GetComponent<Unit>();

        topUnit = starterUnit;
        bottomUnit = starterUnit;
    }

    public void Damage(Unit unit)
    {
        // Hopefully will never happen
        if (!unit.attached)
        {
            throw new Exception("Damaging unattached unit");
        }
        
        // Case of losing
        if (unit == topUnit && topUnit == bottomUnit)
        {
            Debug.Log("Lost");
            // TO DO lose condition
            return;
        }
        
        // Case of top unit being hit
        if (unit == topUnit)
        {
            bottomUnit.Detach();
        }
        // Any other unit being hit
        else
        {
            unit.Detach();
        }
    }
}