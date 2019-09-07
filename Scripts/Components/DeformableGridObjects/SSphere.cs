﻿using System;
using UnityEngine;

namespace SofaUnity
{
    /// <summary>
    /// Specific class for a Deformable Sphere Mesh, inherite from SGrid 
    /// This class will create a SofaBox API object to load the topology from Sofa Sphere Grid Mesh.
    /// </summary>
    [ExecuteInEditMode]
    public class SSphere : SGrid
    {
        /// Method called by @sa loadContext() method. To create the object when Sofa context has been found.
        protected override void createObject()
        {
            /// Method called by @sa loadContext() method. To create the object when Sofa context has been found.
            IntPtr _simu = m_context.getSimuContext();
            if (_simu != IntPtr.Zero) // Create the API object for Sofa Sphere Grid Mesh
                m_impl = new SofaSphere(_simu, m_nameId, false);

            if (m_impl == null || !m_impl.m_isCreated)
            {
                Debug.LogError("SSphere:: Object creation failed: " + m_nameId);
                this.enabled = false;
            }
        }

        // Update is called once per frame
        public override void updateImpl()
        {
            if (m_log)
                Debug.Log("SSphere::updateImpl called.");

            if (m_impl != null)
            {
                // TODO: need to find why velocity doesn't work for grid
                //m_impl.updateMeshVelocity(m_mesh, m_context.timeStep);
                m_impl.updateMesh(m_mesh);
                m_mesh.RecalculateBounds();
                m_mesh.RecalculateNormals(); // TODO check if needed
            }
        }
    }
}
