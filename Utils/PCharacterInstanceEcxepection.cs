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

namespace PCharacter.Exception
{
    public static class PCharacterInstanceEcxepection
    {
        public static void BaseIsNullExcpetion(PCharacterInstance instance)
        {
            Debug.LogErrorFormat("Base of instance {0} is null", instance.gameObject.name);
        }

        public static void FetchRigFromSolidCharacterException(PCharacterInstance instance)
        {
            Debug.LogErrorFormat("Trying to fetch RigidBody2D Component from a solid Character instance: {0}", instance.gameObject.name);
        }

        public static void RigidBody2DIsNullException(PCharacterInstance instance)
        {
            Debug.LogErrorFormat("Trying to fetch a null RigidBody2D: {0}", instance.gameObject.name);
        }

        public static void PlayingAnimationWithoutFramesException(PCharacterInstance instance)
        {
            Debug.LogErrorFormat("Trying to play animation without Frames: {0}", instance.gameObject.name);
        }
    }
}