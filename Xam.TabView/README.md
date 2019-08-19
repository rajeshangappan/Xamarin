Xamarin Forms Tab Control:

This tab control designed by Xamarin.Forms. It has basic tab control options.

Feature:

1. We can able to add more no tab pages.
2. Customize the tab header content and size.
3. Option for Tab Header Orientation.
4. Option for customize the header and body style changes.

Sample c#:
**C# Sample:**
            //XFTab Control
            XFTabControl tab = new XFTabControl
            {
                //Apply the Styles to Tab
                HeaderColor = Color.Gray,
                HeaderHeight = 30,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //Change the Header Position.
                Position = Position.Top
            };
            //Create the Tab Page

            XFTabPage page1 = new XFTabPage();

            //Create the Header title

            page1.Header.Title = "Tab1";

            Label content1 = new Label
            {
                Text = "This Page Displays Tab1 Content"
            };

            //Assign the tab body content.

            page1.Content = content1;

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

**Xaml Sample:**

<tabview:XFTabControl VerticalOptions="FillAndExpand" TabClicked="Tab_TabClicked" HeaderColor="Gray">
                <tabview:XFTabControl.XFTabPages>
                    <tabview:XFTabPage>
                        <tabview:XFTabPage.Header>
                            <tabview:XFTabHeader Title="Tab1">
                            </tabview:XFTabHeader>
                        </tabview:XFTabPage.Header>
                        <tabview:XFTabPage.Content>
                            <Label Text="This Page Displays Tab1 Content"></Label>
                        </tabview:XFTabPage.Content>
                    </tabview:XFTabPage>
                    <tabview:XFTabPage>
                        <tabview:XFTabPage.Header>
                            <tabview:XFTabHeader>
                                <Label Text="Tab2"></Label>
                            </tabview:XFTabHeader>
                        </tabview:XFTabPage.Header>
                        <tabview:XFTabPage.Content>
                            <Label Text="This Page Displays Tab2 Content"></Label>
                        </tabview:XFTabPage.Content>
                    </tabview:XFTabPage>
                </tabview:XFTabControl.XFTabPages>
            </tabview:XFTabControl>

**Event** :

     tab.TabClicked += Tab\_TabClicked;

            privatevoid Tab\_TabClicked(object sender, OnTabClickedEventArgs args)

            {

             //return the tab page. We can change that tab content here also.

            }


