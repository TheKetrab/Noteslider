using Noteslider.Code.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code.Exceptions
{
    public class ForUserException : Exception
    {

        public ForUserException() { }
        public ForUserException(string msg) : base(msg)
        {
            
        }

        public void ShowGUIMessage()
        {
            InfoDialog.ShowMessageDialog(Message);
        }

    }
}
