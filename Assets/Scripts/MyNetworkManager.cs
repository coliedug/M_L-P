using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public static GameObject _Server { get; private set; }
    public static InteractionManager _InteractionManager { get; private set; }
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        if (_Server != null && _Server != this)
        {
            Destroy(this);
        }
        else
        {
            _Server = gameObject;
            _InteractionManager = gameObject.GetComponent<InteractionManager>();
        }
    }
}
