//=============================================================================
//
// (C) BLACKTRIANGLES 2014
// http://www.blacktriangles.com
//
// Howard N Smith | hsmith | howard@blacktriangles.com
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Cafe
{
    public class UIListItem
        : UIElement
        , IBeginDragHandler
        , IDragHandler
        , IEndDragHandler
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public System.Object data                               { get; private set; }
        public UIList parent                                    { get; private set; }
        public int index                                        { get; private set; }
        public bool isSelected                                  { get { return parent.isSelected(this); } }

        [Header("Draggable")]
        public bool isDraggable                                 = false;
        public UIElement dragDisplayPrefab                  = null;
        private UIElement dragDisplay                       = null;

        //
        // constructor / initializer //////////////////////////////////////////
        //

        public virtual void Initialize( UIList _parent )
        {
            parent = _parent;
        }

        //
        // public methods /////////////////////////////////////////////////////
        //

        public virtual void Refresh( System.Object _data, int _index )
        {
            data = _data;
            index = _index;
            Refresh();
        }

        //
        // --------------------------------------------------------------------
        //
        
        public virtual void Refresh()
        {
        }

        //
        // --------------------------------------------------------------------
        //

        public virtual void Select()
        {
            if(parent != null)
            {
                parent.SelectItem(index);
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public void OnBeginDrag(PointerEventData ev)
        {
            if(!isDraggable) return;
            if(dragDisplay != null)
            {
                Destroy(dragDisplay.gameObject);
            }

            dragDisplay = MakeDragDisplay();
            if(dragDisplay == null) return;
            if(dragDisplay != null)
            {
                dragDisplay.transform.SetParent(CafeManager.instance.screenCanvas.transform);
                dragDisplay.transform.SetAsLastSibling();
            }

            UpdateDrag(ev);
        }

        //
        // --------------------------------------------------------------------
        //

        public void OnDrag(PointerEventData ev)
        {
            if(!isDraggable) return;
            if(dragDisplay == null) return;

            UpdateDrag(ev);
        }

        //
        // --------------------------------------------------------------------
        //

        public void OnEndDrag(PointerEventData ev)
        {
            if(!isDraggable) return;
            if(dragDisplay == null) return;

            Destroy(dragDisplay.gameObject);
        }

        //
        // protected methods //////////////////////////////////////////////////
        //

        protected virtual UIElement MakeDragDisplay()
        {
            if(dragDisplayPrefab == null) return null;
            UIElement result = Instantiate(dragDisplayPrefab);
            Dbg.Assert(result.canvasGroup != null && result.canvasGroup.blocksRaycasts == false, "Drag item needs a canvas group and cannot block raycasts (or no drop will ever happen)");
            return result;
        }

        //
        // private methods ////////////////////////////////////////////////////
        //

        private void UpdateDrag(PointerEventData ev)
        {
            Vector3 globalMousePos = Vector3.zero;
            RectTransform dragplane = CafeManager.instance.screenCanvas.transform as RectTransform;
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(
                dragplane,
                ev.position,
                ev.pressEventCamera,
                out globalMousePos))
            {
                dragDisplay.rectTransform.position = globalMousePos;
                dragDisplay.rectTransform.rotation = dragplane.rotation;
            }
        }
        
        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void OnDestroy()
        {
            if(dragDisplay != null)
            {   
                Destroy(dragDisplay.gameObject);
            }
        }       
    }
}
