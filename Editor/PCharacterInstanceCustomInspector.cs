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

namespace PCharacter
{
    [CustomEditor(typeof(PCharacterInstance))]
    public class PCharacterInstanceCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var instance = (PCharacterInstance)target;

            if (instance.Base == null)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Base ");
                    instance.Base = (Character)EditorGUILayout.ObjectField(instance.Base, typeof(Character), true);
                }
                EditorGUILayout.EndHorizontal();
                return;
            }

            if (instance.Base.Configuration == null)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Base: ");
                    instance.Base = (Character)EditorGUILayout.ObjectField(instance.Base, typeof(Character), true);
                }
                EditorGUILayout.EndHorizontal();

                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleCenter;

                GUILayout.Label("<color=red>Global Config of this base is null</color>", style);
            }
            else
                base.OnInspectorGUI();
        }
    }
}
#endif