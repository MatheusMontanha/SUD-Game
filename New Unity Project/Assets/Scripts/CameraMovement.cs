using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothing;

    public float maxSize;
    public float minSize;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void LateUpdate()
    {
        if (player1.gameObject.active == true && player2.gameObject.active == true)
        {
            FixedCameraFollowSmooth();
            return;
        }
        else
        {
            Transform target = transform;
            if (player1.gameObject.active == false)
            {
                target = player2;

            }
            else if(player2.gameObject.active == false)
            {
                target = player1;
            }
            if (transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }


    }


    public void FixedCameraFollowSmooth()
    {
        float zoomFactor = 1f;

        Vector3 midpoint = (player1.position + player2.position) / 2f;

        float distance = (player1.position - player2.position).magnitude;

        distance = Mathf.Max(distance, minSize);
        distance = Mathf.Min(distance, maxSize);
        Vector3 cameraDestination = midpoint - Camera.main.transform.forward * distance * zoomFactor;


        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = distance;
        }
        transform.position = Vector3.Lerp(transform.position,
              cameraDestination,
              smoothing);
        if ((cameraDestination - Camera.main.transform.position).magnitude <= 0.05f)
            Camera.main.transform.position = cameraDestination;
    }
}
