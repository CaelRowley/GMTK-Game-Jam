using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCamera : MonoBehaviour {

    public Camera camera;
    public bool flipCamera = false;


    void OnPreCull() {
        camera.ResetWorldToCameraMatrix();
        camera.ResetProjectionMatrix();
        int y = flipCamera? -1 : 1;
        camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(1, y, 1));
    }

    void OnPreRender() {
        if(flipCamera)
            GL.SetRevertBackfacing(true);
    }

    void OnPostRender() {
        if(flipCamera)
            GL.SetRevertBackfacing(false);
    }

}
