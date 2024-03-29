﻿/*****************************************************************************
 *                 - Copyright (C) - 2022 - InfinyTech3D -                   *
 *                                                                           *
 * This file is part of the SofaUnity-Renderer asset from InfinyTech3D       *
 *                                                                           *
 * GNU General Public License Usage:                                         *
 * This file may be used under the terms of the GNU General                  *
 * Public License version 3. The licenses are as published by the Free       *
 * Software Foundation and appearing in the file LICENSE.GPL3 included in    *
 * the packaging of this file. Please review the following information to    *
 * ensure the GNU General Public License requirements will be met:           *
 * https://www.gnu.org/licenses/gpl-3.0.html.                                *
 *                                                                           *
 * Commercial License Usage:                                                 *
 * Licensees holding valid commercial license from InfinyTech3D may use this *
 * file in accordance with the commercial license agreement provided with    *
 * the Software or, alternatively, in accordance with the terms contained in *
 * a written agreement between you and InfinyTech3D. For further information *
 * on the licensing terms and conditions, contact: contact@infinytech3d.com  *
 *                                                                           *
 * Authors: see Authors.txt                                                  *
 * Further information: https://infinytech3d.com                             *
 ****************************************************************************/

using UnityEditor;
using UnityEngine;
using SofaUnity;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Editor Class to define the creation and UI of SofaContext GameObject
/// </summary>
[CustomEditor(typeof(SofaContext))]
public class SofaContextEditor : Editor
{
    /// <summary>
    ///  Add SofaContext Object creation to the SofaUnity Menu
    /// </summary>
    /// <returns>Pointer to the SofaContext GameObject</returns>
    [MenuItem("SofaUnity/SofaContext")]
    [MenuItem("GameObject/Create Other/SofaContext")]  //right click menu
    public static GameObject CreateNew()
    {
        int cpt = 0;
        if (GameObject.FindObjectOfType<SofaContext>() != null)
        {
            Debug.LogWarning("The Scene already includes a SofaContext. Only one context is possible for the moment.");            
            cpt++;
            return null;
        }

        GameObject go = new GameObject("SofaContext");
        go.AddComponent<SofaContext>();

        return go;
    }

    /// <summary>
    ///  Create Sofa logo for the Editor Menu
    /// </summary>
    private static Texture2D m_SofaLogo;
    public static Texture2D SofaLogo
    {
        get
        {
            if (m_SofaLogo == null)
            {
                Object logo = Resources.Load("icons/sofa_sprite_small");
                if (logo == null)
                    Debug.LogError("logo not found");

                m_SofaLogo = (Texture2D)logo;
            }
            return m_SofaLogo;
        }
    }

    /// <summary>
    /// Method to set the UI of the SofaContext GameObject
    /// </summary>
    public override void OnInspectorGUI()
    {
        SofaContext context = (SofaContext)this.target;

        // Add Sofa Logo
        GUIStyle logoGUIStyle = new GUIStyle();
        logoGUIStyle.border = new RectOffset(0, 0, 0, 0);
        EditorGUILayout.LabelField(new GUIContent(SofaLogo), GUILayout.MinHeight(200.0f), GUILayout.ExpandWidth(true));

        // Add field for gravity
        context.Gravity = EditorGUILayout.Vector3Field("Gravity", context.Gravity);
        EditorGUILayout.Separator();

        // Add field for timestep
        context.TimeStep = EditorGUILayout.FloatField("TimeStep", context.TimeStep);
        EditorGUILayout.Separator();

#if SofaUnityEngine
        EditorGUI.BeginDisabledGroup(true);
        context.AsyncSimulation = EditorGUILayout.Toggle("Asynchronous Simulation", context.AsyncSimulation);
        EditorGUI.EndDisabledGroup();
#endif
        
        context.CatchSofaMessages = EditorGUILayout.Toggle("Activate SOFA message handler", context.CatchSofaMessages);
        context.m_log = EditorGUILayout.Toggle("Activate SofaContext Logs", context.m_log);
        EditorGUILayout.Separator();

        EditorGUILayout.Separator();        

        // Add scene file section
        SceneFileSection(context);

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();   

        if (GUI.changed)
        {
            EditorUtility.SetDirty(context);
        }
    }

    void SceneFileSection(SofaContext context)
    {
        EditorGUILayout.Separator();
        if (context.SceneFileMgr == null)
        {
            EditorGUILayout.LabelField("No scene file manager available");
            return;
        }

        // Add Button to load a filename
        if (GUILayout.Button("Load SOFA Scene (.scn) file"))
        {
            string absolutePath = EditorUtility.OpenFilePanel("Load file scene (*.scn)", "", "scn");
            context.SceneFileMgr.SceneFilename = absolutePath.Substring(Application.dataPath.Length);
            EditorGUILayout.Separator();
        }
        //else if (GUILayout.Button("Load SOFA Python Scene (.py) file"))
        //{
        //    string absolutePath = EditorUtility.OpenFilePanel("Load file scene (*.py)", "", "py");
        //    context.SceneFileMgr.PythonSceneFilename = absolutePath.Substring(Application.dataPath.Length);
        //    EditorGUILayout.Separator();
        //}

        // Label of the filename loaded
        EditorGUILayout.LabelField("Scene Filename: ", context.SceneFileMgr.SceneFilename);

        context.UnLoadScene = GUILayout.Button("Unload Scene file");
        EditorGUILayout.Separator();
    }
    
}
