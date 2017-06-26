﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SofaUnity;
using SofaUnityAPI;
using System;

namespace SofaUnity
{
    public class SRigidGrid : SRigidMesh
    {
        protected bool m_useTex = true;

        private void Awake()
        {
#if UNITY_EDITOR
            if (m_log)
                Debug.Log("UNITY_EDITOR - SRigidGrid::Awake");

            loadContext();

            MeshFilter mf = gameObject.GetComponent<MeshFilter>();
            if (mf == null)
                gameObject.AddComponent<MeshFilter>();

            //to see it, we have to add a renderer
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = gameObject.AddComponent<MeshRenderer>();
                mr.material = new Material(Shader.Find("Diffuse"));

                if (this.m_useTex)
                    mr.material = Resources.Load("Materials/BoxSofa") as Material;
            }

#else
            Debug.Log("UNITY_PLAY - SBox::Awake called.");
#endif
        }

        protected override void initMesh()
        {
            if (m_impl == null)
                return;

            m_mesh.name = "SofaGrid";
            m_mesh.vertices = new Vector3[0];
            m_impl.updateMesh(m_mesh);
            m_mesh.triangles = m_impl.createTriangulation();
            m_impl.updateMesh(m_mesh);
            m_impl.recomputeTriangles(m_mesh);
            m_impl.recomputeTexCoords(m_mesh);

            m_impl.setTranslation(m_translation);
            m_impl.setRotation(m_rotation);
            m_impl.setScale(m_scale);
            m_impl.updateMesh(m_mesh);
            m_impl.setGridResolution(m_gridSize);
        }

        public Vector3 m_gridSize = new Vector3(5, 5, 5);
        public virtual Vector3 gridSize
        {
            get { return m_gridSize; }
            set
            {
                if (value != m_gridSize)
                {
                    m_gridSize = value;
                    if (m_impl != null)
                        m_impl.setGridResolution(m_gridSize);
                }
            }
        }
    }
}