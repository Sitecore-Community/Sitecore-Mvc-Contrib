using Sitecore.Mvc.Contrib.Sample.Model;

namespace Website.Interfaces
{
    public interface IFooService
    {
        SimpleViewModel BusinessLogicCall();
    }
}