namespace Sitecore.Mvc.Controllers
{
    public class ControllerRunnerWrapper : IControllerRunner
    {
        private readonly ControllerRunner _controllerRunner;

        public ControllerRunnerWrapper(ControllerRunner controllerRunner)
        {
            _controllerRunner = controllerRunner;
        }

        public string Execute()
        {
            return _controllerRunner.Execute();
        }
    }
}
