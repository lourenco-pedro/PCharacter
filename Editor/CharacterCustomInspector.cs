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
using UnityEngine.U2D;

namespace PCharacter
{
    [CustomEditor(typeof(Character))]
    public class CharacterCustomInspector : Editor
    {

        private void DrawnAnimationContent(Character character, PCharacterAnimation PAnimation)
        {
            var animations = character.Animations;
            var atlas = character.CharacterAtlas;
            var frames = character.SpritesNames;

            if (atlas == null)
            {
                System.Array.Resize(ref PAnimation.PFrames, 0);
                PAnimation.LastFrameCount = 0;
            }


            GUILayout.Space(10f);

            GUIEditorUtil.DrawnHorizontalLine(2);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("X"))
                {
                    int index = ArrayUtility.IndexOf(animations, PAnimation);
                    ArrayUtility.RemoveAt<PCharacterAnimation>(ref character.Animations, index);
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            var rect = EditorGUILayout.BeginVertical();
            {
                //Display Animation Name field
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Name: ");
                    PAnimation.Name = GUILayout.TextField(PAnimation.Name);
                }
                EditorGUILayout.EndHorizontal();

                //Display AnimationType field
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Animation Type: ");
                    PAnimation.AnimationType = (PAnimationType)EditorGUILayout.EnumPopup(PAnimation.AnimationType);
                }
                EditorGUILayout.EndHorizontal();


                //Display PFrame field
                EditorGUILayout.LabelField("<color=orange>It recomented to add a decent name for each frame on atlas</color>", GUIStyleUtils.GetStyle(GUIStyleType.MEDIUM_ITALIC_LABEL));
                PAnimation.ShowPFrames = EditorGUILayout.Foldout(PAnimation.ShowPFrames, "Frames");
                if (PAnimation.ShowPFrames)
                {
                    bool changed = GUIEditorUtil.UpdateAndDrawEditorArrayCount(ref PAnimation.PFrames, ref PAnimation.LastFrameCount);


                    for (int i = 0; i < PAnimation.PFrames.Length; i++)
                    {
                        var curFrame = PAnimation.PFrames[i];
                        DrawnPFrameContent(curFrame, frames);
                    }

                }
            }
            EditorGUILayout.EndVertical();

            GUILayout.Space(10f);
        }

        private void DrawnPFrameContent(PFrame pFrame, string[] frames)
        {
            if (frames == null || pFrame == null)
                return;

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Frame Name: ");
                pFrame.SelectedIndex = EditorGUILayout.Popup(pFrame.SelectedIndex, frames);
            }
            EditorGUILayout.EndHorizontal();
            pFrame.FrameName = frames[pFrame.SelectedIndex];
        }

        public override void OnInspectorGUI()
        {
            var character = (Character)target;
            SerializedObject characterSerialized = new SerializedObject(character);

            GUILayout.Space(10f);
            GUILayout.Label("Main", GUIStyleUtils.GetStyle(GUIStyleType.NORMAL_HEADER));
            GUILayout.Space(10f);


            character.CharacterType = (PCharacterType)EditorGUILayout.EnumPopup(character.CharacterType);
            character.Name = EditorGUILayout.TextField("Name: ", character.Name);
            GUILayout.Space(5f);
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Global Config: ");
                character.Configuration = (PCharacterGlobalConfig)EditorGUILayout.ObjectField(character.Configuration, typeof(PCharacterGlobalConfig), false);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10f);
            GUILayout.Label("Character Configuration", GUIStyleUtils.GetStyle(GUIStyleType.NORMAL_HEADER));
            GUILayout.Space(10f);

            var smallHeader = GUIStyleUtils.GetStyle(GUIStyleType.NORMAL_HEADER);
            smallHeader.fontSize = 12;
            smallHeader.fontStyle = FontStyle.Normal;
            GUILayout.Label("Character atlas", smallHeader);
            character.CharacterAtlas = (UnityEngine.U2D.SpriteAtlas)EditorGUILayout.ObjectField(character.CharacterAtlas, typeof(UnityEngine.U2D.SpriteAtlas), false);

            if (GUILayout.Button("Update Atlas"))
            {
                if (character.CharacterAtlas == null)
                {
                    Debug.LogError("Atlas is Null !");
                    return;
                }

                Sprite[] sprites = new Sprite[character.CharacterAtlas.spriteCount];
                character.CharacterAtlas.GetSprites(sprites);

                character.SpritesNames = new string[sprites.Length];

                for (int i = 0; i < sprites.Length; i++)
                {
                    var curSprite = sprites[i];
                    string formatedName = curSprite.name.Replace("(Clone)", string.Empty);
                    character.SpritesNames[i] = formatedName;
                }
            }

            GUILayout.Space(10f);

            var animationsArrayContent = characterSerialized.FindProperty("Animations");
            characterSerialized.Update();
            // bool showAnimations = EditorGUILayout.PropertyField(animationsArrayContent);

            // if (showAnimations)
            {

                EditorGUILayout.BeginHorizontal();
                {
                    int addAnimationButtonWidht = 50;
                    GUILayout.Space(Screen.width / 2 - addAnimationButtonWidht / 2);
                    if (GUILayout.Button("+", GUILayout.Width(addAnimationButtonWidht), GUILayout.Height(50)))
                    {
                        PCharacterAnimation newAnimation = new PCharacterAnimation();
                        newAnimation.PFrames = new PFrame[0];
                        ArrayUtility.Add(ref character.Animations, newAnimation);
                    }
                }
                // GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel++;
                for (int i = 0; i < character.Animations.Length; i++)
                {
                    var curAnimation = character.Animations[i];
                    DrawnAnimationContent(character, curAnimation);
                }
                EditorGUI.indentLevel--;
            }

            EditorUtility.SetDirty(target);
        }
    }
}
#endif