﻿using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using UI.Wpf.Consoles;
using UI.Wpf.Properties;

namespace UI.Wpf.Workspace
{
	/// <summary>
	/// Workspace view model interface.
	/// </summary>
	public interface IWorkspaceViewModel
	{
		string AppTitle { get; set; }
		IConsoleOptionsPanelViewModel ConsoleOptionsPanelViewModel { get; }
		Interaction<IGeneralSettingsViewModel, Unit> OpenGeneralSettingsViewInteraction { get; }
		ReactiveCommand WorkspaceSettingsCommand { get; }
	}

	/// <summary>
	/// App workspace view model implementation.
	/// <seealso cref="IWorkspaceViewModel"/>
	/// </summary>
	public class WorkspaceViewModel : ReactiveObject, IWorkspaceViewModel
	{
		//
		private readonly IConsoleOptionsPanelViewModel _consoleOptionsPanelViewModel;
		private readonly IGeneralSettingsViewModel _generalSettingsViewModel;

		//
		private Interaction<IGeneralSettingsViewModel, Unit> _openGeneralSettingsInteraction;
		private ReactiveCommand _workspaceSettingsCommand;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public WorkspaceViewModel(IConsoleOptionsPanelViewModel consoleOptionsPanelViewModel, IGeneralSettingsViewModel generalSettingsViewModel)
		{
			_consoleOptionsPanelViewModel = consoleOptionsPanelViewModel ?? throw new ArgumentNullException(nameof(consoleOptionsPanelViewModel), nameof(WorkspaceViewModel));
			_generalSettingsViewModel = generalSettingsViewModel ?? throw new ArgumentNullException(nameof(generalSettingsViewModel), nameof(WorkspaceViewModel));

			_openGeneralSettingsInteraction = new Interaction<IGeneralSettingsViewModel, Unit>();

			WorkspaceSettingsCommandSetup();
		}

		/// <summary>
		/// Gets the app title.
		/// </summary>
		public string AppTitle { get; set; } = Resources.AppTitle;

		/// <summary>
		/// Gets the interaction that opens the general settings window.
		/// </summary>
		public Interaction<IGeneralSettingsViewModel, Unit> OpenGeneralSettingsViewInteraction => _openGeneralSettingsInteraction;

		/// <summary>
		/// Gets or sets the console options panel view model.
		/// </summary>
		public IConsoleOptionsPanelViewModel ConsoleOptionsPanelViewModel => _consoleOptionsPanelViewModel;

		/// <summary>
		/// Gets the main workspace settings command instance.
		/// </summary>
		public ReactiveCommand WorkspaceSettingsCommand
		{
			get => _workspaceSettingsCommand;
			set => this.RaiseAndSetIfChanged(ref _workspaceSettingsCommand, value);
		}

		/// <summary>
		/// Setup the main settings command actions and observables.
		/// </summary>
		private void WorkspaceSettingsCommandSetup()
		{
			WorkspaceSettingsCommand = ReactiveCommand.Create(() => OpenGeneralSettingsViewInteraction.Handle(_generalSettingsViewModel).Subscribe(result =>
			{

			}));
		}
	}
}