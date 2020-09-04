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

namespace PCharacter
{
    [System.Serializable]
    public class PCharacterInstanceAnimation : IPCharacterAnimation
    {
        [SerializeField]
        private float _Time;
        public float Time { get { return _Time; } set { _Time = value; } }

        [Space(10f)]
        [SerializeField]
        private int _CurrentFrame;
        public int CurrentFrame { get { return _CurrentFrame; } set { _CurrentFrame = value; } }

        public int FrameCount { get { return Clip.PFrames.Length; } }

        [HideInInspector]
        public int FrameModifier = 1;

        [Space(10f)]
        [SerializeField]
        private PCharacterAnimation _Clip;
        public PCharacterAnimation Clip { get { return _Clip; } set { _Clip = value; } }
    }
}