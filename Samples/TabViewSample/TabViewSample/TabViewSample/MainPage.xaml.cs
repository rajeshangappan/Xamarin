using Xam.TabView;
using Xamarin.Forms;

namespace TabViewSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
            //Create the Tab from c# coding
            //testing.Children.Add(createTab());
        }

        private XFTabControl createTab()
        {
            XFTabControl tab = new XFTabControl
            {
                //Apply the Styles to Tab
                HeaderColor = Color.Gray,
                HeaderHeight = 30,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //Change the Header Position.
                Position = Position.Top
            };

            XFTabPage page1 = new XFTabPage();

            //Create the Header title

            page1.Header.Title = "Tab1";

            Tab1ContentPage contentpage = new Tab1ContentPage
            {
                Content = new Label
                {
                    Text = "This Page Displays Tab1 Content"
                }
            };

            //Assign the contentpage to first tab.

            page1.CustomContentPage = contentpage;

            //Add the Page to tab control

            tab.AddPage(page1);

            //Create the Tab Page

            XFTabPage page2 = new XFTabPage();

            //Assign the header Text
            Label headerLabel = new Label
            {
                Text = "Tab2"
            };

            page2.Header.Content = headerLabel;

            // Page content
            Label content2 = new Label
            {
                Text = "This Page Displays Tab2 Content"
            };

            //Assign the tab body content.

            page2.Content = content2;

            //Add the Page to tab control

            tab.AddPage(page2);

            tab.TabClicked += Tab_TabClicked;

            return tab;
        }
        private void Tab_TabClicked(object sender, OnTabClickedEventArgs args)
        {
            //return the tab page. We can change that tab content here also.
        }
    }
}
