﻿using MaterialDesignThemes.Wpf;
using ReactiveUI;

namespace UI.Wpf.Consoles
{
	public class ArrangeOptionViewModel : BaseViewModel
	{
		private ArrangeOption _arrange;
		private string _description;
		private int _index;
		private PackIconKind _icon;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public ArrangeOptionViewModel()
		{
		}

		public ArrangeOption Arrange
		{
			get => _arrange;
			set => this.RaiseAndSetIfChanged(ref _arrange, value);
		}

		public string Description
		{
			get => _description;
			set => this.RaiseAndSetIfChanged(ref _description, value);
		}

		public int Index
		{
			get => _index;
			set => this.RaiseAndSetIfChanged(ref _index, value);
		}

		public PackIconKind Icon
		{
			get => _icon;
			set => this.RaiseAndSetIfChanged(ref _icon, value);
		}
	}
}