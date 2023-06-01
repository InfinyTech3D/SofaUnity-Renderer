using System.Collections.Generic;
using UnityEngine;
using SofaUnity;

public class SofaSphereCollision 
{
    protected SofaCustomMeshAPI m_impl = null;

    /// Booleen to activate/unactivate the collision
    [SerializeField] protected bool m_activated = true;

    /// array of vertex corresponding to the sphere centers
    protected Vector3[] m_centers = null;

    /// Collision sphere contact stiffness
    [SerializeField] protected float m_stiffness = 1000.0f;

    [SerializeField]
    private GameObject parentT = null;

    [SerializeField]
    private bool m_startOnPlay = true;

    public SofaCustomMeshAPI Impl
    {
        get => m_impl;
        set => m_impl = value;
    }

    public GameObject ParentT
    {
        get => parentT;
        set => parentT = value;
    }

    public bool StartOnPlay
    {
        get => m_startOnPlay;
        set => m_startOnPlay = value;
    }

    public Vector3[] Centers
    {
        get => m_centers;
        set => m_centers = value;
    }

    /// Getter/Setter of the parameter @see m_activated  
    public bool Activated
    {
        get { return m_activated; }
        set { m_activated = value; }
    }

    /// Getter/Setter of the parameter @see m_stiffness     
    public float Stiffness
    {
        get { return m_stiffness; }
        set
        {
            if (value != m_stiffness)
            {
                m_stiffness = value;
                if (m_impl != null)
                    m_impl.SetFloatValue("contactStiffness", m_stiffness);
            }
            else
                m_stiffness = value;
        }
    }

    /// Get the number of spheres
    public int NbrSpheres
    {
        get
        {
            if (m_centers != null)
                return m_centers.Length;
            else
                return 0;
        }
    }


    public void UpdateLoop(Transform transform, SofaContext ctxt)
    {
        if (parentT != null)
        {
            transform.position = parentT.transform.position;
        }

        if (m_activated && m_centers != null)
        {
            m_impl.UpdateMesh(transform, m_centers, ctxt.transform);
        }
    }

    public void DrawGizmos(float radius, Transform transform, SofaContext ctxt)
    {
        if (m_centers == null || ctxt == null)
            return;

        Gizmos.color = Color.yellow;
        //float factor = m_sofaContext.GetFactorSofaToUnity();

        foreach (Vector3 vert in m_centers)
        {
            Gizmos.DrawSphere(transform.TransformPoint(vert), radius/**m_sofaContext.GetFactorSofaToUnity(1)*/);
        }
    }

}