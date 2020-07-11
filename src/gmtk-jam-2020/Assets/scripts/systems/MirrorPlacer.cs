﻿using UnityEngine;

public class MirrorPlacer : MonoBehaviour
{
    [SerializeField] private Mirror _mirror;

    private Plane plane;
    private Vector3? startPos;
    private Mirror obj;

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
                obj = GameObject.Instantiate<Mirror>(_mirror);
                obj.gameObject.SetActive(false);
                obj.transform.position = startPos.Value;
                obj.SetSize(0);
            }
        }
        if (obj)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                var endPos = ray.GetPoint(distance);
                obj.gameObject.SetActive(true);
                obj.transform.up = (endPos - startPos.Value).normalized;
                obj.SetSize(f((endPos - startPos.Value).magnitude));
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
    private float f(float f)
    {
        return Mathf.Clamp(Mathf.Pow(f + 1, 2) - 1, 0, 1.7f);
    }
}
