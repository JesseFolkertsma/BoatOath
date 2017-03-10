using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    [SerializeField] Camera cam;

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("HEY DIPSHIT, CAM IS NIET SETUP!");
            enabled = false;
        }
    }

    void Update()
    {
        MouseInputs();
    }

    void MouseInputs()
    {
        //Scrollwheel Input
        float _scroll = Input.mouseScrollDelta.y;
        if (_scroll != 0)
        {
            cam.orthographicSize -= _scroll;
            if (cam.orthographicSize < 1)
                cam.orthographicSize = 1;
        }
        //Mousebuttons Input
        if (Input.GetButton("Fire1"))
        {
            Vector3 _mouseMov = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 _camMov = cam.transform.position - _mouseMov /3;
            cam.transform.position = _camMov;
        }
    }
}
