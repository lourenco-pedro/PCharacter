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
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PCharacter
{
    [System.Serializable]
    public class PFrame
    {
        public string FrameName;
#if UNITY_EDITOR
        public int SelectedIndex;
#endif
    }
}