using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [Header("Contoller Variables")]
    public float inputTime = .1f;

    [SerializeField] Camera cam;
    [SerializeField] PartyMapContoller party;

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

    float inputDelay = 0f;
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
        if (Input.GetButtonDown("Fire1"))
        {
            inputDelay = Time.time + inputTime;
        }
        if (Input.GetButton("Fire1"))
        {
            Vector3 _mouseMov = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector3 _camMov = cam.transform.position - _mouseMov / 8 * (cam.orthographicSize / 2);
            cam.transform.position = _camMov;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if(inputDelay > Time.time)
            {
                Vector3 _move = cam.ScreenToWorldPoint(Input.mousePosition);
                _move.z = 0;
                party.MoveTo(_move);
            }
        }
    }
}
