using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet {
    public float Value;
    public float Range;
    public float Chance;
    private PetType petType;

    public Pet(float value, float range, float chance, PetType pettype)
    {
        Value = value;
        Range = range;
        Chance = chance;
        petType = pettype;
    }

    public void PetProc()
    {
        //switch ()
    }
}
