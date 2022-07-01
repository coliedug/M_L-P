using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableType : MonoBehaviour
{
    public InteractionManager.objectTypes objectType;

    public void Interact()
    {
        MyNetworkManager._Server.GetComponent<InteractionManager>().AskServerInteract(objectType, gameObject);
    }
}
