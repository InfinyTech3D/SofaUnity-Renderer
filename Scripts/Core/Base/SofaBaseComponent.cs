﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
        SofaConstraint,
        SofaVisualModel,
        SofaUnknown
    };  

    public class SofaBaseComponent : SofaBase
    {
        // do generic stuff for baseComponent here
        public SofaDAGNode m_ownerNode = null;
        
        public SBaseComponentType m_baseComponentType;
        protected List<string> m_possibleComponentTypes;
        public string m_componentType;

        /// Pointer to the Sofa Context API.
        public SofaBaseComponentAPI m_impl = null;


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


        ////////////////////////////////////////////
        /////          Component API           /////
        ////////////////////////////////////////////

        protected override void InitImpl()
        {
            if (m_impl == null)
            {
                // Creation method of Sofa component API
                CreateSofaAPI();
                
                // overide method to fill specific possible types
                FillPossibleTypes();

                // overide name with current type
                SetComponentType();

                // Generic Data section
                GetAllData();

                // Generic Link section
                GetAllLinks();

                // overide method to fill specific data section
                FillDataStructure();
            }
            else
                SofaLog("SofaBaseComponent::InitImpl, already created: " + UniqueNameId, 1);
        }


        protected virtual void CreateSofaAPI()
        {
            if (m_impl != null)
            {
                Debug.LogError("SofaBaseComponent " + UniqueNameId + " already has a SofaBaseComponentAPI.");
                return;
            }

            if (m_sofaContext == null)
            {
                SofaLog("CreateSofaAPI: " + UniqueNameId + " m_sofaContext is null", 1);
                return;
            }

            if (m_sofaContext.GetSimuContext() == null)
            {
                SofaLog("CreateSofaAPI: " + UniqueNameId + " m_sofaContext.GetSimuContext() is null", 1);
                return;
            }

            CreateSofaAPI_Impl();
        }

        protected virtual void CreateSofaAPI_Impl()
        {
            SofaLog("SofaBaseComponent::CreateSofaAPI_Impl: " + UniqueNameId + " | m_sofaContext: " + m_sofaContext + " | m_sofaContext.GetSimuContext(): " + m_sofaContext.GetSimuContext());
            m_impl = new SofaBaseComponentAPI(m_sofaContext.GetSimuContext(), UniqueNameId);
        }


        protected virtual void SetComponentType()
        {
            // overide name with current type
            m_componentType = m_impl.GetComponentType();
            this.gameObject.name = m_componentType + "  -  " + m_uniqueNameId;
        }


        protected virtual void FillPossibleTypes()
        {
            
        }


        protected override void ReconnectImpl()
        {
            // 1- reconnect with SofaBaseComponentAPI
            CreateSofaAPI();

            // 2- reconnect and update edited data
            if (m_dataArchiver == null)
            {
                SofaLog("SofaBaseComponent::ReconnectImpl has a null DataArchiver.", 2);
                return;
            }

            bool modified = m_dataArchiver.UpdateEditedData();
            if (modified)
            {
                SofaLog("SofaBaseComponent::ReconnectImpl some Data modified will reinit component.");
                // call reinit here?
            }
        }


        protected virtual void FillDataStructure()
        {

        }


        ////////////////////////////////////////////
        /////        Internal Sata API         /////
        ////////////////////////////////////////////

        [SerializeField]
        public SofaDataArchiver m_dataArchiver = null;

        [SerializeField]
        public SofaLinkArchiver m_linkArchiver = null;


        virtual protected void GetAllData()
        {
            if (m_impl != null)
            {
                string allData = m_impl.LoadAllData();
                if (allData == "None")
                    return;

                if (m_dataArchiver == null)
                    m_dataArchiver = new SofaDataArchiver();

                List<String> datas = allData.Split(';').ToList();
                foreach (String data in datas)
                {
                    String[] values = data.Split(',');
                    if (values.GetLength(0) == 2)
                    {
                        m_dataArchiver.AddData(this, values[0], values[1]);
                    }
                }
            }
            else
            {
                SofaLog("GetAllData: m_impl is null.", 1);
            }
        }


        virtual protected void GetAllLinks()
        {
            if (m_impl != null)
            {
                string allLinks = m_impl.LoadAllLinks();
                if (allLinks == "None" || allLinks.Length == 0)
                    return;

                List<String> links = allLinks.Split(';').ToList();
                if (m_linkArchiver == null)
                    m_linkArchiver = new SofaLinkArchiver();

                foreach (String link in links)
                {
                    String[] values = link.Split(',');
                    
                    if (values.GetLength(0) == 3)
                    {
                        m_linkArchiver.AddLink(this, values[0], values[2]);
                    }
                }
            }
            else
            {
                SofaLog("GetAllLinks: m_impl is null.", 1);
            }
        }
    }

} // namespace SofaUnity
