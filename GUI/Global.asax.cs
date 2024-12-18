﻿using Aplication;
using Aplication.Interfaces;
using Aplication.Interfaces.Observer;
using Aplication.Services;
using Aplication.Services.Observer;
using Data.Composite;
using Data.DAO;
using Data.Interfaces;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Lifetime;

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

            Container.RegisterType<IBackUpService, BackUpService>();
            Container.RegisterType<BackUpDAO, BackUpDAO>();
            Container.RegisterType<IBitacoraService, BitacoraService>();
            Container.RegisterType<IBitacoraDAO, BitacoraDAO>();
            Container.RegisterType<ICarritoService, CarritoService>();
            Container.RegisterType<ICarritoDAO, CarritoDAO>();
            Container.RegisterType<ICompraService, CompraService>();
            Container.RegisterType<ICompraDAO, CompraDAO>();
            Container.RegisterType<IDigitoVerificadorDAO, DigitoVerificadorDAO>();
            Container.RegisterType<IDigitoVerificadorService, DigitoVerificadorService>();
            Container.RegisterType<IDocumentoService, DocumentoService>();
            Container.RegisterType<IDocumentoDAO, DocumentoDAO>();
            Container.RegisterType<IdiomaService>(new ContainerControlledLifetimeManager());  //SINGLETON
            Container.RegisterType<IEmpresaDAO, EmpresaDAO>();
            Container.RegisterType<IEmpresaService, EmpresaService>();
            Container.RegisterType<IPermisoService, PermisoService>();
            Container.RegisterType<PermisoDAO, PermisoDAO>();
            Container.RegisterType<IProductoService, ProductoService>();
            Container.RegisterType<IProductoDAO, ProductoDAO>();
            Container.RegisterType<IUsuarioDAO, UsuarioDAO>();
            Container.RegisterType<IUsuarioService, UsuarioService>();
        }
    }
}