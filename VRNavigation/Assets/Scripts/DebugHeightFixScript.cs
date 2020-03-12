using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHeightFixScript : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
#endif
    }
}
