//-----------------------------------------------------------------------------------------------------------------------
//
// Copyright 2013 Sitecore Corporation A/S
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for          
// the specific language governing permissions and limitations under the License.                                         
// 
//-----------------------------------------------------------------------------------------------------------------------

using System;

namespace Sitecore.Mvc.Contrib.Caching
{
    public sealed class WebCacheAdapter : ICache
    {
        private readonly System.Web.Caching.Cache _cache;

        public WebCacheAdapter()
        {
            if (System.Web.HttpContext.Current != null)
                _cache = System.Web.HttpContext.Current.Cache;
            else
                throw new ArgumentNullException("No HttpContext, unable to use the web cache");
        }

        object ICache.this[string fieldName]
        {
            get { return _cache[fieldName]; }
            set { _cache[fieldName] = value; }
        }
    }
}
