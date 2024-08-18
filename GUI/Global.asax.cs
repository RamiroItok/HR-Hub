using Aplication;
using Aplication.Interfaces;
using Aplication.Services;
using Data.Composite;
using Data.DAO;
using Data.Interfaces;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace GUI
{
    public class Global : HttpApplication
    {
        public static IUnityContainer Container { get; private set; }
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegistrarDependencias();
        }

        private static void RegistrarDependencias()
        {
            Container = new UnityContainer();

            Container.RegisterType<IDigitoVerificadorDAO, DigitoVerificadorDAO>();
            Container.RegisterType<IDigitoVerificadorService, DigitoVerificadorService>();
            Container.RegisterType<IPermisoService, PermisoService>();
            Container.RegisterType<PermisoDAO, PermisoDAO>();
            Container.RegisterType<IUsuarioDAO, UsuarioDAO>();
            Container.RegisterType<IUsuarioService, UsuarioService>();
        }
    }
}