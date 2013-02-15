using System;

using Sitecore.Mvc.Contrib.Sample.Model;
using Website.Interfaces;

namespace Website.Services
{
    public class ConcreteFooService : IFooService
    {
        public SimpleViewModel BusinessLogicCall()
        {
            return new SimpleViewModel
            {
                Message = "Method was executed at: " + DateTime.Now.ToLongTimeString()
            };
        }
    }
}
