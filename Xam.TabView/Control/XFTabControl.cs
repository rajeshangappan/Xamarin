using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Xam.TabView
{
    #region Delegates

    /// <summary>
    /// The OnTabClickEventHandler
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="args">The args<see cref="OnTabClickedEventArgs"/></param>
    public delegate void OnTabClickEventHandler(object sender, OnTabClickedEventArgs args);

    #endregion

    /// <summary>
    /// Defines the <see cref="XFTabControl" />
    /// </summary>
    public class XFTabControl : Frame
    {
        #region Fields

        /// <summary>
        /// Defines the m_tabBodyColor
        /// </summary>
        private readonly Color m_tabBodyColor;

        /// <summary>
        /// Defines the TabbedPagesProperty
        /// </summary>
        public static BindableProperty TabbedPagesProperty = BindableProperty.Create(nameof(XFTabPages), typeof(ObservableCollection<XFTabPage>), typeof(XFTabControl), null, BindingMode.TwoWay);

        /// <summary>
        /// Defines the m_Parent
        /// </summary>
        internal Grid m_Parent;

        /// <summary>
        /// Defines the SelectedPage
        /// </summary>
        internal XFTabPage SelectedPage;

        /// <summary>
        /// Defines the m_Header
        /// </summary>
        private Grid m_Header;

        /// <summary>
        /// Defines the m_headerColor
        /// </summary>
        private Color m_headerColor;

        /// <summary>
        /// Defines the m_Selection
        /// </summary>
        private Grid m_Selection;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XFTabControl"/> class.
        /// </summary>
        public XFTabControl()
        {
            Padding = 0;
            Margin = 0;
            init();
            XFTabPages = new ObservableCollection<XFTabPage>();
            XFTabPages.CollectionChanged += XFTabPages_CollectionChanged;
        }

        #endregion

        #region Events

        /// <summary>
        /// Defines the TabClicked
        /// </summary>
        public event OnTabClickEventHandler TabClicked;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HeaderColor
        /// </summary>
        public Color HeaderColor
        {
            get => m_headerColor;
            set
            {
                m_headerColor = value;
                m_Header.BackgroundColor = value;
                m_Selection.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the HeaderHeight
        /// </summary>
        public int HeaderHeight { get; set; } = 30;

        /// <summary>
        /// Gets or sets the HeaderHeight
        /// </summary>
        public int HeaderSelectionHeight { get; set; } = 8;

        /// <summary>
        /// Gets or sets the HeaderTextColor
        /// </summary>
        public Color HeaderTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Position
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the selectedIndex
        /// </summary>
        public int selectedIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the SelectionColor
        /// </summary>
        public Color SelectionColor { get; set; } = Color.FromRgb(102, 153, 255);

        /// <summary>
        /// Gets or sets the XFTabPages
        /// </summary>
        public ObservableCollection<XFTabPage> XFTabPages { get => (ObservableCollection<XFTabPage>)GetValue(TabbedPagesProperty); set => SetValue(TabbedPagesProperty, value); }

        /// <summary>
        /// Gets or sets the XFTabBody
        /// </summary>
        internal Grid XFTabBody { get; set; }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddPage
        /// </summary>
        /// <param name="tabPage">The tabPage<see cref="XFTabPage"/></param>
        public void AddPage(XFTabPage tabPage)
        {
            if (tabPage?.Header != null && tabPage.Header.IsVisible)
            {
                XFTabPages.Add(tabPage);
            }
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The addTabPageContent
        /// </summary>
        /// <param name="tabPage">The tabPage<see cref="XFTabPage"/></param>
        private void addTabPageContent(XFTabPage tabPage)
        {
            if (tabPage?.Header != null && tabPage.Header.IsVisible)
            {
                tabPage.XFTabParent = this;
                tabPage.Header.BackgroundColor = HeaderColor;
                if (tabPage.Header.HeaderLabel != null)
                {
                    tabPage.Header.HeaderLabel.BackgroundColor = HeaderColor;
                }

                m_Header.Children.Add(tabPage.Header, m_Header.Children.Count, 0);
                tabPage.Header.Selector = new BoxView
                {
                    BackgroundColor = HeaderColor
                };
                m_Selection.Children.Add(tabPage.Header.Selector, m_Selection.Children.Count, 0);
            }
        }

        /// <summary>
        /// The init
        /// </summary>
        private void init()
        {
            m_Parent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            XFTabBody = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Header = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Selection = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
        }

        /// <summary>
        /// The TabLayout
        /// </summary>
        private void TabLayout()
        {
            if (Position == Position.Top)
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
                {
                new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) },
                new RowDefinition { Height = new GridLength(HeaderSelectionHeight, GridUnitType.Absolute) },
                new RowDefinition { Height = GridLength.Star}
                };
                m_Parent.Children.Add(m_Header, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(XFTabBody, 0, 2);
            }
            else
            {
                m_Parent.RowDefinitions = new RowDefinitionCollection
                {
                new RowDefinition { Height = GridLength.Star},
                new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute)},
                new RowDefinition { Height = new GridLength(HeaderHeight, GridUnitType.Absolute) }
                };
                m_Parent.Children.Add(XFTabBody, 0, 0);
                m_Parent.Children.Add(m_Selection, 0, 1);
                m_Parent.Children.Add(m_Header, 0, 2);
            }
        }

        /// <summary>
        /// The XFTabPages_CollectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/></param>
        private void XFTabPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var page = XFTabPages[e.NewStartingIndex];
                    addTabPageContent(page);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// The OnParentSet
        /// </summary>
        protected override void OnParentSet()
        {
            TabLayout();
            Content = m_Parent;
            var page = XFTabPages.FirstOrDefault(x => x.Header != null && x.Header.IsVisible);
            selectedIndex = XFTabPages.IndexOf(page);
            SelectPage(page);
            SetHeaderColor();
        }

        /// <summary>
        /// The SelectPage
        /// </summary>
        /// <param name="page">The page<see cref="XFTabPage"/></param>
        internal void SelectPage(XFTabPage page)
        {
            if (page?.Header != null && page.Header.IsVisible)
            {
                if (SelectedPage != null)
                {
                    SelectedPage.Header.Selector.BackgroundColor = HeaderColor;
                    SelectedPage.Header.Opacity = 0.8;
                }

                page.Header.Selector.BackgroundColor = SelectionColor;
                page.Header.Opacity = 1;

                SelectedPage = page;
                XFTabBody.Children.Clear();
                XFTabBody.Children.Add(page.Content);
            }
        }

        /// <summary>
        /// The SetHeaderColor
        /// </summary>
        internal void SetHeaderColor()
        {
            foreach (var page in XFTabPages)
            {
                if (page.Header.HeaderLabel != null)
                {
                    page.Header.HeaderLabel.TextColor = HeaderTextColor;
                }
            }
        }

        /// <summary>
        /// The OnTabClicked
        /// </summary>
        /// <param name="e">The e<see cref="OnTabClickedEventArgs"/></param>
        internal void OnTabClicked(OnTabClickedEventArgs e)
        {
            if (TabClicked != null)
            {
                TabClicked(this, e);
            }
        }
    }
}
