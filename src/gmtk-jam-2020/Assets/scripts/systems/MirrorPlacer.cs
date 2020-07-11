using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class MirrorPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _mirror;

    private Plane plane;
    private Vector3? startPos;
    private GameObject obj;

    private void Start()
    {
        plane = new Plane(-Vector3.forward, Vector3.zero);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                startPos = ray.GetPoint(distance);
                obj = GameObject.Instantiate(_mirror);
                obj.transform.position = startPos.Value;
            }
        }
        if (obj)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                var endPos = ray.GetPoint(distance);
                obj.transform.up = (endPos - startPos.Value).normalized;
            }
            if (Input.GetMouseButtonUp(0))
            {
                startPos = null;
                obj = null;
            }
        }
        else
        {
            startPos = null;
            obj = null;
        }

    }
}
