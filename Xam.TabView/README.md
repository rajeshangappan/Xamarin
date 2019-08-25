# Xam.TabView
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://github.com/rajeshangappan/Xamarin)

This Package support in Xamarin.Forms
Xam.TabView Features
- Tab Header Customization (Tab Header Height/Color/Content).
- Tab contet Page customization.
- Tab Header Positioning (Top/Bottom)
- Tab page/content change events.
# New Features!
  - Tabbed Page and Header Customization.
  - Add tab view to any layouts like (Stack/absolute or grid etc.)
Sample Link : [TabviewSample](https://github.com/rajeshangappan/Xamarin/tree/master/Samples/TabViewSample)
# Tabbed Header Customization Sample Code
 - Add the header using title property.
 - Add the Header using Header Content property.
```
  //Assign the Content to tabheader
  Label label = new Label{Text="Tab1"};
  PageHeader1.Content = label;
  //Use the Title for Default Header
  PageHeader1.Title = "Tab1";
```
# Tabbed Page content Customization
 - Add the tabview content by using content.
 - Add the tabview content by using custom content page.
```
  //content
  Label label = new Label{Text="Tab Page content"};
  Page1.Content = label;
  //Assign the page to tabview
  ContentPage samplePage = new ContentPage();
  Page1.CustomContentPage = samplePage;
```
# Output
![TabView](https://github.com/rajeshangappan/Xamarin/blob/master/Xam.TabView/TabControl.gif)