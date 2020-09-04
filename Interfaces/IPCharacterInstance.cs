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
    public interface IPCharacterInstance
    {
        Character Base { get; }
        Transform Transform { get; }
        string Name { get; set; }
        SpriteRenderer Render { get; }
        GameObject RenderObject { get; }
        Rigidbody2D Rig { get; set; }
        PCharacterInstanceController Controller { get; set; }
        PCharacterInstanceAnimation CurrentAnimation { get; set; }
    }
}