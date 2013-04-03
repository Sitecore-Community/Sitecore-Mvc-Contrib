namespace Sitecore.Diagnostics
{
    public interface ILog
    {
        void Audit(object owner, string format, params string[] parameters);
        void Audit(System.Type ownerType, string format, params string[] parameters);
        void Audit(string message, System.Type ownerType);
        void Audit(string message, object owner);
        void Debug(string message);
        void Debug(string message, object owner);
        void Error(string message, System.Exception exception, System.Type ownerType);
        void Error(string message, System.Exception exception, object owner);
        void Error(string message, System.Type ownerType);
        void Error(string message, object owner);
        void Fatal(string message, System.Exception exception, System.Type ownerType);
        void Fatal(string message, System.Exception exception, object owner);
        void Fatal(string message, System.Type ownerType);
        void Fatal(string message, object owner);
        void Info(string message, object owner);
        void SingleError(string message, object owner);
        void SingleFatal(string message, System.Exception exception, System.Type ownerType);
        void SingleFatal(string message, System.Exception exception, object owner);
        void SingleWarn(string message, object owner);
        void Warn(string message, System.Exception exception, object owner);
        void Warn(string message, object owner);
        bool Enabled { get; }
        // Not implemented ... Sitecore.Caching.Cache Singles { get; }
    }
}