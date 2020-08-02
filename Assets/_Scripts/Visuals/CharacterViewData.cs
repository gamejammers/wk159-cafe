//
//
//

using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(fileName="CharacterViewData", menuName="Art/CharacterViewData")]
    public class CharacterViewData
        : ScriptableObject
    {
        //
        // types //////////////////////////////////////////////////////////////
        //

        public enum Part
        {
            Body = 3,
            Feet = 2,
            Hands = 1,
            Head = 0
        }
        
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Texture2D texture                                = null;
        public float spriteSize                                 = 12f;
        public Vector2 pivot                                    = new Vector2(0.5f, 0f);
        public float pixelsPerUnit                              = 0f;
        public CafeCharacterView cafeViewPrefab                 = null;

        //
        // initialize /////////////////////////////////////////////////////////
        //
        
        public GameObject Create(string name)
        {
            return Create(name, new System.Random());
        }

        //
        // --------------------------------------------------------------------
        //

        public GameObject Create(string name, System.Random rnd)
        {
            if( texture == null) return null;

            int count = GetVariationCount();

            GameObject go = new GameObject(name);
            CreatePart(go.transform, Part.Body, rnd.Next(count));
            CreatePart(go.transform, Part.Feet, rnd.Next(count));
            CreatePart(go.transform, Part.Hands, rnd.Next(count));
            CreatePart(go.transform, Part.Head, rnd.Next(count));

            return go;
        }

        //
        // --------------------------------------------------------------------
        //
        
        public void Create(Transform root, Character character)
        {
            CreateSpriteRenderer(root, character.bodySprite);
            CreateSpriteRenderer(root, character.feetSprite);
            CreateSpriteRenderer(root, character.handsSprite);
            CreateSpriteRenderer(root, character.headSprite);
        }

        //
        // --------------------------------------------------------------------
        //
        
        public int GetVariationCount()
        {
            return (int)(texture.width / spriteSize) - 1;
        }

        //
        // --------------------------------------------------------------------
        //

        public Sprite CreateSprite(Part part, int index)
        {
            float ypos = (int)part * spriteSize;
            float xpos = (index+1) * spriteSize;
            Rect rect = new Rect(xpos, ypos, spriteSize, spriteSize);
            Sprite result = Sprite.Create(texture, rect, pivot, pixelsPerUnit);
            result.name = part.ToString();
            return result;
        }

        //
        // --------------------------------------------------------------------
        //
        
        public SpriteRenderer CreateSpriteRenderer(Transform root, Sprite sprite)
        {
            GameObject go = new GameObject(sprite.name.ToString());

            go.transform.SetParent(root);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            return sr;
        }

        //
        // --------------------------------------------------------------------
        //

        public void CreatePart(Transform root, Part part, int idx)
        {
            CreateSpriteRenderer(root, CreateSprite(part, idx));
        }
    }
}
