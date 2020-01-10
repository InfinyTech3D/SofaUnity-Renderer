﻿using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using SofaUnity;
using SofaScripts;


public class Benchmark01Editor : Editor
{
    [MenuItem("SofaUnity/Benchmarks/Benchmark01")]
    public static GameObject CreateNew()
    {
        if (!SofaScripts.Benchmarks.hasSofaContext())
            return null;

        // Add sofa context first
        GameObject context = SofaScripts.Benchmarks.createSofaContext();

        // Add a floor        
        SofaScripts.Benchmarks.createFloor();

        // Add the cubes
        for (int i=0; i<2; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                GameObject cube = SofaScripts.Benchmarks.createCube("SofaBox_" + i + "_" + j);
                SofaBox box = cube.GetComponent<SofaBox>();
                box.m_translation[0] = -2 + j * 2;
                box.m_translation[1] = 6+i*2;
            }
        }

        // Add camera
        SofaScripts.Benchmarks.addThirdPartyCamera();

        return context;
    }
}


public class Benchmark02Editor : Editor
{
    [MenuItem("SofaUnity/Benchmarks/Benchmark02")]
    public static GameObject CreateNew()
    {        
        if (!SofaScripts.Benchmarks.hasSofaContext())
            return null;

        // Add sofa context first
        GameObject context = SofaScripts.Benchmarks.createSofaContext();
        SofaContext _sofaContext = context.GetComponent<SofaContext>();
        _sofaContext.TimeStep = 0.01f;

        // Add a floor        
        GameObject floor = SofaScripts.Benchmarks.createFloor();
        SofaRigidPlane _sofaPlane = floor.GetComponent<SofaRigidPlane>();

        // Add Spheres
        for (int i = 0; i < 5; ++i)
        {
            GameObject obj = SofaScripts.Benchmarks.createSphere("SofaSphere_" + i);
            SofaSphere objImpl = obj.GetComponent<SofaSphere>();
            objImpl.m_translation[0] = -3;
            objImpl.m_translation[1] = 6 + i * 2;
        }

        // Add Cubes
        for (int i = 0; i < 5; ++i)
        {
            GameObject obj = SofaScripts.Benchmarks.createCube("SofaBox_" + i);
            SofaBox objImpl = obj.GetComponent<SofaBox>();
            objImpl.m_translation[0] = 0;
            objImpl.m_translation[1] = 6 + i * 2;
        }

        // Add Cylinders
        for (int i = 0; i < 5; ++i)
        {
            GameObject obj = SofaScripts.Benchmarks.createCylinder("SofaCylinder_" + i);
            SofaCylinder objImpl = obj.GetComponent<SofaCylinder>();
            objImpl.m_translation[0] = 3;
            objImpl.m_translation[1] = 5.5f + i * 2;
        }

        // Add camera
        SofaScripts.Benchmarks.addThirdPartyCamera();

        return context;
    }
}
