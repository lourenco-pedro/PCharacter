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
using System.Collections.Generic;
using PCharacter.Exception;

namespace PCharacter
{
    public static class PCharacterInstanceUtil
    {
        ///<summary>
        ///Creates a PCharacterInstance with a given base Character base.
        ///</summary>
        public static PCharacterInstance CreateInstanceFromBase(Character characterBase)
        {
            PCharacterInstance instance = new GameObject("Instance_" + characterBase.Name).AddComponent<PCharacterInstance>();
            instance.CurrentAnimation = new PCharacterInstanceAnimation();
            instance.Base = characterBase;

            InitializePCharacter(instance);

            return instance;
        }

        ///<summary>
        ///Define intial values for a given PCharacterInstance, such as name, sprite, collision, physics, etc...
        ///</summary>
        public static void InitializePCharacter(PCharacterInstance instance)
        {
            instance.Name = "Instance_" + instance.Base.Name;

            instance.CurrentAnimation = new PCharacterInstanceAnimation();
            instance.CurrentAnimation.Clip = new PCharacterAnimation();
            instance.Controller = new PCharacterInstanceController();
            instance.Controller.Actions = new List<PControllerActionType>();

            instance.RenderObject = new GameObject(instance.Base.Name);
            instance.Render = instance.RenderObject.AddComponent<SpriteRenderer>();

            instance.RenderObject.transform.parent = instance.transform;
            instance.RenderObject.transform.localPosition = Vector2.zero;

            Refresh(instance);
        }

        ///<summary>
        ///Update PCharacterInstance physics and collision values
        ///</summary>
        public static void Refresh(PCharacterInstance instance)
        {
            if (instance.Base.CharacterType == PCharacterType.CHARACTER)
            {
                instance.Rig = instance.gameObject.GetComponent<Rigidbody2D>();
                if (instance.Rig == null)
                    instance.Rig = instance.gameObject.AddComponent<Rigidbody2D>();

                instance.Rig.gravityScale = instance.Base.Configuration.GravityScale;
                instance.Rig.constraints = instance.Base.Configuration.Constraints2D;
            }

            if (Application.isPlaying && instance.Base.Animations.Length > 0 && instance.Base.Animations[0].PFrames.Length > 0)
                instance.Render.sprite = instance.Base.CharacterAtlas.GetSprite(instance.Base.Animations[0].PFrames[0].FrameName);

            RecalculateCollider((PCharacterInstance)instance);
        }

        ///<summary>
        ///Set collider2D values to boudaires of the current rendered sprite of the PCharacterInstance
        ///</summary>
        public static void RecalculateCollider(PCharacterInstance instance)
        {
            BoxCollider2D collider = instance.RenderObject.GetComponent<BoxCollider2D>();
            if (collider == null)
                collider = instance.RenderObject.AddComponent<BoxCollider2D>();

            if (instance.Render.sprite != null)
            {
                Sprite _sprite = instance.Render.sprite;

                Vector2 spriteBounds = instance.Render.sprite.bounds.size;
                collider.size = spriteBounds;
                collider.offset = new Vector2(0, collider.size.y / 2);
                return;
            }

            collider.offset = Vector2.zero;
            collider.size = Vector2.one;
        }

        ///<summary>
        ///Is PCharacterInstance collinding whit an object of a given LayerMask
        ///</summary>
        public static bool IsCharacterGrounded(Vector2 detectorOrigin, float radius, LayerMask groundLayer)
        {
            return Physics2D.OverlapCircle(detectorOrigin, radius, groundLayer);
        }

        public static void AddInstanceControllerAction(IPCharacterInstance instance, PControllerActionType actionType)
        {
            if (actionType == PControllerActionType.JUMP)
                instance.Controller.Actions.Clear();

            instance.Controller.Actions.Add(actionType);
        }

        public static PControllerActionType UpdateInstanceController(IPCharacterInstance instance, float moveSpeed, float jumpForce, bool faceToWalkDirection = true)
        {
            if (instance.Controller.Actions.Count > 0)
            {
                var curAction = instance.Controller.Actions[0];
                Transform instanceTransform = instance.Transform;

                switch (curAction)
                {
                    case PControllerActionType.TRANSLATE_RIGHT:
                        if (!instance.Controller.isWallFacing)
                            instanceTransform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                        break;
                    case PControllerActionType.TRANSLATE_LEFT:
                        if (!instance.Controller.isWallFacing)
                            instanceTransform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                        break;
                    case PControllerActionType.JUMP:
                        instance.Rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        break;
                }

                instance.Controller.CurrentAction = curAction;
                instance.Controller.Actions.RemoveAt(0);

                if (faceToWalkDirection)
                {
                    FaceToMoveDirection(instance);
                }

                return curAction;
            }

            instance.Controller.CurrentAction = PControllerActionType.NONE;
            return PControllerActionType.NONE;
        }

