using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float transitionTime;

    private Vector3 roomPosition = Vector3.zero;

    private bool _onLab;

    void Start()
    {
        _onLab = true;
    }

    void Update()
    {
        _onLab = GameManager.instance.OnLab();
        if (!_onLab)
        {
            ChangeOrthoSize(5, 0.5f);
            TransitionToRoom();
        }
        else
        {
            ChangeOrthoSize(10, 0.1f);
            SetPosition(new Vector2(0, 18));
            MovePosition();
        }
    }

    private void TransitionToRoom()
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

    private void ChangeOrthoSize(float size, float time = 1)
    {
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(
            GetComponent<Camera>().orthographicSize,
            size,
            time
        );
    }

    public void SetRoomPosition(Transform room)
    {
        roomPosition = room.position;
        roomPosition.z = transform.position.z;
    }

    public void SetPosition(Vector3 position)
    {
        roomPosition = position;
        roomPosition.z = transform.position.z;
    }

    private void MovePosition()
    {
        if (roomPosition != transform.position)
        {
            transform.position = roomPosition;
        }
    }
}
