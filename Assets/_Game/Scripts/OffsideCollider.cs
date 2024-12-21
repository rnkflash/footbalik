using System;
using _Game.Scripts;
using UnityEngine;

public class OffsideCollider : MonoBehaviour
{
    Offside offside;
    
    void Start()
    {
        offside = GetComponentInParent<Offside>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            offside.Sound();
        }
    }
}