        ///<summary>
        ///Flip PCharacterInstance render sprite in X using TRANSLATE_LEFT or TRANSLATE_RIGHT
        ///<summary>
        public static void FaceToMoveDirection(IPCharacterInstance instace)
        {
            var curAction = instace.Controller.CurrentAction;
            switch (curAction)
            {
                case PControllerActionType.TRANSLATE_LEFT:
                    instace.Render.flipX = true;
                    break;
                case PControllerActionType.TRANSLATE_RIGHT:
                    instace.Render.flipX = false;
                    break;
            }
        }

        ///<summary>
        ///Will prevent the character from keep walking towards the wall
        ///</summary>
        public static void BreakControllerWhenFacingWall(IPCharacterInstance instance, LayerMask targetLayer, float detectorDistance = 1f)
        {
            Vector2 detectorPos = instance.RenderObject.transform.position;
            detectorPos.y += instance.RenderObject.transform.localScale.x / 2;
            float direction = (instance.Render.flipX) ? -1 : 1;

            Debug.DrawRay(detectorPos, Vector2.right * detectorDistance * direction, Color.red);
            instance.Controller.isWallFacing = Physics2D.Raycast(detectorPos, Vector2.right * direction, detectorDistance, targetLayer);
        }

        ///<summary>
        ///Set to an animation to a given PCharacterInstance
        ///</summary>
        public static void SetInstanceAnimation(IPCharacterInstance instance, string animation)
        {

            if (instance.CurrentAnimation.Clip.Name == animation)
                return;

            for (int i = 0; i < instance.Base.Animations.Length; i++)
            {
                var curBaseAnimation = instance.Base.Animations[i];

                if (curBaseAnimation.Name == animation)
                {
                    instance.CurrentAnimation.Clip = curBaseAnimation;
                    instance.CurrentAnimation.Time = 0;
                    instance.CurrentAnimation.CurrentFrame = 0;
                }
            }
        }

        public static void UpdateInstanceAnimation(IPCharacterInstance instance)
        {
            if (instance.CurrentAnimation.Clip.PFrames.Length == 0)
            {
                PCharacterInstanceEcxepection.PlayingAnimationWithoutFramesException((PCharacterInstance)instance);
                return;
            }

            if (instance.CurrentAnimation.Clip.PFrames.Length == 0)
            {
                string firstFrameName = instance.CurrentAnimation.Clip.PFrames[0].FrameName;
                instance.Render.sprite = instance.Base.CharacterAtlas.GetSprite(firstFrameName);
                return;
            }

            instance.CurrentAnimation.Time += Time.deltaTime * instance.Base.Configuration.FrameSpeedMultiplier;

            if (instance.CurrentAnimation.Time >= instance.Base.Configuration.DelayToNextFrame)
            {
                instance.CurrentAnimation.Time = 0;
                instance.CurrentAnimation.CurrentFrame += instance.CurrentAnimation.FrameModifier;

                if (instance.CurrentAnimation.Clip.AnimationType == PAnimationType.NORMAL)
                    instance.CurrentAnimation.FrameModifier = 1;
                else
                {
                    if (instance.CurrentAnimation.CurrentFrame < 0 || instance.CurrentAnimation.CurrentFrame >= instance.CurrentAnimation.Clip.PFrames.Length)
                    {
                        instance.CurrentAnimation.FrameModifier *= -1;
                        instance.CurrentAnimation.CurrentFrame += instance.CurrentAnimation.FrameModifier;
                    }
                }

                if (instance.CurrentAnimation.CurrentFrame >= instance.CurrentAnimation.Clip.PFrames.Length)
                    instance.CurrentAnimation.CurrentFrame = 0;
                else if (instance.CurrentAnimation.CurrentFrame < 0)
                    instance.CurrentAnimation.CurrentFrame = instance.CurrentAnimation.Clip.PFrames.Length - 1;
            }

            int currentFrame = instance.CurrentAnimation.CurrentFrame;
            string frameName = instance.CurrentAnimation.Clip.PFrames[currentFrame].FrameName;
            instance.Render.sprite = instance.Base.CharacterAtlas.GetSprite(frameName);
        }
    }
}