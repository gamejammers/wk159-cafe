//
//
//

using UnityEngine;

namespace Soapy
{
    [CreateAssetMenu(fileName="CharacterView", menuName="Art/CharacterView")]
    public class CharacterView
        : ScriptableObject
    {
        //
        // types //////////////////////////////////////////////////////////////
        //

        private enum Part
        {
            Body = 0,
            Feet,
            Hands,
            Head
        }
        
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Texture2D texture;
        public float spriteSize;
        public Vector2 pivot;
        public float pixelsPerUnit;

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

            GameObject go = new GameObject(name);
            CreatePart(go, Part.Head, rnd);
            CreatePart(go, Part.Hands, rnd);
            CreatePart(go, Part.Feet, rnd);
            CreatePart(go, Part.Body, rnd);

            return go;
        }

        //
        // --------------------------------------------------------------------
        //

        private void CreatePart(GameObject root, Part part, System.Random rnd)
        {
            int count = (int)(texture.width / spriteSize);
            
            float ypos = (int)part * spriteSize;
            float xpos = rnd.Next(1, count) * spriteSize;
            Rect rect = new Rect(xpos, ypos, spriteSize, spriteSize);

            GameObject go = new GameObject(part.ToString());

            go.transform.SetParent(root.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit);
            sr.sortingOrder = (int)part;
        }
    }
}
