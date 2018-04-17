using System.Windows.Input;
using Xamarin.Forms;

namespace Xam.TabView
{
    public class XFTabHeaderViewModel
    {
        public XFTabHeaderViewModel()
        {
            tapCommand = new Command(OnTabClick);
        }
        ICommand tapCommand;
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }
        private void OnTabClick(object obj)
        {
            XFTabPage page = (XFTabPage)obj;
            OnTabClickedEventArgs args = new OnTabClickedEventArgs(page);
            page.XFTabParent.OnTabClicked(args);
            page.XFTabParent.SelectPage(page);
        }
    }
}
