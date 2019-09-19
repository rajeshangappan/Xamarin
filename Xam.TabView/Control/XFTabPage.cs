using System;
using Xam.TabView.Control;
using Xamarin.Forms;

namespace Xam.TabView
{
    /// <summary>
    /// Defines the <see cref="XFTabPage" />
    /// </summary>
    public class XFTabPage
    {
        #region Fields

        /// <summary>
        /// Defines the m_content
        /// </summary>
        internal ContentPage m_content;

        /// <summary>
        /// Defines the m_contentPage
        /// </summary>
        internal ContentPage m_contentPage;

        /// <summary>
        /// Defines the XFTabParent
        /// </summary>
        internal XFTabControl XFTabParent;

        /// <summary>
        /// Defines the m_header
        /// </summary>
        private XFTabHeader m_header;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XFTabPage"/> class.
        /// </summary>
        public XFTabPage()
        {
            m_content = new ContentPage();
            Header = new XFTabHeader();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public View Content { get => m_content.Content; set => m_content.Content = value; }

        /// <summary>
        /// Gets or sets the CustomContentPage
        /// </summary>
        public ContentPage CustomContentPage { get => m_content; set => m_content = value; }

        /// <summary>
        /// Gets or sets the Header
        /// </summary>
        public XFTabHeader Header
        {
            get => m_header;
            set
            {
                m_header = value;
                InitHeaderEvent();
            }
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The TapGestureRecognizer_Tapped
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnTabClickedEventArgs args = new OnTabClickedEventArgs(this)
            {
                SelectedIndex = XFTabParent.XFTabPages.IndexOf(this)
            };
            XFTabParent.OnTabClicked(args);
            XFTabParent.SelectTabPage(this);
        }

        #endregion

        /// <summary>
        /// The InitHeaderEvent
        /// </summary>
        internal void InitHeaderEvent()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            m_header.GestureRecognizers.Add(tapGestureRecognizer);
            tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        }
    }
}
