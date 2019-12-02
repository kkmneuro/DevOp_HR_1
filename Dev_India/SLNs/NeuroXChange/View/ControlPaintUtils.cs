using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public static class ControlPaintUtils
    {
        private const int WM_SETREDRAW = 0x000B;
        private const int WmPaint = 0x000F;

        [DllImport("User32.dll")]
        public static extern Int64 SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static void ForcePaint(Control control)
        {
            SendMessage(control.Handle, WmPaint, IntPtr.Zero, IntPtr.Zero);
        }

        public static void Suspend(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public static void Resume(Control control)
        {
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);

            control.Invalidate();
        }

        public static void Update(Control control)
        {
            Resume(control);
            ForcePaint(control);
            control.Refresh();
            Suspend(control);
        }
    }
}
