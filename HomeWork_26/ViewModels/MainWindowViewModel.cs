﻿using HomeWork_26.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна

        /// <summary>Заголовок окна</summary>
        /// 
        private string _Title = "Bank A";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        } 
        #endregion
    }
}