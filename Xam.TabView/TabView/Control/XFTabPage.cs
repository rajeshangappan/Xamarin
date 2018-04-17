using Xamarin.Forms;

namespace Xam.TabView
{
    public class XFTabPage
    {
        internal ContentPage m_content;
        internal XFTabControl XFTabParent;
        internal XFTabHeader header;
        public XFTabPage()
        {
            m_content = new ContentPage();
            header = new XFTabHeader();
        }
        public View Content
        {
            get
            {
                return m_content.Content;
            }
            set
            {
                m_content.Content = value;
            }
        }

        public XFTabHeader Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        internal void CreateTabLabel()
        {
            header.BindingContext = new XFTabHeaderViewModel();
            var tapGestureRecognizer = new TapGestureRecognizer();
            header.GestureRecognizers.Add(tapGestureRecognizer);
            Binding onClickBinding = new Binding("TapCommand");
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, onClickBinding);
            tapGestureRecognizer.CommandParameter = this;
            header.BackgroundColor = XFTabParent.HeaderColor;
        }
    }
}
