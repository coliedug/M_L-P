using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InteractionManager : NetworkBehaviour
{
    public enum objectTypes
    {
        Door,
        Button,
        Player,
        RedCube,
        BlueCube
    }
    public objectTypes objectType;

    [ClientRpc]
    public void Interact()
    {
        Debug.Log("Interacting");
        switch(objectType)
        {
            case objectTypes.BlueCube:
                transform.Translate(new Vector3(0, 0.5f, 0));
                break;
            case objectTypes.RedCube:
                transform.Rotate(new Vector3(0, 45, 0));
                break;
        }
    }
}
