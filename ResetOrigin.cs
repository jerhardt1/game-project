using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class FloatingOrigin : MonoBehaviour
{
    public float threshold = 100.0f;

    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;

        if (cameraPosition.magnitude > threshold)
        {
            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    g.transform.position -= cameraPosition;
                }
            }
            
        }
    }
}