using System;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        if (!unit.attached)
        {
            throw new Exception("Damaging unattached unit");
        }

        if (unit == topUnit && topUnit == bottomUnit)
        {
            Debug.Log("Lost");
            // TO DO lose condition
            return;
        }
        if (unit == topUnit)
        {
            Detach(bottomUnit);
        }
        else
        {
            Detach(unit);
        }
    }

    public void Attach(Unit unit)
    {
        unit.Attach();
    }

    public void Detach(Unit unit)
    {
        unit.Detach();
    }
}