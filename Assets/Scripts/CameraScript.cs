using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;
    [SerializeField] float m_minZoom = 5f;
    [SerializeField] float m_maxZoom = 20f;

    private void Update()
    {
        FixedCameraFollowSmooth(cam, player1, player2);
    }
    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        
        float zoomFactor = 1f;
        float followTimeDelta = 0.8f;

        Vector3 midpoint = (t1.position + t2.position) / 2f;

      
        float distance = (t1.position - t2.position).magnitude;

        if (distance >m_minZoom && distance <= m_maxZoom)
        {
            Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
            if (cam.orthographic)
            {

                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,distance,followTimeDelta);
            }

            cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);
           
                cam.transform.position = new Vector3(cam.transform.position.x, cam.orthographicSize-5, cam.transform.position.z);
           
             if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
         cam.transform.position = cameraDestination;
        }
    }
}
