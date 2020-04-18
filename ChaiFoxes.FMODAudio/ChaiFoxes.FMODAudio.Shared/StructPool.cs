using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ChaiFoxes.FMODAudio
{
	internal class StructPool<T> where T : class
	{
		private readonly Dictionary<ulong, T> _items = new Dictionary<ulong, T>();

		private ulong _currentIndex = 450;

		public IntPtr Add(T item)
		{ 
			_items.Add(_currentIndex, item);

			var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(_currentIndex));
			Marshal.StructureToPtr(_currentIndex, ptr, false);
			_currentIndex += 1;

			return ptr;
		}

		public T Get(IntPtr ptr)
		{
			var index = (ulong)Marshal.PtrToStructure(ptr, typeof(ulong));
			return _items[index];
		}
		
		public void Remove(IntPtr ptr)
		{
			var index = (ulong)Marshal.PtrToStructure(ptr, typeof(ulong));
			_items.Remove(index);
			Marshal.FreeHGlobal(ptr);
		}
	}
}
