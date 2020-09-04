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
using System;

namespace PCharacter
{
    [Serializable]
    public class PCharacterAnimation
    {
        public string Name;
        public PAnimationType AnimationType;
        public PFrame[] PFrames;

#if UNITY_EDITOR
        [HideInInspector] public int LastFrameCount;
        public bool ShowPFrames { get; set; }
#endif
    }
}