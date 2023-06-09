using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    private float y;
    public float speedFollow = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 followPos = target.position + offset;

        RaycastHit hit;

        if (Physics.Raycast(target.position, Vector3.down, out hit, 2.5f))
        {
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * speedFollow);
        }
        else
        {
            y = Mathf.Lerp(y, target.position.y, Time.deltaTime * speedFollow);
        }
        followPos.y = offset.y;
        transform.position = followPos;
    }
}
