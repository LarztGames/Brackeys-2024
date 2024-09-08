using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float transitionTime;

    private Vector3 roomPosition = Vector3.zero;

    void Update()
    {
        if (roomPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                roomPosition,
                transitionTime * Time.deltaTime
            );
        }
    }

    public void SetRoomPosition(Transform room)
    {
        roomPosition = room.position;
        roomPosition.z = transform.position.z;
    }
}
