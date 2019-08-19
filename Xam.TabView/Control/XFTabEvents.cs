namespace Xam.TabView
{
    #region Enums

    /// <summary>
    /// Defines the Position
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// Defines the Top
        /// </summary>
        Top,
        /// <summary>
        /// Defines the Bottom
        /// </summary>
        Bottom
    }

    #endregion

    /// <summary>
    /// Defines the <see cref="OnTabClickedEventArgs" />
    /// </summary>
    public class OnTabClickedEventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTabClickedEventArgs"/> class.
        /// </summary>
        /// <param name="Item">The Item<see cref="XFTabPage"/></param>
        public OnTabClickedEventArgs(XFTabPage Item)
        {
            this.Item = Item;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Item
        /// </summary>
        public XFTabPage Item { get; }

        /// <summary>
        /// Gets or sets the SelectedIndex
        /// </summary>
        public int SelectedIndex { get; set; }

        #endregion
    }
}
