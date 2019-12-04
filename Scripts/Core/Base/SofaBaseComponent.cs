﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SofaUnity
{
    // TODO find a way to interactively add more type if plugin are loaded
    public enum SBaseComponentType
    {
        SofaSolver,
        SofaLoader,
        SofaMesh,
        SofaMass,
        SofaFEMForceField,
        SofaMechanicalMapping,
        SofaCollisionModel,
        SofaVisualModel,
        SofaUnknown
    };
    

    public class SofaBaseComponent : SofaBase
    {
        // do generic stuff for baseComponent here
        protected SofaDAGNode m_ownerNode = null;
        
        public SBaseComponentType m_baseComponentType;
        public string m_componentType = "Not set";


        public string BaseTypeToString(SBaseComponentType type)
        {
            return type.ToString();
        }

        public SBaseComponentType BaseTypeFromString(string typeS)
        {
            SBaseComponentType enumRes = SBaseComponentType.SofaUnknown;
            var enumValues = Enum.GetValues(typeof(SBaseComponentType));
            foreach (SBaseComponentType enumVal in enumValues)
                if (enumVal.ToString() == typeS)
                    return enumVal;

            return enumRes;
        }

        public void setDAGNode(SofaDAGNode _node)
        {
            m_ownerNode = _node;
            m_sofaContext = m_ownerNode.m_sofaContext;
        }

        void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateInEditor();
                return;
            }


            // Call internal method that can be overwritten. Only if dirty
            if (m_isDirty)
            {
                UpdateImpl();
                m_isDirty = false;
            }
        }


        /// Method called by @sa Update() method. To be implemented by child class.
        public virtual void UpdateImpl()
        {

        }


        /// Method called by @sa Update() method. When Unity is not playing.
        public virtual void UpdateInEditor()
        {

        }
    }

} // namespace SofaUnity