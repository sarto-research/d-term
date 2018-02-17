﻿using System.Runtime.InteropServices;

namespace Win32Api
{
	public static class Win32Charset
	{
#if ANSI
		public const CharSet BuildCharSet = CharSet.Ansi;
#else
		public const CharSet BuildCharSet = CharSet.Unicode;
#endif
	}
}
