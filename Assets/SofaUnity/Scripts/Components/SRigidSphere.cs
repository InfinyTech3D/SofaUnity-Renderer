﻿using System;
using UnityEngine;

namespace SofaUnity
{
    /// <summary>
    /// Specific class for a Rigid Sphere Mesh, inherite from SRigidGrid 
    /// This class will create a SofaBox API object to load the topology from Sofa Sphere Grid Mesh.
    /// </summary>

    [ExecuteInEditMode]
    public class SRigidSphere : SRigidGrid
    {
        /// Method called by @sa loadContext() method. To create the object when Sofa context has been found.
        protected override void createObject()
        {
            // Get access to the sofaContext
            IntPtr _simu = m_context.getSimuContext();
            if (_simu != IntPtr.Zero) // Create the API object for Sofa Sphere Grid Mesh
                m_impl = new SofaSphere(_simu, m_nameId, true);

            if (m_impl == null)
                Debug.LogError("SRigidSphere:: Object creation failed.");
        }
    }
}