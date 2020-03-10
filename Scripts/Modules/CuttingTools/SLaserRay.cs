﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// Specialisation of SRayCaster class
/// Will comunicate with Sofa ray caster and allow several interaction using a ray:
/// Grasping and fixing pointes and deleting elements.
/// </summary>
public class SLaserRay : SRayCaster
{    
    /// Direction of the laser ray in local coordinate 
    public Vector3 m_axisDirection = new Vector3(1.0f, 0.0f, 0.0f);
    /// Translation of the origin of the laser ray from the origin of the GameObject in world coordinate
    public Vector3 m_translation = new Vector3(0.0f, 0.0f, 0.0f);

    /// Booleen to activate or not that tool
    public bool m_isActivated = false;

    /// Booleen to draw the effective ray sent to Sofa ray caster
    public bool drawRay = false;

    public float m_stiffness = 10000f;
    protected float oldStiffness = 10000f;

    /// Enum that set the type of interaction to plug to this tool on sofa side
    public SofaDefines.SRayInteraction m_laserType;


    /// Laser object
    /// {    
    /// Laser material
    public Material laserMat = null;
    /// Laser GameObject
    protected GameObject laser = null;

    /// Booleen to draw the laser object
    public bool drawLaserParticles = false;

    /// Laser renderer
    protected LineRenderer lr;
    [SerializeField]
    public Color startColor = Color.green;
    [SerializeField]
    public Color endColor = Color.green;
    [SerializeField]
    public float width = 0.15f;

    // Light emitted by the laser origin
    protected GameObject lightSource = null;
    protected Light light;

    // Light Particle system following the lineRenderer
    protected ParticleSystem ps;
    protected bool psInitialized = false;
    public Material particleMat;
    /// }
    protected float m_startSpeed = 100;


    /// Protected method that will really create the Sofa ray caster
    protected override void createSofaRayCaster()
    {
        // Create Laser
        if (laser == null)
        {
            laser = new GameObject("Laser");
            laser.transform.parent = this.transform;
            laser.transform.localPosition = Vector3.zero;
            laser.transform.localRotation = Quaternion.identity;
            laser.transform.localScale = Vector3.one * 0.1f;
        }

        if (drawLaserParticles && lightSource == null)
        {
            // Create light source
            lightSource = new GameObject("Light");
            lightSource.transform.parent = laser.transform;
            lightSource.transform.localPosition = Vector3.zero;
            lightSource.transform.localRotation = Quaternion.identity;
            lightSource.transform.localScale = Vector3.one;

            initializeLaser();

            // initialise for the first time the particule system
            if (psInitialized == false)
                initializeParticles();            
        }

        if (drawRay)
            initialiseRay();

        this.activeTool(false);

        // Get access to the sofaContext
        IntPtr _simu = m_sofaContext.GetSimuContext();
        if (_simu != IntPtr.Zero && m_sofaRC == null)
        {

            float raySofaLength = length * m_sofaContext.GetFactorUnityToSofa(1);
            if (m_laserType == SofaDefines.SRayInteraction.CuttingTool)
            {
                m_sofaRC = new SofaRayCaster(_simu, 0, base.name, raySofaLength*2);
                Debug.Log(this.name + " create SofaRayCaster CuttingTool with length: " + raySofaLength);
            }
            else if (m_laserType == SofaDefines.SRayInteraction.AttachTool)
            {
                m_sofaRC = new SofaRayCaster(_simu, 1, base.name, raySofaLength);
                Debug.Log(this.name + " create SofaRayCaster AttachTool with length: " + raySofaLength);
            }
            else if (m_laserType == SofaDefines.SRayInteraction.FixTool)
            {
                m_sofaRC = new SofaRayCaster(_simu, 2, base.name, raySofaLength);
                Debug.Log(this.name + " create SofaRayCaster FixTool with length: " + raySofaLength);
            }
            else
            {
                m_sofaRC = null;
                m_isReady = false;
            }

            base.createSofaRayCaster();
        }

        if (m_sofaRC == null)
        {
            Debug.Log(this.name + " No SofaRayCaster created");
        }
        else
        {
            m_isReady = true;
        }
    }

    //private void OnDestroy()
    //{
    //    if (m_sofaRC != null)
    //    {
    //        m_sofaRC.Dispose();
    //        m_sofaRC = null;
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        if (!startOnPlay)
            return;

        m_axisDirection.Normalize();
        //if (m_sofaContext.testAsync == true)
        //    m_sofaContext.registerCaster(this);
        //else
        //    automaticCast = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isReady)
            return;

        if (Input.GetKey(KeyCode.A))
            activeTool(true);

        // compute the direction and origin of the ray by adding object transform + additional manual transform
        Vector3 transLocal = transform.TransformVector(m_translation);
        origin = transform.position + transLocal;
        direction = transform.forward * m_axisDirection[0] + transform.right * m_axisDirection[1] + transform.up * m_axisDirection[2];

