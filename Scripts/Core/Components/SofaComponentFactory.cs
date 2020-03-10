﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SofaUnityAPI;

namespace SofaUnity
{
    /// manage specific component class implementation and creation 
    /// TODO: how to create a real factory in C# ??
    static public class SofaComponentFactory
    {
        static public SofaBaseComponent CreateSofaComponent(string nameId, string componentType, SofaDAGNode sofaNodeOwner, GameObject parent)
        {
            GameObject compoGO = new GameObject("SofaComponent - " + nameId);
            SofaBaseComponent sofaCompo = null;
            if (componentType == "SofaSolver")
            {
                sofaCompo = compoGO.AddComponent<SofaSolver>();
            }
            else if (componentType == "SofaLoader")
            {
                sofaCompo = compoGO.AddComponent<SofaLoader>();
            }
            else if (componentType == "SofaMesh")
            {
                sofaCompo = compoGO.AddComponent<SofaMesh>();
            }
            else if (componentType == "SofaMass")
            {
                sofaCompo = compoGO.AddComponent<SofaMass>();
            }
            else if (componentType == "SofaFEMForceField")
            {
                sofaCompo = compoGO.AddComponent<SofaFEMForceField>();
            }
            else if (componentType == "SofaMechanicalMapping")
            {
                sofaCompo = compoGO.AddComponent<SofaMechanicalMapping>();
            }
            else if (componentType == "SofaCollisionModel")
            {
                sofaCompo = compoGO.AddComponent<SofaCollisionModel>();
            }
            else if (componentType == "SofaConstraint")
            {
                sofaCompo = compoGO.AddComponent<SofaConstraint>();
            }
            else if (componentType == "SofaVisualModel")
            {
                sofaCompo = compoGO.AddComponent<SofaVisualModel>();
            }
            else
            {
                Debug.LogError("Component type not handled: " + componentType);
                return null;
            }

            // set generic parameters
            sofaCompo.SetDAGNode(sofaNodeOwner);
            sofaCompo.Create(sofaNodeOwner.m_sofaContext, nameId);
            sofaCompo.m_baseComponentType = sofaCompo.BaseTypeFromString(componentType);
            compoGO.transform.parent = parent.gameObject.transform;

            return sofaCompo;
        }
    }

}
