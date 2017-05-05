using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace UniCade
{
    public class GlobalKeyboardHook
    {
        public delegate int keyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);
        private static keyboardHookProc callbackDelegate;

        public struct KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int extraInfo;
        }

        //Define keyboard codes
        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;

        public List<Keys> HookedKeys = new List<Keys>();

        IntPtr hhook = IntPtr.Zero;

        public event KeyEventHandler KeyDown;

        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Public constructor for the GlobalKeyboardHook class
        /// </summary>
		public GlobalKeyboardHook()
        {
            Hook();
        }

        /// <summary>
        /// destructor for the GlobalKeyboardHook class
        /// </summary>
		~GlobalKeyboardHook()
        {
            Unhook();
        }

        #region Public Methods

        /// <summary>
        /// Hook the specified keys
        /// </summary>
        public void Hook()
        {
            if (callbackDelegate != null)
            {
                throw new InvalidOperationException("Can't hook more than once");
            }

            IntPtr hInstance = LoadLibrary("User32");
            callbackDelegate = new keyboardHookProc(HookProc);
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, callbackDelegate, hInstance, 0);
            if (hhook == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Unhook the specified keys and set the callbackDelegate to null
        /// </summary>
        public void Unhook()
        {
            if (callbackDelegate == null)
            {
                return;
            }
            callbackDelegate = null;
        }


        public int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;
                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs keyEventArgs = new KeyEventArgs(key);
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, keyEventArgs);
                    }
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, keyEventArgs);
                    }
                    if (keyEventArgs.Handled)
                    {
                        return 1;
                    }
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
        }

        #endregion

        #region DLL Imports

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        #endregion
    }
}
