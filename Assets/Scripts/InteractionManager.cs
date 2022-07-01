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

    [Command]
    public void AskServerInteract(objectTypes objectType, GameObject askingObject)
    {
        Debug.Log("Running command");
        //Interact(objectType, askingObject);
    }

    [ClientRpc]
    private void Interact(objectTypes objectType, GameObject askingObject)
    {
        switch(objectType)
        {
            case objectTypes.BlueCube:
                askingObject.transform.Translate(new Vector3(0, 0.5f, 0));
                break;
            case objectTypes.RedCube:
                askingObject.transform.Rotate(new Vector3(0, 45, 0));
                break;
        }
    }
}