        // update the light source
        if (drawLaserParticles && lightSource)
            lightSource.transform.position = origin + transLocal;


        if (automaticCast && m_sofaRC != null)
        {
            int triId = -1;
            // get the id of the selected triangle. If < 0, no intersection
            if (m_isActivated)
            {
                Vector3 originS = m_sofaContext.transform.InverseTransformPoint(origin);
                Vector3 directionS = m_sofaContext.transform.InverseTransformDirection(direction);
                triId = m_sofaRC.castRay(originS, directionS);
                //if (triId >= 0)
                //    Debug.Log("origin: " + origin + " => originS: " + originS + " |  directionS: " + directionS + " | triId: " + triId);

                //if (m_laserType == SofaDefines.SRayInteraction.AttachTool)
                //{
                //    if (oldStiffness != m_stiffness)
                //    {
                //        oldStiffness = m_stiffness;
                //        m_sofaRC.setToolAttribute("stiffness", m_stiffness);
                //    }
                //}
            }                
        }

        // Update the laser drawing
        if (drawRay)
            this.draw(origin, origin + direction * length);
    }

    public override void updateImpl()
    {
        if (!m_isReady)
            return;
        
        Vector3 transLocal = transform.TransformVector(m_translation);
        origin = transform.position + transLocal;
        direction = transform.forward * m_axisDirection[0] + transform.right * m_axisDirection[1] + transform.up * m_axisDirection[2];

        if (m_sofaRC != null)
        {
            int triId = -1;
            // get the id of the selected triangle. If < 0, no intersection
            if (m_isActivated)
            {
                Vector3 originS = m_sofaContext.transform.InverseTransformPoint(origin);
                Vector3 directionS = m_sofaContext.transform.InverseTransformDirection(direction);
                triId = m_sofaRC.castRay(originS, directionS);
            }
        }
    }


    /// Internal method to activate or not the tool, will also update the rendering
    public void activeTool(bool value)
    {
        m_isActivated = value;

        if (m_sofaRC != null)
            m_sofaRC.activateTool(m_isActivated);

        if (value)
            this.endColor = Color.red;
        else
            this.endColor = Color.green;

        if (drawLaserParticles || drawRay)
            this.updateLaser();
    }


    private void initialiseRay()
    {
        //create linerenderer
        laser.AddComponent<LineRenderer>();
        lr = laser.GetComponent<LineRenderer>();
        if (laserMat == null)
            laserMat = Resources.Load("Materials/laser") as Material;

        lr.sharedMaterial = laserMat;
        lr.startWidth = width;
        lr.endWidth = width;

#if UNITY_5_5 || UNITY_5_4 || UNITY_5_3 || UNITY_5_2 || UNITY_5_1
            lr.numPositions = 2;
#else
        lr.positionCount = 2;
#endif
    }

    /// Internal method to create the laser line renderer and light
    private void initializeLaser()
    {
        //create light
        light = lightSource.AddComponent<Light>();
        light = lightSource.GetComponent<Light>();
        light.intensity = width * 10;
        light.bounceIntensity = width * 10;
        light.range = width / 4;
        light.color = endColor;
    }

    /// Internal method to create the laser particle system rendering
    private void initializeParticles()
    {
        psInitialized = true;
        //create particlesystem
        //TODO: add scaling/size with laser width
        if (drawLaserParticles)
        {
            ps = laser.AddComponent<ParticleSystem>();
            var shape = ps.shape;
            shape.angle = 0;
            shape.radius = 0.2f;
            var em = ps.emission;
            em.rateOverTime = 1000;
            var psmain = ps.main;
            psmain.startSize = 1.0f;
            psmain.startLifetime = length * 0.1f;
            psmain.startSpeed = 100;
            psmain.maxParticles = 800;
            psmain.startColor = new Color(1, 1, 1, 0.25f);
            //var pscolor = ps.colorOverLifetime;
            //pscolor.color = new ParticleSystem.MinMaxGradient(startColor, endColor);
        
            var psrenderer = ps.GetComponent<ParticleSystemRenderer>();
            if (particleMat == null)
                particleMat = new Material(Shader.Find("Particles/Default-Particle"));

            psrenderer.material = particleMat;
        }
    }

    /// Method to update the position of the laser to render
    public void draw(Vector3 start, Vector3 end)
    {
        if (drawRay)
        {
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
    }

    /// Method to update the laser rendering when tool status change
    public void updateLaser()
    {
        if (drawRay)
        {
            lr.startColor = startColor;
            lr.endColor = endColor;
            lr.startWidth = width;
            lr.endWidth = width;
        }

        if (drawLaserParticles)
        {
            ps = laser.GetComponent<ParticleSystem>();
            var psmain = ps.main;
            psmain.startColor = new Color(endColor.r, endColor.g, endColor.b, 0.25f); ;
        
            light.color = endColor;
            light.intensity = width * 100;
            light.bounceIntensity = width * 3;
            light.range = width / 2.5f;
        }
    }

}