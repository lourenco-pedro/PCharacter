/**********************************************************************
* Project           : PCharacter
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑03‑08
*
* Purpose           : An easy way to create 2D characters
**********************************************************************/
 
 
using System;
using UnityEngine;
using System.Collections.Generic;

namespace PCharacter
{
    [System.Serializable]
    public class PCharacterInstanceController : IPCharacterController
    {
        [SerializeField]
        private bool _IsWallFacing;
        public bool isWallFacing { get { return _IsWallFacing; } set { _IsWallFacing = value; } }

        [SerializeField]
        private PControllerActionType _CurrentAction;
        public PControllerActionType CurrentAction { get { return _CurrentAction; } set { _CurrentAction = value; } }

        [Space(10f)]

        [SerializeField]
        private List<PControllerActionType> _Actions;
        public List<PControllerActionType> Actions { get { return _Actions; } set { _Actions = value; } }
    }
}