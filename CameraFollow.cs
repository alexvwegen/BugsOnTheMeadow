using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public bool Orthographic;
    private Vector3 offset;
    private Vector3 velocity;
    public float smoothTime;
    public float minZoom;
    public float maxZoom;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
        velocity = Vector3.zero;
    }

    private void Update()
    {
        if (Orthographic == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && cam.orthographicSize < maxZoom)
            {
                cam.orthographicSize += .5f;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && cam.orthographicSize > minZoom)
            {
                cam.orthographicSize -= .5f;
            }
        } else {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && cam.fieldOfView < maxZoom)
            {
                cam.fieldOfView += 2.5f;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && cam.fieldOfView > minZoom)
            {
                cam.fieldOfView -= 2.5f;
            }
        }
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
