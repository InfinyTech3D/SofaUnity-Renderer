﻿using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class SofaSphere : SofaMeshObject
{
    public SofaSphere(IntPtr simu, int idObject, bool isRigid)
        : base(simu, idObject, isRigid)
    {

    }

    ~SofaSphere()
    {
        Dispose(false);
    }

    public override int[] createTriangulation()
    {
        return base.createTriangulation();
    }

    public override void recomputeTriangles(Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] verts = mesh.vertices;
        Vector3[] norms = mesh.normals;

        int cpt = 0;
        while (cpt< triangles.Length)
        {
            Vector3 AB = verts[triangles[cpt + 1]] - verts[triangles[cpt]];
            Vector3 AC = verts[triangles[cpt + 2]] - verts[triangles[cpt]];

            Vector3 AD = Vector3.Cross(AB, AC);
            Vector3 norm = norms[triangles[cpt]];
            float dot = Vector3.Dot(AD, norm);

            if (dot <0) // need to inverse triangle
            {
                int tmp = triangles[cpt + 1];
                triangles[cpt + 1] = triangles[cpt + 2];
                triangles[cpt + 2] = tmp;
            }

            cpt = cpt + 3;
        }

        mesh.triangles = triangles;
    }

    public override void recomputeTexCoords(Mesh mesh)
    {
        Vector3[] verts = mesh.vertices;
        Vector2[] uvs = new Vector2[verts.Length];

        for (int i = 0; i < verts.Length; i++)
            uvs[i] = new Vector2(verts[i].x + verts[i].y, verts[i].z + verts[i].y);

        mesh.uv = uvs;
    }

    protected override void createObject()
    {
        m_name = "sphere_" + m_idObject + "_node";

        if (m_native == IntPtr.Zero) // first time create object only
        {
            // Create the sphere
            int res = sofaPhysicsAPI_addSphere(m_simu, "sphere_" + m_idObject, m_isRigid);
            if (res == 1) // sphere added
            {
                Debug.Log("sphere Added! " + m_name);

                // Set created object to native pointer
                m_native = sofaPhysicsAPI_get3DObject(m_simu, m_name);
            }

            //    m_native = sofaPhysicsAPI_get3DObject(m_simu, "truc1");

            if (m_native == IntPtr.Zero)
                Debug.LogError("Error sphere created can't be found!");
        }
    }

    [DllImport("SofaAdvancePhysicsAPI", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern int sofaPhysicsAPI_addSphere(IntPtr obj, string name, bool isRigid);

}