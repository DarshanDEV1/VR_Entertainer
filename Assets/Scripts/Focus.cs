using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
using Unity.XR.CoreUtils;

public class Focus : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] GameObject _cursor;
    [SerializeField] GameObject _target;
    [SerializeField] Camera _camera; // Add this line

    private void Start()
    {
        _cursor = Instantiate(_cursor, _camera.transform.position + _camera.transform.forward, Quaternion.identity);
    }

    private void Update()
    {
        Vector3 cameraPosition = _camera.transform.position;
        Vector3 cameraForward = _camera.transform.forward;

        CastRay(cameraPosition, cameraForward, Api.IsTriggerPressed);
    }

    private void CastRay(Vector3 origin, Vector3 direction, bool _config)
    {
        RaycastHit _hit;
        if (Physics.Raycast(origin, direction, out _hit, 50f, _layer))
        {
            if (_config)
            {
                var stain = Instantiate(_target, _hit.point, Quaternion.identity);
                stain.transform.up = _hit.normal;

                Movement(transform.position, _hit.point, _hit.normal);
            }
            else
            {
                _cursor.transform.position = _hit.point;
                _cursor.transform.up = _hit.normal;

                _cursor.transform.position = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
            }
        }
    }

    private void Movement(Vector3 _origin_position, Vector3 _destination_position, Vector3 _destination_normal)
    {
        // Teleport the player and all its children to the new location
        transform.position = new Vector3(_destination_position.x, _destination_position.y + 3.4f, _destination_position.z);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        // If you want the player to face the same direction as the stain
        transform.up = _destination_normal;
    }
}
