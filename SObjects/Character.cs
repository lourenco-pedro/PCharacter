/**********************************************************************
* Project           : PCharacter
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑03‑08
*
* Purpose           : An easy way to create 2D characters
**********************************************************************/
 
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace PCharacter
{
    [CreateAssetMenu(menuName = "PCharacter/Character", fileName = "PCharacter")]
    public class Character : ScriptableObject
    {
        [Header("Simulation Configurations")]
        public PCharacterGlobalConfig Configuration;

        [Space(10f)]
        [Header("Character Configurations")]

        public PCharacterType CharacterType;

        [Space(5f)]

        public string Name;
        public PCharacterAnimation[] Animations = new PCharacterAnimation[0];

        [Space(10f)]
        public SpriteAtlas CharacterAtlas;
        [HideInInspector]
        public string[] SpritesNames = new string[0];
    }
}
