Xamarin Forms Tab Control:

This tab control designed by Xamarin.Forms. It has basic tab control options.

Feature:

1. We can able to add more no tab pages.
2. Customize the tab header content and size.
3. Option for Tab Header Orientation.
4. Option for customize the header and body style changes.

NameSpace:

using XFTabComponent;

**Creation of object:**

            //Create the Tab Control

            XFTabControl tab = new XFTabControl();

**Add the Styles:**

            //Apply the Styles to Tab

            tab.XFTabBodyColor = Color.Pink;

            tab.HeaderColor = Color.FromRgb(36,133,139);

            tab.TextColor = Color.FromRgb(165, 211, 190);

            tab.HeaderHeight = 30;

**Change the Header Position:**

            //Change the Header Position.

            tab.Position = Position.Top;

**Create the tab page and add to Control:**

- **Tab Page Header with text:**

            //Create the Tab Page

            XFTabPage Tp = new XFTabPage();

            //Assign the header Text

            Tp.Header.Text = &quot;tab1&quot;;

            Label l = new Label();

            l.Text = &quot;tab1&quot;;

            l.BackgroundColor = Color.Green;

            //Assign the tab body content.

            Tp.Content = l;

            //Add the Page to tab control

            tab.AddPage(Tp);

- **Tab page header with custom element:**

             //Create the Image

            Image img = new Image();

            img.Source = ImageSource.FromResource(&quot;XFTabControl1.sample.PNG&quot;);

            //Create the Tab Page

            XFTabPage Tp1 = new XFTabPage();

            //Assign the header Content

            Tp1.Header.Content = img;

            l = new Label();

            l.Text = &quot;tab4&quot;;

            l.BackgroundColor = Color.SkyBlue;

            //Assign the tab body content.

            Tp1.Content = l;

            //Add the Page to tab control

            tab.AddPage(Tp1);

**Event** :

     tab.TabClicked += Tab\_TabClicked;

            privatevoid Tab\_TabClicked(object sender, OnTabClickedEventArgs args)

            {

             //return the tab page. We can change that tab content here also.

            }

Output:
![Image of Tab](https://github.com/rajeshangappan/Xamarin/blob/master/Xam.TabView/TabControl.gif)