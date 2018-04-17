using System;

namespace Xam.TabView
{
    public class OnTabClickedEventArgs
    {
        private XFTabPage m_item;

        public OnTabClickedEventArgs(XFTabPage Item)
        {
            m_item = Item;
        }
        public XFTabPage Item
        {
            get
            {
                return m_item;

            }
        }
    }
    public enum Position
    {
        Top,
        Bottom
    }
}
