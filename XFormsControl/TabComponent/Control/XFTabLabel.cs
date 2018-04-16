using Xamarin.Forms;

namespace XFTabComponent
{
    public class XFTabHeader : Frame
    {
        Label label = new Label();
        internal BoxView Selector;
        public XFTabHeader()
        {
            CornerRadius = 0;
            Padding = 0;
            Margin = 0;
            label.TextColor = Color.FromRgb(165, 211, 190);
        }
        public string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
                Content = label;
            }
        }

        internal FontAttributes FontStyle
        {
            get
            {
                return label.FontAttributes;
            }
            set
            {
                label.FontAttributes = value;
            }
        }
        internal Color TextColor
        {
            get
            {
                return label.TextColor;
            }
            set
            {
                label.TextColor = value;
            }
        }
    }
}
