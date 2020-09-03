using Xamarin.Forms;

namespace Xam.DataGrid.Control
{
    /// <summary>
    /// Defines the <see cref="GridStyle" />.
    /// </summary>
    public class GridStyle : BindableObject
    {
        #region Public_Internal_Properties

        /// <summary>
        /// Gets or sets the FontAttributes.
        /// </summary>
        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the FontFamily.
        /// </summary>
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the FontSize.
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the TextColor.
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the HorizontalTextAlignment.
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the VerticalTextAlignment.
        /// </summary>
        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(VerticalTextAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the FontAttributesProperty.
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create (nameof(FontAttributes), typeof(FontAttributes), typeof(GridStyle), FontAttributes.None);

        /// <summary>
        /// Defines the FontFamilyProperty.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create (nameof(FontFamily), typeof(string), typeof(GridStyle), null);

        /// <summary>
        /// Defines the FontSizeProperty.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create (nameof(FontSize), typeof(double), typeof(GridStyle), 14.0);

        /// <summary>
        /// Defines the TextColorProperty.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create (nameof(TextColor), typeof(Color), typeof(GridStyle), Color.Default);

        /// <summary>
        /// Defines the HorizontalTextAlignmentProperty.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create (nameof(HorizontalTextAlignment), typeof(TextAlignment ), typeof(GridStyle), TextAlignment.Start);

        /// <summary>
        /// Defines the VerticalTextAlignmentProperty.
        /// </summary>
        public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create (nameof(VerticalTextAlignment), typeof(TextAlignment ), typeof(GridStyle), TextAlignment.Start);

        #endregion
    }
}
