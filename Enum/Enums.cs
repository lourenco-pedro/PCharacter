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

namespace PCharacter
{

    public enum PCharacterType { CHARACTER, SOLID }
    public enum PAnimationType { NORMAL, PING_PONG };
    public enum PControllerActionType { NONE, TRANSLATE_LEFT, TRANSLATE_RIGHT, JUMP }

#if UNITY_EDITOR

    public enum GUIStyleType { NONE, NORMAL_HEADER, MEDIUM_ITALIC_LABEL }
#endif
}