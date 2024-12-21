using System;
using UnityEngine;
using UnityEngine.Events;

public class KickAnimation : MonoBehaviour
{
    public UnityEvent kickEvent = new UnityEvent();
    
    public void Puknum(string puk)
    {
        if (puk == "Kick")
        {
            kickEvent.Invoke();
        }
    }
}
