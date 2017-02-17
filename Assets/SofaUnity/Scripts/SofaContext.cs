﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SofaUnityAPI;


namespace SofaUnity
{
    public class SofaContext : MonoBehaviour
    {
        SofaContextAPI m_impl;

        void Awake()
        {
            Debug.Log("SofaContext::Awake called.");
            m_impl = new SofaContextAPI();
        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("SofaContext::Start called.");
            GL.wireframe = true;
            m_impl.start();
        }

        void OnDestroy()
        {
            m_impl.stop();
            m_impl.Dispose();
        }

        void OnPreRender()
        {
            GL.wireframe = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Debug.Log("SofaContext::Update called.");
            m_impl.step();
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_impl.step();
            }
        }

        Vector3 m_gravity = new Vector3(0f, -9.8f, 0f);
        public Vector3 gravity
        {
            get { return m_gravity; }
            set
            {
                
                //if (_ddWorld != null)
                //{
                //    BulletSharp.Math.Vector3 grav = value.ToBullet();
                //    _ddWorld.SetGravity(ref grav);
                //}
                m_gravity = value;
            }
        }

        float m_timeStep = 0.02f; // ~ 1/60
        public float timeStep
        {
            get
            {
                return m_timeStep;
            }
            set
            {
                //if (lateUpdateHelper != null)
                //{
                //    lateUpdateHelper.m_fixedTimeStep = value;
                //}
                m_timeStep = value;
            }
        }
    }
}
