using System;

namespace Utilities.Extensions
{
    public static class SafeInvokeExtensions
    {
        public static void SafeInvoke(this EventHandler eventHandler, object sender, EventArgs eventArgs)
        {
            if (eventHandler != null) eventHandler.Invoke(sender, eventArgs);
        }

        public static void SafeInvoke(this EventHandler eventHandler, object sender)
        {
            if (eventHandler != null) eventHandler.Invoke(sender, EventArgs.Empty);
        }

        public static void SafeInvoke(this Action action)
        {
            if (action != null) action.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> action, T args)
        {
            if (action != null) action.Invoke(args);
        }

        public static void SafeInvoke<T, T1>(this Action<T, T1> action, T args0, T1 args1)
        {
            if (action != null) action.Invoke(args0, args1);
        }

        public static void SafeInvoke<T, T1, T2>(this Action<T, T1, T2> action, T args0, T1 args1, T2 args2)
        {
            if (action != null) action.Invoke(args0, args1, args2);
        }

        public static void SafeInvoke<T>(this EventHandler<T> eventHandler, object sender, T eventArgs)
            where T : EventArgs
        {
            if (eventHandler != null) eventHandler.Invoke(sender, eventArgs);
        }

        public static void SafeInvoke(this Delegate action, params object[] args)
        {
            if (action != null) action.DynamicInvoke(args);
        }
    }
}