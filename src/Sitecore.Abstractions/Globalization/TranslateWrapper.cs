using System;

namespace Sitecore.Globalization
{
    public class TranslateWrapper : ITranslate
    {
        public bool HasPendingReloads
        {
            get { return Translate.HasPendingReloads; }
        }

        public void CachePhrase(string key, string phrase, Language language, DictionaryDomain domain)
        {
            Translate.CachePhrase(key, phrase, language, domain);
        }

        public System.Xml.Linq.XDocument CreateDictionary(Type type)
        {
            return Translate.CreateDictionary(type);
        }

        public DictionaryDomain[] GetCachedDomains()
        {
            return Translate.GetCachedDomains();
        }

        public Language[] GetCachedLanguages(DictionaryDomain domain)
        {
            return Translate.GetCachedLanguages(domain);
        }

        public void ReloadDomainCache(DictionaryDomain domain)
        {
            Translate.ReloadDomainCache(domain);
        }

        public void ReloadFromDatabase()
        {
            Translate.ReloadFromDatabase();
        }

        public void RemoveKeyFromCache(string key)
        {
            Translate.RemoveKeyFromCache(key);
        }

        public void RemoveKeyFromCache(string key, Language language)
        {
            Translate.RemoveKeyFromCache(key, language);
        }

        public void RemoveKeyFromCache(string key, Language language, DictionaryDomain domain)
        {
            Translate.RemoveKeyFromCache(key, language, domain);
        }

        public void ResetCache()
        {
            Translate.ResetCache();
        }

        public void ResetCache(bool removeFileCache)
        {
            Translate.ResetCache(removeFileCache);
        }

        public string Text(string key)
        {
            return Translate.Text(key);
        }

        public string Text(string key, params object[] parameters)
        {
            return Translate.Text(key, parameters);
        }

        public string TextByDomain(string domain, string key, params object[] parameters)
        {
            return Translate.TextByDomain(domain, key, parameters);
        }

        public string TextByDomain(string domain, TranslateOptions options, string key, params object[] parameters)
        {
            return Translate.TextByDomain(domain, options, key, parameters);
        }

        public string TextByLanguage(string key, Language language)
        {
            return Translate.TextByLanguage(key, language);
        }

        public string TextByLanguage(string key, Language language, string defaultValue)
        {
            return Translate.TextByLanguage(key, language, defaultValue);
        }

        public string TextByLanguage(string key, Language language, string defaultValue, params object[] parameters)
        {
            return Translate.TextByLanguage(key, language, defaultValue, parameters);
        }

        public string TextByLanguage(string domainName, TranslateOptions options, string key, Language language, string defaultValue, params object[] parameters)
        {
            return Translate.TextByLanguage(domainName, options, key, language, defaultValue, parameters);
        }
    }
}