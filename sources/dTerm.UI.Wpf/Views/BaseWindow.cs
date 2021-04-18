﻿using ReactiveUI;
using System.Windows;

namespace dTerm.UI.Wpf.Views
{
    public abstract class BaseWindow<TViewModel> : Window, IViewFor<TViewModel> where TViewModel : class
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(TViewModel),
            typeof(BaseWindow<TViewModel>),
            new PropertyMetadata(default(TViewModel))
        );

        public TViewModel BindingRoot => ViewModel;

        public TViewModel ViewModel
        {
            get => (TViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel)value;
        }
    }
}