namespace Sitecore.Mvc.Contrib.Data.Validators
{
    public interface IControllerValidator
    {
        bool ActionExistsOnController(string controllerName, string actionName);
    }
}