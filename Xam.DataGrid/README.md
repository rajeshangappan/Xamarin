**Xam.GridView**

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://github.com/rajeshangappan/Xamarin)

- This control useful to show the list of items in grid view. It's implemented by xamarin.forms. And it's not depends on native UI (xamarin.android and ios).

Sample code:

XML:

```
<control:XFDataGridControl ItemsSource="{Binding Itemsource}" EnablePagination="True"  ShowRecordPerPages="12" 
       GridBorderColor="Blue" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">                
</control:XFDataGridControl> 
```

C#:
```
XFDataGridControl control = new XFDataGridControl();
control.ItemsSource = model.Itemsource;
control.ColumnsSource = (List<XFGridColumn>)model.GridColumns;
control.EnablePagination = true;
control.ShowRecordPerPages = 12; 
```
Features:

- Load the list of items in grid view.
  - This component implemented using xamarin.Forms. It's not depends on xamarin.android\ios.
- Pagination support
  - We can use the **"EnablePagination"** property to achieve the basic pagination support.
- Virtual pagination support.
  - We can use the below property and events to achive this.
  - **"EnablePagination"** and **"EnableVirtualPagination"** should be true and use the **"OnNeedDataSource"** events to load the source dynamically.
  - Also, should mention the total record count in "VirtualRecordCount";
- Pull To Refresh Support
  - **"EnablePullToRefresh"** property should be true.
  - Should implement the "OnPullToRefresh"; event and add the source items as per your needs.
- Header Style
  - We can customize the header text styles using **"GridHeaderStyle"** object.
- Item Style
  - We can customize the Item text styles using **"GridItemStyle"** object.
- Customize the Grid Header, Item and paginator.

| **Property** | **Usage** |
| --- | --- |
| GridRowHeight | Using this we can set the height of the grid row |
| HeaderHeight | We can set the height of Grid Header. |
| ItemsSource | Source of the Grid |
| PaginatorHeight | We can set the paginator Height |
| ShowRecordPerPages | On Pagination, shows the record per page |
| GridBackgroundColor | We can set the background color to grid |
| GridBorderColor | We can set the border color to grid |
| GridBorderWidth | We can set the grid border width |
| HeaderColor | We can set the color of the grid header. |

Events:

| Event name | Comments |
| --- | --- |
| OnItemSelect | It will trigger when selecting the grid item |
| OnPullToRefresh | It will trigger when pull to refresh action happened. |
| OnNeedDataSource | It will trigger below scenario.Virtual Pagination – when you are selecting the page index.Sorting – on virtual pagination when you are trying to show the new data.This event will override your existing data and it will show the new item source. |


Sample Link : [GridSample](https://github.com/rajeshangappan/Xamarin/tree/master/Samples/GridSample)