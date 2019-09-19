using Xamarin.Forms;

namespace Xam.TabView
{
    /// <summary>
    /// Defines the <see cref="XFTabHeader" />
    /// </summary>
    public class XFTabHeader : Frame
    {
        #region Fields

        /// <summary>
        /// Defines the Selector
        /// </summary>
        internal BoxView Selector;

		/// <summary>
        /// Header Tab Page
        /// </summary>
        internal XFTabPage XFParentTabPage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XFTabHeader"/> class.
        /// </summary>
        public XFTabHeader()
        {
            CornerRadius = 0;
            Padding = 0;
            Margin = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get => HeaderLabel.Text;
            set
            {
                HeaderLabel = new Label
                {
                    Text = value
                };
                HeaderLabel.Opacity = 0.8;
                Content = HeaderLabel;
            }
        }

        /// <summary>
        /// Gets or sets the HeaderLabel
        /// </summary>
        internal Label HeaderLabel { get; set; }

        #endregion
    }
}
