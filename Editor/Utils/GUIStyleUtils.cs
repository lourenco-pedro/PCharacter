/**********************************************************************
* Project           : PCharacter
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑03‑08
*
* Purpose           : An easy way to create 2D characters
**********************************************************************/
 
 
#if UNITY_EDITOR
using UnityEngine;

namespace PCharacter
{
    public static class GUIStyleUtils
    {
        public static GUIStyle GetStyle(GUIStyleType type)
        {
            GUIStyle target = new GUIStyle();

            switch (type)
            {
                case GUIStyleType.NORMAL_HEADER:
                    target.fontStyle = FontStyle.Bold;
                    target.fontSize = 15;
                    target.alignment = TextAnchor.MiddleCenter;
                    break;
                case GUIStyleType.MEDIUM_ITALIC_LABEL:
                    target.fontStyle = FontStyle.Italic;
                    break;
            }

            return target;

        }
    }
}
#endif