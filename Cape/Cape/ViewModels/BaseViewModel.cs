using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cape.Models;

namespace Cape.ViewModels
{
    public class BaseViewModel
    {
        protected ApplicationUser _User;

        public BaseViewModel(ApplicationUser User)
        {
            _User = User;
        }
        public BaseViewModel()
        { }

    }
}