using System.Configuration;
using System.Data;
using System.Windows;
using static Mader_Control_System.Login.LoginWindow;

namespace Mader_Control_System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseHelper db = new DatabaseHelper();
        }
    }

}
