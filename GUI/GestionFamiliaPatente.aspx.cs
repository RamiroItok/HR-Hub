using Aplication.Interfaces;
using Models.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace GUI
{
    public partial class GestionFamiliaPatente : Page
    {
        private Familia _seleccionFamilia;
        private List<Familia> _familiaComparacion;
        private readonly IPermisoService _iPermiso;

        public GestionFamiliaPatente()
        {
            _iPermiso = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFamilias();
                CargarPatentes();
            }
        }

        protected void btn_Listar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmb_Familia1.SelectedValue))
                {
                    lblMessage.Text = "Por favor, selecciona una opción válida.";
                    return;
                }

                int familiaId;
                if (int.TryParse(cmb_Familia1.SelectedValue, out familiaId))
                {
                    _seleccionFamilia = new Familia
                    {
                        Id = familiaId,
                        Nombre = cmb_Familia1.SelectedItem.Text
                    };

                    CargarTreeFamilia(true);
                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Error al cargar el árbol de familia.";
            }
        }

        protected void btn_AgregarPatente_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seleccionFamilia != null)
                {
                    var hola = cmb_Patentes;
                    Patente patente = null;//_iPermiso.ObtenerPatentePorId(Convert.ToInt32(cmb_Patentes.SelectedValue));
                    if (patente != null)
                    {
                        bool existeComponente = _iPermiso.ExisteComponente(_seleccionFamilia, patente.Id);

                        if (existeComponente)
                        {
                            lblMessage.Text = "La patente ya está cargada en esta familia.";
                        }
                        else
                        {
                            _seleccionFamilia.AgregarHijo(patente);
                            CargarTreeFamilia(false);
                        }
                    }
                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Error al cargar la patente.";
            }
        }

        protected void btn_AgregarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seleccionFamilia != null)
                {
                    ValidarArbol();

                    var familiaId = int.Parse(cmb_Familia1.SelectedValue);
                    Familia familia = new Familia()
                    {
                        Id = familiaId,
                        Nombre = cmb_Familia2.SelectedItem.Text
                    };
                    
                    Componente _familia = _iPermiso.ObtenerFamiliaArbol(familia.Id, new Familia(), null);

                    foreach (var item in _familia.Hijos)
                    {
                        familia.AgregarHijo(item);
                    }

                    if (familia != null)
                    {
                        bool existeComponente = _iPermiso.ExisteComponente(_seleccionFamilia, familia.Id);

                        if (existeComponente)
                        {
                            lblMessage.Text = "La familia ya está cargada en esta familia.";
                        }
                        else
                        {
                            _seleccionFamilia.AgregarHijo(familia);
                            CargarTreeFamilia(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seleccionFamilia != null)
                {
                    _iPermiso.GuardarFamiliaCreada(_seleccionFamilia);

                    treePatenteFamilia.Nodes.Clear();
                    lblMessage.Text = "Familia guardada con éxito.";
                    CargarFamilias();
                    CargarPatentes();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btn_EliminarPatente_Click(object sender, EventArgs e)
        {
            try
            {
                if (treePatenteFamilia.SelectedNode != null)
                {
                    IList<Componente> familia = _seleccionFamilia.Hijos;

                    foreach (var item in familia)
                    {
                        if (treePatenteFamilia.SelectedNode.Text == item.Nombre)
                        {
                            _seleccionFamilia.BorrarHijo(item);
                        }
                    }

                    CargarTreeFamilia(false);
                    lblMessage.Text = "Patente eliminada. Recuerde guardar los cambios.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            cmb_Familia1.SelectedIndex = -1;
            cmb_Familia2.SelectedIndex = -1;
            cmb_Patentes.SelectedIndex = -1;
            treePatenteFamilia.Nodes.Clear();
            lblMessage.Text = string.Empty;
        }

        private void CargarFamilias()
        {
            cmb_Familia1.DataSource = _iPermiso.ObtenerFamilias();
            cmb_Familia1.DataTextField = "Nombre";
            cmb_Familia1.DataValueField = "Id";
            cmb_Familia1.DataBind();

            cmb_Familia2.DataSource = _iPermiso.ObtenerFamilias();
            cmb_Familia2.DataTextField = "Nombre";
            cmb_Familia2.DataValueField = "Id";
            cmb_Familia2.DataBind();
        }

        private void CargarPatentes()
        {
            cmb_Patentes.DataSource = _iPermiso.ObtenerPatentes();
            cmb_Patentes.DataTextField = "Nombre";
            cmb_Patentes.DataValueField = "Id";
            cmb_Patentes.DataBind();
        }

        private void CargarTreeFamilia(bool familia)
        {
            try
            {
                if (_seleccionFamilia == null) return;

                Componente _familia = new Familia();

                if (familia)
                {
                    _familia = _iPermiso.ObtenerFamiliaArbol(_seleccionFamilia.Id, new Familia(), null);

                    foreach (var i in _familia.Hijos) _seleccionFamilia.AgregarHijo(i);
                }
                else
                {
                    _familia = _seleccionFamilia;
                }

                treePatenteFamilia.Nodes.Clear();
                TreeNode root = new TreeNode(_seleccionFamilia.Nombre);
                root.Value = _seleccionFamilia.Id.ToString();
                treePatenteFamilia.Nodes.Add(root);

                foreach (var item in _familia.Hijos)
                {
                    MostrarEnTreePatenteFamilia(root, item);
                }
                treePatenteFamilia.ExpandAll();
            }
            catch (Exception)
            {
                lblMessage.Text = "Error al cargar el árbol.";
            }
        }

        private void ValidarArbol()
        {
            try
            {
                _familiaComparacion = new List<Familia>();

                Familia familia = _seleccionFamilia;
                Familia familiaSeleccionada = new Familia()
                {
                    Id = int.Parse(cmb_Familia2.SelectedValue),
                    Nombre = cmb_Familia2.SelectedItem.Text
                };

                _familiaComparacion.AddRange(_iPermiso.GetFamiliasValidacion(familia.Id));
                List<Familia> familiaCopia = new List<Familia>(_familiaComparacion);

                familiaCopia.ForEach(f => FillFamilia(f.Id));
                _familiaComparacion = LimpiarLista(_familiaComparacion);

                ValidarFamilias(_familiaComparacion, familiaSeleccionada);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillFamilia(int familiaId)
        {
            try
            {
                IList<Familia> listaFlia = _iPermiso.GetFamiliasValidacion(familiaId);

                if (listaFlia.Count > 0)
                {
                    _familiaComparacion.AddRange(listaFlia);

                    foreach (Familia familia in listaFlia)
                    {
                        FillFamilia(familia.Id);
                    }
                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Error al cargar el árbol.";
            }
        }

        private List<Familia> LimpiarLista(List<Familia> familias)
        {
            try
            {
                List<Familia> listaFlia = new List<Familia>();

                foreach (Familia familia in familias)
                {
                    if (!listaFlia.Where(f => f.Id == familia.Id).Any())
                    {
                        listaFlia.Add(familia);
                    }
                }

                return listaFlia;
            }
            catch (Exception)
            {
                throw new Exception("Error al cargar el árbol.");
            }
        }

        private void ValidarFamilias(List<Familia> familias, Familia familiaAgregada)
        {
            try
            {
                foreach (Familia familia in familias)
                {
                    if (familia.Id == familiaAgregada.Id) throw new Exception("Recursividad detectada.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MostrarEnTreePatenteFamilia(TreeNode treeNode, Componente componente)
        {
            try
            {
                TreeNode nodo = new TreeNode(componente.Nombre);
                nodo.Value = componente.Id.ToString();
                treeNode.ChildNodes.Add(nodo);

                if (componente.Hijos != null)
                {
                    foreach (var item in componente.Hijos)
                    {
                        MostrarEnTreePatenteFamilia(nodo, item);
                    }
                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Error al cargar el árbol.";
            }
        }
    }
}