using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xam.TabView
{
    public delegate void OnTabClickEventHandler(object sender, OnTabClickedEventArgs args);
    public class XFTabControl : Frame
    {

        protected override void OnParentSet()
        {
            TabLayout();
            Content = m_Parent;
        }
        internal Grid m_Parent;
        private Grid m_Body;
        private Grid m_Header;
        private Grid m_Selection;
        private int m_headerHeight = 30;
        private IList<XFTabPage> m_tabPages;
        private Color m_headerColor, m_textColor, m_tabBodyColor;
        internal XFTabPage SelectedPage;
        private Position m_pos;
        public XFTabControl()
        {
            Padding = 0;
            Margin = 0;
            init();
        }
        void init()
        {
            m_Parent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Body = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Header = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Selection = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_tabPages = new List<XFTabPage>();
        }
        void TabLayout()
        {
            if (Position == Position.Top)
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
                {
                new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) },
                new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute) },
                new RowDefinition { Height = GridLength.Star}
                };
                m_Parent.Children.Add(m_Header, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(m_Body, 0, 2);
            }
            else
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
                {
                new RowDefinition { Height = GridLength.Star},
                new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute)},
                new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) }
                };
                m_Parent.Children.Add(m_Body, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(m_Header, 0, 2);
            }
        }
        #region Properties
        public Color HeaderColor
        {
            get
            {
                return m_headerColor;
            }
            set
            {
                m_headerColor = value;
                m_Header.BackgroundColor = value;
                m_Selection.BackgroundColor = value;
            }
        }
        public Color TextColor
        {
            get
            {
                return m_textColor;
            }
            set
            {
                m_textColor = value;
            }
        }
        public Position Position
        {
            get
            {
                return m_pos;
            }
            set
            {
                m_pos = value;
            }
        }
        public Color XFTabBodyColor
        {
            get
            {
                return m_tabBodyColor;
            }
            set
            {
                m_tabBodyColor = value;
                XFTabBody.BackgroundColor = value;
            }
        }

        public IList<XFTabPage> XFTabPages
        {
            get
            {
                return m_tabPages;
            }
        }
        public int HeaderHeight
        {
            get
            {
                return m_headerHeight;
            }
            set
            {
                m_headerHeight = value;
            }
        }
        internal Grid XFTabBody
        {
            get
            {
                return m_Body;
            }
            set
            {
                m_Body = value;
            }
        }
        #endregion

        #region Method
        public void AddPage(XFTabPage tabPage)
        {
            tabPage.XFTabParent = this;
            tabPage.CreateTabLabel();
            XFTabPages.Add(tabPage);
            m_Header.Children.Add(tabPage.header, m_Header.Children.Count, 0);


            BoxView Bv = new BoxView();
            Bv.BackgroundColor = HeaderColor;
            tabPage.header.Selector = Bv;
            m_Selection.Children.Add(Bv, m_Selection.Children.Count, 0);

            if (m_Header.Children.Count == 1 && tabPage.Content != null)
            {
                XFTabBody.Children.Add(tabPage.Content);
                SelectPage(tabPage);
            }
        }

        internal void SelectPage(XFTabPage page)
        {
            if (SelectedPage != null)
            {
                SelectedPage.header.Opacity = 1;
                SelectedPage.header.FontStyle = FontAttributes.None;
                SelectedPage.header.Selector.BackgroundColor = HeaderColor;
                SelectedPage.header.TextColor = TextColor;
            }

            Color c = HeaderColor;
            page.header.TextColor = Color.White;
            page.header.FontStyle = FontAttributes.Bold;
            page.header.Selector.BackgroundColor = Color.White;

            SelectedPage = page;
            XFTabBody.Children.Clear();
            XFTabBody.Children.Add(page.Content);
        }
        #endregion

        internal void OnTabClicked(OnTabClickedEventArgs e)
        {
            if (TabClicked != null)
            {
                TabClicked(this, e);
            }
        }
        public event OnTabClickEventHandler TabClicked;
    }
}
