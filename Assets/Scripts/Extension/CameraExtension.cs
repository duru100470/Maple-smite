using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtension
{
    public static (float, float) GetFrustum(this Camera camera)
    {
        var frustumHeight = 2.0f * Mathf.Abs(camera.transform.position.z) * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * camera.aspect;

        return (frustumWidth, frustumHeight);
    }
}