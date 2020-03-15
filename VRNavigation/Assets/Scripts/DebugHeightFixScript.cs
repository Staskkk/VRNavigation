using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHeightFixScript : MonoBehaviour
{
    public CapsuleCollider ovrCameraRigCollider;

    void Start()
    {
#if UNITY_EDITOR
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
        ovrCameraRigCollider.center = new Vector3(ovrCameraRigCollider.center.x,
            ovrCameraRigCollider.center.y + 1, ovrCameraRigCollider.center.z);
#endif
    }
}
