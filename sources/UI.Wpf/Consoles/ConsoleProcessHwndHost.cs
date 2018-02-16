﻿using Consoles.Core;
using Shared.Kernel;
using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace UI.Wpf.Consoles
{
	public class ConsoleProcessHwndHost : HwndHost
	{
		private IConsoleProcess _consoleProcess;

		public ConsoleProcessHwndHost(IConsoleProcess consoleProcess)
		{
			_consoleProcess = consoleProcess ?? throw new ArgumentNullException(nameof(consoleProcess), nameof(ConsoleProcessHwndHost));
		}

		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			var childHandle = _consoleProcess.MainWindowHandle;
			IntegrateConsole(childHandle, hwndParent.Handle);
			return new HandleRef(this, childHandle);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			Win32Api.DestroyWindow(hwnd.Handle);
		}

		private void IntegrateConsole(IntPtr childHandle, IntPtr parentHandle)
		{
			Win32Api.SetParent(childHandle, parentHandle);

			var newStyle = (WindowStyles)Win32Api.GetWindowLongPtr(childHandle, WindowLongFlags.GWL_STYLE);

			newStyle &= ~WindowStyles.WS_MAXIMIZEBOX;
			newStyle &= ~WindowStyles.WS_MINIMIZEBOX;
			newStyle &= ~WindowStyles.WS_THICKFRAME;
			newStyle &= ~WindowStyles.WS_DLGFRAME;
			newStyle &= ~WindowStyles.WS_BORDER;
			newStyle &= ~WindowStyles.WS_POPUP;
			newStyle |= WindowStyles.WS_CHILD;

			Win32Api.SetWindowLongPtr(childHandle, WindowLongFlags.GWL_STYLE, new IntPtr((long)newStyle));
		}
	}
}
