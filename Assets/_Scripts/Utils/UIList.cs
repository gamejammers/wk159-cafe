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
    public class UIList
        : UIElement
        , IEnumerable
        , IEnumerable<UIElement>
        , IDropHandler
    {
        //
        // types //////////////////////////////////////////////////////////////
        //

        public delegate void SelectionCallback(int index, System.Object selection);
        public event SelectionCallback OnSelection;

        public delegate void DragDropCallback(PointerEventData ev);
        public event DragDropCallback OnDropped;
        
        //
        // members ////////////////////////////////////////////////////////////
        //

        [SerializeField] ScrollRect scrollRect                  = null;
        [SerializeField] UIListItem itemPrefab                  = null;
        [SerializeField] UIElement content                  = null;
        [SerializeField] UIElement emptyPanel               = null;
        [SerializeField] Image expandToggle                     = null;
        [SerializeField] Sprite expandedIcon                    = null;
        [SerializeField] Sprite collapsedIcon                   = null;
        public GameObject eventHandler                          = null;

        public bool isExpanded                                  { get; private set; }
        public int selectedItemIndex                            { get { return GetSelectedItemIndex(); } set { SelectItem( value ); } }
        public UIListItem selectedItem                          { get { return GetSelectedItem(); } set { SelectItem( value ); } }
        public System.Object selectedItemData                   { get { return GetSelectedItemData(); } set { SelectItemFromData( value ); } }

        private int selectedIndex                               = -1;
        private List<System.Object> itemData                    = new List<System.Object>();
        private List<UIListItem> itemPanels                     = new List<UIListItem>();

        //
        // public methods /////////////////////////////////////////////////////
        //

        public bool Refresh()
        {
            if( expandToggle )
            {
                expandToggle.sprite = isExpanded ? expandedIcon : collapsedIcon;
            }

            itemPanels.SetLength(
                itemData.Count
              , (index)=>{
                    UIListItem newItem = Instantiate( itemPrefab );
                    newItem.Initialize( this );
                    newItem.transform.SetParent( content.transform );
                    return newItem;
              }
              , (item,index)=>{
                    Destroy( item.gameObject );
              }
            );

            for( int i = 0; i < itemData.Count; ++i )
            {
                itemPanels[i].Refresh( itemData[i], i );
            }

            if(emptyPanel != null)
            {
                emptyPanel.SetVisible(itemData.Count <= 0);
            }
            
            return true;
        }

        //
        // --------------------------------------------------------------------
        //

        public void ShowSelectedItem()
        {
            if(scrollRect != null)
            {
                UIListItem sel = GetSelectedItem();
                float yOffset = sel.rectTransform.localPosition.y;
                float yScroll = 1.0f + (yOffset / content.rectTransform.rect.height);
                scrollRect.verticalNormalizedPosition = yScroll;
            }
        }

        //
        // public access methods //////////////////////////////////////////////
        //

        public int GetIndex( UIListItem selection )
        {
            return itemPanels.IndexOf( selection );
        }

        //
        // --------------------------------------------------------------------
        //

        public int GetIndexFromData( System.Object selection )
        {
            return itemData.IndexOf( selection );
        }

        //
        // --------------------------------------------------------------------
        //

        public UIElement GetItem( int index )
        {
            if( itemPanels.IsValidIndex(index) == false ) return null;
            return itemPanels[index];
        }

        //
        // --------------------------------------------------------------------
        //

        public void SelectItem( int index )
        {
            selectedIndex = index;
            NotifySelection();
            Refresh();
        }

        //
        // --------------------------------------------------------------------
        //

        public void SelectItem( UIListItem selection )
        {
            int idx = GetIndex(selection);
            SelectItem(idx);
        }

        //
        // --------------------------------------------------------------------
        //

        public void SelectItemFromData( System.Object selection )
        {
            int idx = GetIndexFromData(selection);
            SelectItem(idx);
        }

        //
        // --------------------------------------------------------------------
        //

        public bool isSelected(int index)
        {
            return index == selectedIndex;
        }

        //
        // --------------------------------------------------------------------
        //
        
        public bool isSelected( UIListItem selection )
        {
            if(selection == null) return false;
            return isSelected(selection.index);
        }

        //
        // --------------------------------------------------------------------
        //

        public UIElement GetItemFromData( System.Object data )
        {
            UIElement result = null;

            int i = itemData.IndexOf( data );
            if( i >= 0 )
            {
                result = itemPanels[i];
            }

            return result;
        }

        //
        // --------------------------------------------------------------------
        //

        public void SetData( IEnumerable data, bool filterNull = true )
        {
            itemData.Clear();
            if(data != null)
            {
                foreach( System.Object obj in data )
                {
                    if(!filterNull || obj != null)
                    {
                        itemData.Add( obj );
                    }
                }
            }
            Refresh();
        }

        //
        // ui callbacks ///////////////////////////////////////////////////////
        //

        public void ToggleExpanded()
        {
            isExpanded = !isExpanded;
            Refresh();
        }

        //
        // --------------------------------------------------------------------
        //
        
        public virtual void OnDrop(PointerEventData ev)
        {
            if(OnDropped != null)
            {
                OnDropped(ev);
            }
        }

        //
        // private methods ////////////////////////////////////////////////////
        //

        private int GetSelectedItemIndex()
        {
            return selectedIndex;
        }

        //
        // --------------------------------------------------------------------
        //

        private UIListItem GetSelectedItem()
        {
            if(itemPanels.IsValidIndex(selectedIndex))
            {
                return itemPanels[selectedIndex];
            }

            return null;
        }

        //
        // --------------------------------------------------------------------
        //

        private System.Object GetSelectedItemData()
        {
            UIListItem item = GetSelectedItem();
            if( item != null ) return item.data;
            return null;
        }

        //
        // --------------------------------------------------------------------
        //
        
        private void NotifySelection()
        {
            if(OnSelection != null)
            {
                OnSelection(GetSelectedItemIndex(), GetSelectedItemData());
            }
        }

        //
        // operators //////////////////////////////////////////////////////////
        //

        IEnumerator IEnumerable.GetEnumerator()
        {
            return itemPanels.GetEnumerator();
        }

        //
        // --------------------------------------------------------------------
        //

        public IEnumerator<UIElement> GetEnumerator()
        {
            return itemPanels.GetEnumerator();
        }

        //
        // end class //////////////////////////////////////////////////////////
        //

    }
}
