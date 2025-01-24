using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using HomeEnergyApi.Controllers;
using HomeEnergyApi.Models;

namespace HomeEnergyApi
{
    public class ApplicationFactory : IControllerFactory
    {
        private static HomeRepository repository;

        public object CreateController(ControllerContext context)
        {
            Console.WriteLine("setting up controller...");

            if(repository == null){
                Console.WriteLine("setting up singleton repository");
                repository = new HomeRepository();
            }

            return new HomesController(repository);
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            if (controller is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}