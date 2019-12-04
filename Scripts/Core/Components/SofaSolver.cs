﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SofaUnity
{
    public class SofaSolver : SofaBaseComponent
    {
        override public void Init()
        {
            SofaLog("Init SofaSolver");
        }


        /// Method called by @sa Update() method.
        override public void UpdateImpl()
        {
            SofaLog("UpdateImpl SofaSolver");
        }
    }

} // namespace SofaUnity
