﻿using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Windows;
using UI.Wpf.Processes;

namespace UI.Wpf.Shell
{
	public partial class ShellView : IViewFor<IShellViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public ShellView()
		{
			InitializeComponent();

			this.WhenActivated(activator =>
			{
				activator(this.WhenAnyValue(@this => @this.ViewModel).BindTo(this, @this => @this.DataContext));

				activator(this.WhenAnyValue(@this => @this.ViewModel.Processes.ConsolesPanel.StartProcessAsAdmin).Subscribe(startingAsAdmin =>
				{
					runAsAdminWarningInfo.Visibility = startingAsAdmin ? Visibility.Visible : Visibility.Collapsed;
				}));

				activator(this.WhenAnyValue(@this => @this.ViewModel.Processes).Subscribe(processes =>
				{
					processes.ConsolesPanel.Consoles.CountChanged.Subscribe(count =>
					{
						var visibility = count > 0 ? Visibility.Visible : Visibility.Collapsed;
						consolesPanelHostSepparator.Visibility = visibility;
						consolesPanelHost.Visibility = visibility;
					});

					processes.OpenConfigsInteraction.RegisterHandler(context =>
					{
						var settingsView = new ConfigsView(context.Input)
						{
							Owner = Application.Current.MainWindow
						};

						settingsView.ShowDialog();

						context.SetOutput(Unit.Default);
					});
				}));

				activator(minimizedInstancesScrollViewer.Events().PreviewMouseWheel.Subscribe(args =>
				{
					if (args.Delta > 0)
						minimizedInstancesScrollViewer.LineLeft();
					else
						minimizedInstancesScrollViewer.LineRight();

					args.Handled = true;
				}));
			});
		}

		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(IShellViewModel), typeof(ShellView), new PropertyMetadata(null));

		public IShellViewModel ViewModel
		{
			get { return (IShellViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (IShellViewModel)value; }
		}
	}
}
