﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Exceptions
{
    public class NewTrackDialogException : ForUserException
    {
        public NewTrackDialogException(string msg) : base(msg)
        {
        }  
    }
}
