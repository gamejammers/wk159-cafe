//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class GenericListItem
        : UIListItem
    {
        //
        // types //////////////////////////////////////////////////////////////
        //

        public class Payload
        {
            public string message;
            public Sprite img;
        }
        
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("Item UI")]
        public TextMeshProUGUI message;
        public Image image;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public override void Refresh()
        {
            base.Refresh();

            Sprite sprite = null;
            string msg = System.String.Empty;

            if(data is Payload pld)
            {
                msg = pld.message;
                sprite = pld.img;
            }
            else if(data is System.String str)
            {
                msg = str;
            }
            else if(data is Sprite spr)
            {
                sprite = spr;
            }

            if(message != null)
                message.text = msg;

            if(image != null)
                image.sprite = sprite;
        }
        
    }
}
