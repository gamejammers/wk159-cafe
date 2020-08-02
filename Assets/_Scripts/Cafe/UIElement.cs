//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{

    // All cafe UI elements inherit from this class, it has some useful features
    // like direct access to rectTransforms and canvas groups, as well as Hide
    // and Show methods.

    [RequireComponent(typeof(RectTransform))]
    public class UIElement
        : MonoBehaviour
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        public RectTransform rectTransform                      { get; private set; }
        public CanvasGroup canvasGroup                          { get; private set; }
        public CafeManager cafe                                 { get; protected set; }

        public bool isVisible                                   { get { return canvasGroup == null ? gameObject.activeSelf : canvasGroup.alpha > 0f; } }

        [Header("Interactive")]
        public bool blocksRaycasts                              = false;
        public bool interactable                                = false;

        //
        // public methods /////////////////////////////////////////////////////
        //
        
        public virtual void Show()
        {
            if(canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = interactable;
                canvasGroup.interactable = interactable;
                canvasGroup.alpha = 1f;
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        //
        // --------------------------------------------------------------------
        //
        
        public virtual void Hide()
        {
            if(canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                canvasGroup.alpha = 0f;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public bool SetVisible(bool visible)
        {
            if(visible)
                Show();
            else
                Hide();

            return visible;
        }
        
        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
