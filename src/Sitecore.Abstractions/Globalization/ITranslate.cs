using System;
using System.Xml.Linq;

namespace Sitecore.Globalization
{
    public interface ITranslate
    {
        bool HasPendingReloads { get; }

        void CachePhrase(string key, string phrase, Language language, DictionaryDomain domain);
        XDocument CreateDictionary(Type type);
        DictionaryDomain[] GetCachedDomains();
        Language[] GetCachedLanguages(DictionaryDomain domain);
        void ReloadDomainCache(DictionaryDomain domain);
        void ReloadFromDatabase();
        void RemoveKeyFromCache(string key);
        void RemoveKeyFromCache(string key, Language language);
        void RemoveKeyFromCache(string key, Language language, DictionaryDomain domain);
        void ResetCache();
        void ResetCache(bool removeFileCache);
        string Text(string key);
        string Text(string key, params object[] parameters);
        string TextByDomain(string domain, string key, params object[] parameters);
        string TextByDomain(string domain, TranslateOptions options, string key, params object[] parameters);
        string TextByLanguage(string key, Language language);
        string TextByLanguage(string key, Language language, string defaultValue);
        string TextByLanguage(string key, Language language, string defaultValue, params object[] parameters);
        string TextByLanguage(string domainName, TranslateOptions options, string key, Language language, string defaultValue, params object[] parameters);
    }
}
