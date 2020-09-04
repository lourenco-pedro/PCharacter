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
using UnityEditor;
using System;

public static class GUIEditorUtil
{
    public static bool UpdateAndDrawEditorArrayCount<T>(ref T[] targetArray, ref int referenceCount)
    {
        referenceCount = EditorGUILayout.IntField("Size: ", referenceCount);
        int length = referenceCount;

        if (length > targetArray.Length)
        {
            T[] arrayClone = new T[length];

            for (int i = 0; i < arrayClone.Length; i++)
            {
                if (i < targetArray.Length)
                {
                    arrayClone[i] = targetArray[i];
                }
            }

            targetArray = new T[arrayClone.Length];
            Array.Copy(arrayClone, targetArray, arrayClone.Length);
            return true;
        }
        else if (length < targetArray.Length)
        {
            int diff = targetArray.Length - length;
            Array.Resize(ref targetArray, targetArray.Length - diff);
            return true;
        }

        return false;
    }

    public static void DrawnHorizontalLine(float bottomSpacing = 0)
    {
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1f), new Color(0f, 0f, 0f, 0.3f));
        GUILayout.Space(bottomSpacing);
    }
}
#endif