using ProjektManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektManager.Logic
{
    public class WindowService
    {

        public WindowService() { }

        public static void OpenWindow(Window w)
        {
            w.Show();

        }

        public static bool IsWindowOpen(Type windowType)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if(window.GetType() == windowType)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
