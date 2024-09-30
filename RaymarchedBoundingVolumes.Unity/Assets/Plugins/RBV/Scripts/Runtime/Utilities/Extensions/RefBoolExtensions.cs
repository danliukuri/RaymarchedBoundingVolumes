using System;

namespace RBV.Utilities.Extensions
{
    public static class RefBoolExtensions
    {
        public static ref bool IfYesInvoke(this ref bool flag, Action action)
        {
            if (flag)
                action?.Invoke();
            return ref flag;
        }

        public static ref bool IfNotInvoke(this ref bool flag, Action action)
        {
            if (!flag)
                action?.Invoke();
            return ref flag;
        }

        public static ref bool IfYesSet(this ref bool flag, bool value)
        {
            if (flag)
                flag = value;
            return ref flag;
        }

        public static ref bool IfNotSet(this ref bool flag, bool value)
        {
            if (!flag)
                flag = value;
            return ref flag;
        }
    }
}