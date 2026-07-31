using System;
using System.Web.UI;

namespace Capara_Presentacion_Web
{
    public partial class frmAcercaDe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Configuración inicial de la página si fuera necesaria
                Page.Title = "Acerca de Nosotros | Hotel APM Grand";
            }
        }
    }
}