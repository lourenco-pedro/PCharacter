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
using PCharacter.Exception;

namespace PCharacter
{
    public class PCharacterInstance : MonoBehaviour, IPCharacterInstance
    {
        [SerializeField]
        private Character _Base;
        public Character Base { get { return _Base; } set { _Base = value; } }

        public Transform Transform { get { return transform; } }

        [Space(10f)]

        [SerializeField]
        private string InstanceName;
        public string Name { get { return InstanceName; } set { InstanceName = value; } }

        [Space(10f)]

        [SerializeField]
        private GameObject _RenderObject;
        public GameObject RenderObject { get { return _RenderObject; } set { _RenderObject = value; } }

        [SerializeField]
        private SpriteRenderer _Render;
        public SpriteRenderer Render { get { return _Render; } set { _Render = value; } }

        public Rigidbody2D Rig
        {
            get
            {
                if (Base == null)
                {
                    PCharacterInstanceEcxepection.BaseIsNullExcpetion(this);
                    return null;
                }

                if (Base.CharacterType == PCharacterType.SOLID)
                {
                    PCharacterInstanceEcxepection.FetchRigFromSolidCharacterException(this);
                    return null;
                }

                return _Rig;
            }

            set
            {
                if (Base == null)
                {
                    PCharacterInstanceEcxepection.BaseIsNullExcpetion(this);
                    return;
                }

                if (Base.CharacterType == PCharacterType.SOLID)
                {
                    PCharacterInstanceEcxepection.FetchRigFromSolidCharacterException(this);
                    return;
                }

                _Rig = value;
            }
        }
        private Rigidbody2D _Rig;

        [Space(10f)]
        [SerializeField]
        private PCharacterInstanceController _Controller;
        public PCharacterInstanceController Controller { get { return _Controller; } set { _Controller = value; } }

        [Space(10f)]
        [SerializeField]
        private PCharacterInstanceAnimation _CurrentClip;
        public PCharacterInstanceAnimation CurrentAnimation { get { return _CurrentClip; } set { _CurrentClip = value; } }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (Base != null && gameObject.transform.childCount == 0)
            {
                gameObject.name = "Instance_" + Base.Name;
                RenderObject = new GameObject(Base.Name);
                InstanceName = name;
                RenderObject.transform.parent = transform;
                RenderObject.transform.localPosition = Vector2.zero;

                var collider = RenderObject.AddComponent<BoxCollider2D>();
                collider.offset = Vector2.zero;
                collider.size = RenderObject.transform.localScale;

                Render = RenderObject.AddComponent<SpriteRenderer>();
                if (Base.Animations.Length > 0 && Base.Animations[0].PFrames.Length > 0)
                    Render.sprite = Base.CharacterAtlas.GetSprite(Base.Animations[0].PFrames[0].FrameName);

                if (Base.CharacterType == PCharacterType.CHARACTER)
                {
                    Rig = gameObject.AddComponent<Rigidbody2D>();
                    Rig.gravityScale = Base.Configuration.GravityScale;
                    Rig.constraints = Base.Configuration.Constraints2D;
                }

                PCharacterInstanceUtil.RecalculateCollider(this);
            }
            else if (Render == null)
            {
                Render = GetComponentInChildren<SpriteRenderer>();
            }

            if (Base != null)
                PCharacterInstanceUtil.Refresh(this);
        }
#endif
    }
}