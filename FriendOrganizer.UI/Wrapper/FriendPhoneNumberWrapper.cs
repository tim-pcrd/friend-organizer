﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Annotations;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendPhoneNumberWrapper:ModelWrapper<FriendPhoneNumber>
    {
        public FriendPhoneNumberWrapper(FriendPhoneNumber model) : base(model)
        {

        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value);}
        }
    }
}
