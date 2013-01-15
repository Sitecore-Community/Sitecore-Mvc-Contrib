using System;
using Sitecore.Caching;

namespace Sitecore.Diagnostics
{
    public class LogWrapper : ILog
    {
        public void Audit(object owner, string format, params string[] parameters)
        {
            Log.Audit(owner, format, parameters);
        }

        public void Audit(Type ownerType, string format, params string[] parameters)
        {
            Log.Audit(ownerType, format, parameters);
        }

        public void Audit(string message, Type ownerType)
        {
            Log.Audit(message, ownerType);
        }

        public void Audit(string message, object owner)
        {
            Log.Audit(message, owner);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Debug(string message, object owner)
        {
            Log.Debug(message, owner);
        }

        public void Error(string message, Exception exception, Type ownerType)
        {
            Log.Error(message, exception, ownerType);
        }

        public void Error(string message, Exception exception, object owner)
        {
            Log.Error(message, exception, owner);
        }

        public void Error(string message, Type ownerType)
        {
            Log.Error(message, ownerType);
        }

        public void Error(string message, object owner)
        {
            Log.Error(message, owner);
        }

        public void Fatal(string message, Exception exception, Type ownerType)
        {
            Log.Fatal(message, exception, ownerType);
        }

        public void Fatal(string message, Exception exception, object owner)
        {
            Log.Fatal(message, exception, owner);
        }

        public void Fatal(string message, Type ownerType)
        {
            Log.Fatal(message, ownerType);
        }

        public void Fatal(string message, object owner)
        {
            Log.Fatal(message, owner);
        }

        public void Info(string message, object owner)
        {
            Log.Info(message, owner);
        }

        public void SingleError(string message, object owner)
        {
            Log.SingleError(message, owner);
        }

        public void SingleFatal(string message, Exception exception, Type ownerType)
        {
            Log.SingleFatal(message, exception, ownerType);
        }

        public void SingleFatal(string message, Exception exception, object owner)
        {
            Log.SingleFatal(message, exception, owner);
        }

        public void SingleWarn(string message, object owner)
        {
            Log.SingleWarn(message, owner);
        }

        public void Warn(string message, Exception exception, object owner)
        {
            Log.Warn(message, exception, owner);
        }

        public void Warn(string message, object owner)
        {
            Log.Warn(message, owner);
        }

        public bool Enabled
        {
            get { return Log.Enabled; }
        }

        public Cache Singles
        {
            get
            {
                return Log.Singles;
            }
        }
    }
}