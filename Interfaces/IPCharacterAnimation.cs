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
    public interface IPCharacterAnimation
    {
        float Time { get; set; }
        int CurrentFrame { get; set; }
        int FrameCount { get; }
        PCharacterAnimation Clip { get; set; }
    }
}