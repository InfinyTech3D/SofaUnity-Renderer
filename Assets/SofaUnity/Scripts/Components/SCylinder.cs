﻿using System;
using System.Collections.Generic;
using UnityEngine;
using SofaUnity;
using SofaUnityAPI;

namespace SofaUnity
{
    [ExecuteInEditMode]
    public class SCylinder : SGrid
    {
        /// Mesh of this object
        protected override void createObject()
        {
            IntPtr _simu = m_context.getSimuContext();
            if (_simu != IntPtr.Zero)
                m_impl = new SofaCylinder(_simu, m_context.objectcpt, m_nameId, false);

            if (m_impl == null)
                Debug.LogError("SCylinder:: Object not created");
        }

        // Update is called once per frame
        protected override void updateImpl()
        {
            if (m_log)
                Debug.Log("SCylinder::updateImpl called.");

            if (m_impl != null)
            {
                m_impl.updateMesh(m_mesh);
                m_mesh.RecalculateNormals();
            }
        }

    }
}
