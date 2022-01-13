using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Photon.Pun;

public class RequestOwnership : Grabbable
{
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public override void OnGrab(Hand hand)
    {
        photonView.RequestOwnership();
        base.OnGrab(hand);
    }
}
