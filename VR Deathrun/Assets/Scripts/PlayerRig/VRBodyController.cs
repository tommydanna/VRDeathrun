using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap 
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map() 
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

}

public class VRBodyController : MonoBehaviour
{
    public float turnSmoothness = 1f;

    public VRMap head;
    public VRMap rHand;
    public VRMap lHand;

    public Transform headConstraints;
    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraints.position;
    }

    void LateUpdate()
    {
        transform.position = headConstraints.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraints.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        lHand.Map();
        rHand.Map();
    }
}
