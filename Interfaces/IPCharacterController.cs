/**********************************************************************
* Project           : PCharacter
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑03‑08
*
* Purpose           : An easy way to create 2D characters
**********************************************************************/
 
 
using UnityEngine;
using System.Collections.Generic;

namespace PCharacter
{
    public interface IPCharacterController
    {
        bool isWallFacing { get; set; }
        PControllerActionType CurrentAction { get; set; }
        List<PControllerActionType> Actions { get; set; }
    }
}