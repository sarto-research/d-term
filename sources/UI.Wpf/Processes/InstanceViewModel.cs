﻿using Processes.Core;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace UI.Wpf.Processes
{
	//
	public interface IInstanceViewModel
	{
		string Name { get; set; }
		bool IsElevated { get; set; }
		bool IsMinimized { get; set; }
		string PicturePath { get; set; }
		int ProcessId { get; }
		IntPtr ProcessMainWindowHandle { get; }
		IObservable<EventPattern<EventArgs>> ProcessTerminated { get; }
		void KillProcess();
	}

	//
	public class InstanceViewModel : ReactiveObject, IInstanceViewModel
	{
		private readonly IProcess _process;
		private readonly IObservable<EventPattern<EventArgs>> _terminated;

		private string _name;
		private bool _isElevated;
		private bool _isMinimized;
		private string _picturePath;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public InstanceViewModel(IProcess process)
		{
			_process = process ?? throw new ArgumentNullException(nameof(process), nameof(InstanceViewModel));

			_terminated = Observable.FromEventPattern<EventHandler, EventArgs>(
				handler => _process.Exited += handler,
				handler => _process.Exited -= handler);
		}

		public string Name
		{
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}

		public bool IsElevated
		{
			get => _isElevated;
			set => this.RaiseAndSetIfChanged(ref _isElevated, value);
		}

		public bool IsMinimized
		{
			get => _isMinimized;
			set => this.RaiseAndSetIfChanged(ref _isMinimized, value);
		}

		public string PicturePath
		{
			get => _picturePath;
			set => this.RaiseAndSetIfChanged(ref _picturePath, value);
		}

		public int ProcessId => _process.Id;

		public IntPtr ProcessMainWindowHandle => Win32Api.GetProcessWindow(ProcessId);

		public IObservable<EventPattern<EventArgs>> ProcessTerminated => _terminated;

		public void KillProcess() => _process.Kill();
	}
}
