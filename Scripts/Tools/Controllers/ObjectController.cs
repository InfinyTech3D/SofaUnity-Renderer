﻿using System;
using UnityEngine;
using SofaUnity;

/// <summary>
/// Script to Add to a GameOject in order to move it using the keybord.
/// I = move forward
/// K = move backword
/// U = move left
/// O = move right
/// J = move Up
/// L = move down
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// Pointer to the current Mesh of the GameObject
    public GameObject light = null;
    public GameObject otherTool = null;


    protected bool m_isactive = false;
    protected ObjectController otherObjectCtrl = null;

    public float factor = 0.05f;
    void Start()
    {
        if (light)
            light.SetActive(m_isactive);

        if (otherTool)
            otherObjectCtrl = otherTool.GetComponent<ObjectController>();
    }

    /// Method calle at each update, will move the object regardings keys pushed.
    void FixedUpdate()
    {
        if (!m_isactive)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.Keypad4))
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 0.5f, transform.eulerAngles.z);
            else if (Input.GetKey(KeyCode.Keypad6))
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 0.5f, transform.eulerAngles.z);
            else if (Input.GetKey(KeyCode.Keypad8))
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 0.5f, transform.eulerAngles.y, transform.eulerAngles.z);
            else if (Input.GetKey(KeyCode.Keypad5))
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - 0.5f, transform.eulerAngles.y, transform.eulerAngles.z);
            else if (Input.GetKey(KeyCode.Keypad7))
                transform.position = transform.position - transform.forward * factor;
            else if (Input.GetKey(KeyCode.Keypad9))
                transform.position = transform.position + transform.forward * factor;
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad8))
                transform.position = transform.position + transform.up * factor;
            else if (Input.GetKey(KeyCode.Keypad5))
                transform.position = transform.position - transform.up * factor;
            else if (Input.GetKey(KeyCode.Keypad4))
                transform.position = transform.position - transform.right * factor;
            else if (Input.GetKey(KeyCode.Keypad6))
                transform.position = transform.position + transform.right * factor;
            else if (Input.GetKey(KeyCode.Keypad7))
                transform.position = transform.position - transform.forward * factor;
            else if (Input.GetKey(KeyCode.Keypad9))
                transform.position = transform.position + transform.forward * factor;
        }

        if (Input.GetKey(KeyCode.C) && light)
        {
            Light lt = light.GetComponent<Light>();
            lt.color = Color.red;
        }
        else if (Input.GetKey(KeyCode.V))
        {
            Light lt = light.GetComponent<Light>();
            lt.color = Color.green;
        }
    }

    void activateTool(bool value)
    {
        m_isactive = value;
        if (light)
            light.SetActive(m_isactive);
    }

    bool isToolActive()
    {
        return m_isactive;
    }

    void OnMouseDown()
    {
        activateTool(m_isactive = !m_isactive);

        if (otherObjectCtrl)
            otherObjectCtrl.activateTool(!m_isactive);
    }

}
