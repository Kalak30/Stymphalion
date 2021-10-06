using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour{
    private String name;

    public abstract void touchingInteractable();
}