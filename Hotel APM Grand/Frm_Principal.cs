using Entidades.Cuentas;
using Logica.Cuentas;
using Logica.Prestamos;
using Logica.Transacciones;
using Logica.Usuarios;
using Presentacion.cuenta;
using Presentacion.Inicio;
using Presentacion.Prestamos;
using Presentacion.Reportes;
using Presentacion.Sistema;
using Presentacion.Tarjetas;
using Presentacion.Transacciones;
using Presentacion.usuarios;
using Presentacion.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tu.Namespace.Formularios;

namespace Presentacion
{
    public partial class Frm_Principal : Form
    {
        private Timer timerHora;
        private readonly CuentaLN cuentaLogica = new CuentaLN();

        // ✅ Agregar estas instancias
        private readonly ClienteLN clienteLogica = new ClienteLN();
        private readonly PrestamoLN prestamoLogica = new PrestamoLN();
        private readonly TransaccionesLN transaccionLogica = new TransaccionesLN();

        private Form formularioActual = null;
        private Dictionary<Panel, bool> estadoAcordeones = new Dictionary<Panel, bool>();
        private Dictionary<Panel, List<Panel>> subMenus = new Dictionary<Panel, List<Panel>>();

        public Frm_Principal()
        {
            InitializeComponent();
            InicializarTimer();
        }

        private void MostrarFormularioEnPanel(Form formulario)
        {
            if (formularioActual != null)
            {
                formularioActual.Close();
                formularioActual.Dispose();
            }

            this.panelPrincipal.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(248, 249, 250);

            this.panelPrincipal.Controls.Add(formulario);
            formulario.Show();

            formularioActual = formulario;
        }

        private void RestaurarDashboard()
        {
            if (formularioActual != null)
            {
                formularioActual.Close();
                formularioActual.Dispose();
                formularioActual = null;
            }

            CrearDashboard();
        }

        private void BtnContainer_MouseEnter(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null && p.BackColor != Color.FromArgb(54, 120, 255))
                p.BackColor = Color.FromArgb(55, 65, 81);
        }

        private void BtnContainer_MouseLeave(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null && p.BackColor != Color.FromArgb(54, 120, 255))
                p.BackColor = Color.Transparent;
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Panel btnContainer = control as Panel ?? control.Parent as Panel;

            if (btnContainer == null || btnContainer.Tag == null)
                return;

            if (subMenus.ContainsKey(btnContainer))
                return;

            int index = Convert.ToInt32(btnContainer.Tag);

            foreach (Control ctrl in panelLateral.Controls)
            {
                if (ctrl is Panel && ctrl.Tag != null && ctrl.Name.StartsWith("btnMenu"))
                {
                    ctrl.BackColor = Color.Transparent;
                }
            }
            btnContainer.BackColor = Color.FromArgb(54, 120, 255);

            switch (index)
            {
                case 0:
                    RestaurarDashboard();
                    break;

                case 1:
                    try
                    {
                        frmAdminCliente frm = new frmAdminCliente();
                        MostrarFormularioEnPanel(frm);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 2:
                    try
                    {
                        Frm_GestionCuentas fr = new Frm_GestionCuentas();
                        MostrarFormularioEnPanel(fr);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Cuentas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 3:
                    try
                    {
                        frmAdminBeneficiario beni = new frmAdminBeneficiario();
                        MostrarFormularioEnPanel(beni);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Beneficiarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 6:
                    try
                    {
                        frmAdminEmpleado form = new frmAdminEmpleado();
                        MostrarFormularioEnPanel(form);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 8:
                    try
                    {
                        frmMenuReportes frm = new frmMenuReportes();
                        MostrarFormularioEnPanel(frm);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Tarjetas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 9:
                    try
                    {
                        FormAuditoria Au = new FormAuditoria();
                        MostrarFormularioEnPanel(Au);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al abrir Auditoría: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void ConfigurarAcordeones()
        {
            ConfigurarAcordeonParaBoton(btnMenu4, new[]
            {
                new { Index = 40, Texto = "Todas las Transacciones" },
                new { Index = 41, Texto = "Log de Transacciones" },
                new { Index = 42, Texto = "Tipos de Transacción" }
            });

            ConfigurarAcordeonParaBoton(btnMenu5, new[]
            {
                new { Index = 50, Texto = "Gestión de Préstamos" },
                new { Index = 51, Texto = "Tipos de Préstamo" },
                new { Index = 52, Texto = "Pagos de Préstamos" }
            });

            ConfigurarAcordeonParaBoton(btnMenu7, new[]
            {
                new { Index = 70, Texto = "Lista de Sucursales" },
                new { Index = 71, Texto = "Tipos de Cuenta" }
            });

            ConfigurarAcordeonParaBoton(btnMenu10, new[]
            {
                new { Index = 100, Texto = "Configuración Sistema" },
                new { Index = 101, Texto = "Servicios de Pago" },
                new { Index = 102, Texto = "Tablas de Grafos" }
            });

            ConfigurarAcordeonParaBoton(btnMenu11, new[]
           {
                new { Index = 103, Texto = "Lista de Tarjetas" },
                new { Index = 104, Texto = "Tipos de Tarjetas" },
                new { Index = 105, Texto = "Cargos Tarjeta" },


            });
        }

        private void ConfigurarAcordeonParaBoton(Panel btnPanel, dynamic[] subItems)
        {
            Label lblFlecha = new Label
            {
                Text = "▼",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Name = "flecha",
                AutoSize = false,
                Size = new Size(20, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(btnPanel.Width - 35, (btnPanel.Height - 20) / 2)
            };

            btnPanel.Controls.Add(lblFlecha);
            lblFlecha.BringToFront();

            btnPanel.Click -= BtnMenu_Click;
            btnPanel.Click += BtnAcordeon_Click;

            foreach (Control ctrl in btnPanel.Controls)
            {
                if (ctrl is Label && ctrl.Name != "flecha")
                {
                    ctrl.Click -= BtnMenu_Click;
                    ctrl.Click += (s, e) => BtnAcordeon_Click(btnPanel, e);
                }
            }

            List<Panel> subMenuItems = new List<Panel>();
            int baseYPosition = btnPanel.Location.Y + btnPanel.Height;

            for (int i = 0; i < subItems.Length; i++)
            {
                Panel subItem = new Panel
                {
                    Name = $"subMenu_{btnPanel.Tag}_{i}",
                    Size = new Size(210, 35),
                    Location = new Point(20, baseYPosition + (i * 35)),
                    BackColor = Color.FromArgb(25, 35, 45),
                    Tag = subItems[i].Index,
                    Cursor = Cursors.Hand,
                    Visible = false
                };

                Label lblSubTexto = new Label
                {
                    Text = "  • " + subItems[i].Texto,
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(25, 8),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(200, 200, 200),
                    BackColor = Color.Transparent
                };

                subItem.Controls.Add(lblSubTexto);

                subItem.MouseEnter += (s, e) => subItem.BackColor = Color.FromArgb(45, 55, 65);
                subItem.MouseLeave += (s, e) => subItem.BackColor = Color.FromArgb(25, 35, 45);
                subItem.Click += SubMenu_Click;
                lblSubTexto.Click += (s, e) => SubMenu_Click(subItem, e);

                panelLateral.Controls.Add(subItem);
                subItem.BringToFront();
                subMenuItems.Add(subItem);
            }

            subMenus[btnPanel] = subMenuItems;
            estadoAcordeones[btnPanel] = false;
        }

        private void BtnAcordeon_Click(object sender, EventArgs e)
        {
            Panel btnContainer = sender as Panel ?? (sender as Control)?.Parent as Panel;
            if (btnContainer == null || !subMenus.ContainsKey(btnContainer)) return;

            bool estaAbierto = estadoAcordeones[btnContainer];
            var subItems = subMenus[btnContainer];

            Label flecha = btnContainer.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "flecha");

            foreach (Control ctrl in panelLateral.Controls)
            {
                if (ctrl is Panel && ctrl.Name.StartsWith("btnMenu"))
                {
                    ctrl.BackColor = Color.Transparent;
                }
            }

            btnContainer.BackColor = Color.FromArgb(54, 120, 255);

            if (estaAbierto)
            {
                foreach (var subItem in subItems)
                {
                    subItem.Visible = false;
                }
                if (flecha != null) flecha.Text = "▼";
                estadoAcordeones[btnContainer] = false;
            }
            else
            {
                foreach (var kvp in estadoAcordeones.Keys.ToList())
                {
                    if (kvp != btnContainer && estadoAcordeones[kvp])
                    {
                        foreach (var subItem in subMenus[kvp])
                        {
                            subItem.Visible = false;
                        }
                        Label otraFlecha = kvp.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "flecha");
                        if (otraFlecha != null) otraFlecha.Text = "▼";
                        estadoAcordeones[kvp] = false;
                    }
                }

                foreach (var subItem in subItems)
                {
                    subItem.Visible = true;
                }
                if (flecha != null) flecha.Text = "▲";
                estadoAcordeones[btnContainer] = true;
            }

            ReorganizarMenuConAcordeon();
        }

        private void ReorganizarMenuConAcordeon()
        {
            var posiciones = new Dictionary<string, int>
            {
                { "btnMenu0", 100 },
                { "btnMenu1", 150 },
                { "btnMenu2", 200 },
                { "btnMenu3", 250 },
                { "btnMenu4", 300 },
                { "btnMenu5", 350 },
                { "btnMenu6", 400 },
                { "btnMenu7", 450 },
                { "btnMenu8", 500 },
                { "btnMenu9", 550 },
                { "btnMenu10", 600 },
                  { "btnMenu11", 650 },
                { "btnMenu12", 700 }

            };

            foreach (var kvp in posiciones)
            {
                Panel btnPanel = panelLateral.Controls.Find(kvp.Key, false).FirstOrDefault() as Panel;
                if (btnPanel != null)
                {
                    int yPos = kvp.Value;

                    foreach (var otroKvp in posiciones)
                    {
                        if (otroKvp.Value < kvp.Value)
                        {
                            Panel otroPanel = panelLateral.Controls.Find(otroKvp.Key, false).FirstOrDefault() as Panel;
                            if (otroPanel != null && subMenus.ContainsKey(otroPanel) && estadoAcordeones[otroPanel])
                            {
                                yPos += subMenus[otroPanel].Count * 35;
                            }
                        }
                    }

                    btnPanel.Location = new Point(20, yPos);

                    if (subMenus.ContainsKey(btnPanel) && estadoAcordeones[btnPanel])
                    {
                        int subYPos = yPos + btnPanel.Height;
                        foreach (var subItem in subMenus[btnPanel])
                        {
                            subItem.Location = new Point(20, subYPos);
                            subYPos += 35;
                        }
                    }
                }
            }
        }

        private void SubMenu_Click(object sender, EventArgs e)
        {
            Panel subItem = sender as Panel ?? (sender as Control)?.Parent as Panel;
            if (subItem == null || subItem.Tag == null) return;

            int index = Convert.ToInt32(subItem.Tag);

            foreach (Control ctrl in panelLateral.Controls)
            {
                if (ctrl is Panel && ctrl.Name.StartsWith("subMenu"))
                {
                    ctrl.BackColor = Color.FromArgb(25, 35, 45);
                }
            }

            subItem.BackColor = Color.FromArgb(54, 120, 255);

            EjecutarAccionSubMenu(index);
        }

        private void EjecutarAccionSubMenu(int index)
        {
            try
            {
                switch (index)
                {
                    case 40:
                        FrmAdminTransacciones frm40 = new FrmAdminTransacciones();
                        MostrarFormularioEnPanel(frm40);
                        break;
                    case 41:
                        FrmLogTransaccionesLista frm41 = new FrmLogTransaccionesLista();
                        MostrarFormularioEnPanel(frm41);
                        break;
                    case 42:
                        FrmAdmin_TipoTransaccion frm42 = new FrmAdmin_TipoTransaccion();
                        MostrarFormularioEnPanel(frm42);
                        break;

                    case 50:
                        FrmListarPrestamos frm50 = new FrmListarPrestamos();
                        MostrarFormularioEnPanel(frm50);
                        break;
                    case 51:
                        FrmTiposPrestamo frm51 = new FrmTiposPrestamo();
                        MostrarFormularioEnPanel(frm51);
                        break;
                    case 52:
                        Frm_AdminPagoPrestamo frm = new Frm_AdminPagoPrestamo();
                        MostrarFormularioEnPanel(frm);
                        break;

                    case 70:
                        FrmAdmin_Sucursal frm70 = new FrmAdmin_Sucursal();
                        MostrarFormularioEnPanel(frm70);
                        break;
                    case 71:
                        Frm_TipoCuenta frm71 = new Frm_TipoCuenta();
                        MostrarFormularioEnPanel(frm71);
                        break;

                    case 100:
                        FrmConfiguracionSistema frm72 = new FrmConfiguracionSistema();
                        MostrarFormularioEnPanel(frm72);
                        break;
                    case 101:
                        FrmAdminServiciosPago frm111 = new FrmAdminServiciosPago();
                        MostrarFormularioEnPanel(frm111);
                        break;
                    case 102:
                        MessageBox.Show("Tablas de Grafos en desarrollo...", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case 103:
                        FrmTarjetaLista frm113 = new FrmTarjetaLista();
                        MostrarFormularioEnPanel(frm113);
                        break;
                    case 104:
                        FrmTipoTarjetaLista frm114 = new FrmTipoTarjetaLista();
                        MostrarFormularioEnPanel(frm114);
                        break;

                    case 105:
                        FrmAdminCargos frm115 = new FrmAdminCargos();
                        MostrarFormularioEnPanel(frm115);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CrearDashboard()
        {
            this.panelPrincipal.Controls.Clear();

            // ✅ PANEL DE TÍTULO CORREGIDO
            Panel tituloPanel = new Panel
            {
                Location = new Point(20, 0),
                Size = new Size(800, 60),
                BackColor = Color.Transparent
            };

            Label iconoDashboard = new Label
            {
                Text = "🏦",  // Icono de banco
                Font = new Font("Segoe UI Emoji", 28),  // ⬅️ Usar fuente Emoji
                Location = new Point(5, 8),
                Size = new Size(45, 45),
                TextAlign = ContentAlignment.MiddleCenter  // ⬅️ Centrar
            };

            Label titulo = new Label
            {
                Text = "Sistema Bancario",  // ⬅️ Tu título personalizado
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(55, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(31, 41, 55)  // Color oscuro
            };

            tituloPanel.Controls.Add(iconoDashboard);
            tituloPanel.Controls.Add(titulo);

            // Panel de métricas
            Panel panelMetricas = new Panel
            {
                Location = new Point(20, 70),  // Bajado un poco
                Size = new Size(1000, 150),
                BackColor = Color.Transparent
            };

            CrearTarjetasMetricas(panelMetricas);

            Panel panelAcciones = new Panel
            {
                Location = new Point(20, 220),  // Ajustado
                Size = new Size(1000, 200),
                BackColor = Color.Transparent
            };

            CrearAccionesRapidas(panelAcciones);

            Panel panelTransacciones = new Panel
            {
                Location = new Point(20, 460),
                Size = new Size(600, 300),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            CrearTransaccionesRecientes(panelTransacciones);

            Panel panelActividad = new Panel
            {
                Location = new Point(620, 460),
                Size = new Size(400, 300),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Panel panelExtra = new Panel
            {
                Location = new Point(20, 770),
                Size = new Size(1000, 600),  // ⬅️ Aumentado de 550 a 600
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            CrearContenidoExtra(panelExtra);

            this.panelPrincipal.Controls.Add(tituloPanel);
            this.panelPrincipal.Controls.Add(panelMetricas);
            this.panelPrincipal.Controls.Add(panelAcciones);
            this.panelPrincipal.Controls.Add(panelTransacciones);
            this.panelPrincipal.Controls.Add(panelActividad);
            this.panelPrincipal.Controls.Add(panelExtra);


            CrearActividadSistema(panelActividad);

            this.panelPrincipal.Controls.Add(tituloPanel);
            this.panelPrincipal.Controls.Add(panelMetricas);
            this.panelPrincipal.Controls.Add(panelAcciones);
            this.panelPrincipal.Controls.Add(panelTransacciones);
            this.panelPrincipal.Controls.Add(panelActividad);
        }
        private void CrearTarjetasMetricas(Panel contenedor)
        {
            try
            {
                int totalClientes = ObtenerTotalClientes();
                int cuentasActivas = ObtenerCuentasActivas();
                int transaccionesHoy = ObtenerTransaccionesHoy();
                int prestamosAprobados = ObtenerPrestamosAprobados();

                string porcentajeClientes = CalcularPorcentajeClientes();
                string porcentajeCuentas = CalcularPorcentajeCuentas();
                string porcentajeTransacciones = CalcularPorcentajeTransacciones();
                string porcentajePrestamos = CalcularPorcentajePrestamos();

                var metricas = new[]
                {
            new { Titulo = "Total Clientes", Valor = totalClientes.ToString(), Porcentaje = porcentajeClientes, Color = Color.FromArgb(54, 120, 255), Icono = "👥" },
            new { Titulo = "Cuentas Activas", Valor = cuentasActivas.ToString(), Porcentaje = porcentajeCuentas, Color = Color.FromArgb(34, 197, 94), Icono = "💳" },
            new { Titulo = "Transacciones Hoy", Valor = transaccionesHoy.ToString(), Porcentaje = porcentajeTransacciones, Color = Color.FromArgb(147, 51, 234), Icono = "💰" },
            new { Titulo = "Préstamos Aprobados", Valor = prestamosAprobados.ToString(), Porcentaje = porcentajePrestamos, Color = Color.FromArgb(255, 102, 51), Icono = "🏦" }
        };

                for (int i = 0; i < metricas.Length; i++)
                {
                    Panel tarjeta = new Panel
                    {
                        Size = new Size(220, 120),
                        Location = new Point(i * 240, 0),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand
                    };

                    // ✅ ICONO CORREGIDO
                    Label icono = new Label
                    {
                        Text = metricas[i].Icono,
                        Font = new Font("Segoe UI Emoji", 24),  // ⬅️ Fuente Emoji y tamaño mayor
                        Location = new Point(165, 12),
                        Size = new Size(40, 40),
                        ForeColor = metricas[i].Color,
                        TextAlign = ContentAlignment.MiddleCenter  // ⬅️ Centrado
                    };

                    Label lblTitulo = new Label
                    {
                        Text = metricas[i].Titulo,
                        Location = new Point(20, 20),
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 10),
                        AutoSize = true
                    };

                    Label lblValor = new Label
                    {
                        Text = metricas[i].Valor,
                        Location = new Point(20, 45),
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 20, FontStyle.Bold),
                        AutoSize = true
                    };

                    Color colorPorcentaje = metricas[i].Porcentaje.StartsWith("+") ?
                        Color.FromArgb(34, 197, 94) :
                        Color.FromArgb(239, 68, 68);

                    Label lblPorcentaje = new Label
                    {
                        Text = metricas[i].Porcentaje,
                        Location = new Point(20, 85),
                        ForeColor = colorPorcentaje,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        AutoSize = true
                    };

                    tarjeta.Controls.Add(icono);
                    tarjeta.Controls.Add(lblTitulo);
                    tarjeta.Controls.Add(lblValor);
                    tarjeta.Controls.Add(lblPorcentaje);
                    contenedor.Controls.Add(tarjeta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar métricas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Métodos para Obtener Datos Reales

        private int ObtenerTotalClientes()
        {
            try
            {
                var clientes = clienteLogica.ShowCliente("");
                return clientes?.Count() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private int ObtenerCuentasActivas()
        {
            try
            {
                var cuentas = cuentaLogica.ShowCuenta("");
                return cuentas?.Count(c => c.Estado == "Activa") ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private int ObtenerTransaccionesHoy()
        {
            try
            {
                var transacciones = transaccionLogica.ObtenerTodasLasTransacciones();
                return transacciones?.Count(t => t.FechaTransaccion.Date == DateTime.Now.Date) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private int ObtenerPrestamosAprobados()
        {
            try
            {
                int cantidad = prestamoLogica.ObtenerCantidadPrestamosAprobados();

                // DEBUG temporal - Eliminar después
                System.Diagnostics.Debug.WriteLine($"Préstamos aprobados: {cantidad}");

                return cantidad;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        // 📈 CALCULAR PORCENTAJES AUTOMÁTICOS
        private string CalcularPorcentajeClientes()
        {
            try
            {
                var clientes = clienteLogica.ShowCliente("");
                if (clientes == null) return "0%";

                var clientesMesAnterior = clientes.Count(c => c.FechaRegistro < DateTime.Now.AddMonths(-1));
                var clientesActuales = clientes.Count();

                if (clientesMesAnterior == 0) return "+100%";

                decimal porcentaje = ((decimal)(clientesActuales - clientesMesAnterior) / clientesMesAnterior) * 100;
                return porcentaje >= 0 ? $"+{porcentaje:F0}%" : $"{porcentaje:F0}%";
            }
            catch
            {
                return "0%";
            }
        }

        private string CalcularPorcentajeCuentas()
        {
            try
            {
                var cuentas = cuentaLogica.ShowCuenta("");
                if (cuentas == null) return "0%";

                var cuentasMesAnterior = cuentas.Count(c => c.FechaApertura < DateTime.Now.AddMonths(-1) && c.Estado == "Activa");
                var cuentasActuales = cuentas.Count(c => c.Estado == "Activa");

                if (cuentasMesAnterior == 0) return "+100%";

                decimal porcentaje = ((decimal)(cuentasActuales - cuentasMesAnterior) / cuentasMesAnterior) * 100;
                return porcentaje >= 0 ? $"+{porcentaje:F0}%" : $"{porcentaje:F0}%";
            }
            catch
            {
                return "0%";
            }
        }

        private string CalcularPorcentajeTransacciones()
        {
            try
            {
                var transacciones = transaccionLogica.ObtenerTodasLasTransacciones();
                if (transacciones == null) return "0%";

                var transaccionesHoy = transacciones.Count(t => t.FechaTransaccion.Date == DateTime.Now.Date);
                var transaccionesAyer = transacciones.Count(t => t.FechaTransaccion.Date == DateTime.Now.AddDays(-1).Date);

                if (transaccionesAyer == 0) return transaccionesHoy > 0 ? "+100%" : "0%";

                decimal porcentaje = ((decimal)(transaccionesHoy - transaccionesAyer) / transaccionesAyer) * 100;
                return porcentaje >= 0 ? $"+{porcentaje:F0}%" : $"{porcentaje:F0}%";
            }
            catch
            {
                return "0%";
            }
        }

        private string CalcularPorcentajePrestamos()
        {
            try
            {
                // Obtener todos los préstamos
                var prestamos = prestamoLogica.ListarPrestamosFiltro(null, null, null, null, null);

                if (prestamos == null || !prestamos.Any())
                {
                    System.Diagnostics.Debug.WriteLine("❌ No hay préstamos");
                    return "0%";
                }

                System.Diagnostics.Debug.WriteLine($"📊 Total préstamos: {prestamos.Count}");

                var fechaLimite = DateTime.Now.AddMonths(-1);
                int actual = 0;
                int anterior = 0;

                foreach (var p in prestamos)
                {
                    try
                    {
                        // Usar reflexión para acceder a las propiedades del objeto dinámico
                        var tipoPrestamo = p.GetType();

                        var estadoProp = tipoPrestamo.GetProperty("Estado");
                        var fechaProp = tipoPrestamo.GetProperty("FechaSolicitud");

                        if (estadoProp == null || fechaProp == null)
                        {
                            System.Diagnostics.Debug.WriteLine("⚠️ Propiedades no encontradas");
                            continue;
                        }

                        string estado = estadoProp.GetValue(p, null)?.ToString()?.Trim() ?? "";
                        DateTime fecha = (DateTime)(fechaProp.GetValue(p, null) ?? DateTime.Now);

                        System.Diagnostics.Debug.WriteLine($"   Préstamo: Estado='{estado}', Fecha={fecha:yyyy-MM-dd}");

                        bool esAprobado = estado.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) ||
                                         estado.Equals("Activo", StringComparison.OrdinalIgnoreCase) ||
                                         estado.Equals("Desembolsado", StringComparison.OrdinalIgnoreCase);

                        if (esAprobado)
                        {
                            actual++;

                            if (fecha < fechaLimite)
                            {
                                anterior++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Error procesando préstamo: {ex.Message}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"✅ Actual: {actual}, Anterior: {anterior}");

                if (anterior == 0)
                {
                    string resultado = actual > 0 ? "+100%" : "0%";
                    System.Diagnostics.Debug.WriteLine($"🎯 Porcentaje calculado: {resultado}");
                    return resultado;
                }

                decimal porc = ((decimal)(actual - anterior) / anterior) * 100;
                string porcentajeFinal = porc >= 0 ? $"+{porc:F0}%" : $"{porc:F0}%";

                System.Diagnostics.Debug.WriteLine($"🎯 Porcentaje calculado: {porcentajeFinal}");
                return porcentajeFinal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error en CalcularPorcentajePrestamos: {ex.Message}");
                return "0%";
            }
        }
        #endregion
        private void CrearAccionesRapidas(Panel contenedor)
        {
            Panel tituloPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(500, 40),
                BackColor = Color.Transparent
            };

            Label iconoAcciones = new Label
            {
                Text = "⚡",
                Font = new Font("Segoe UI Emoji", 18),  // ⬅️ Fuente Emoji
                Location = new Point(0, 5),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titulo = new Label
            {
                Text = "Acciones Rápidas",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(35, 8),
                AutoSize = true
            };

            tituloPanel.Controls.Add(iconoAcciones);
            tituloPanel.Controls.Add(titulo);

            var acciones = new[]
            {
        new { Texto = "Nuevo Cliente", Color = Color.FromArgb(54, 120, 255), Icono = "👤" },
        new { Texto = "Nueva Cuenta", Color = Color.FromArgb(34, 197, 94), Icono = "💳" },
        new { Texto = "Procesar Préstamo", Color = Color.FromArgb(147, 51, 234), Icono = "🏦" },
        new { Texto = "Ver Reportes", Color = Color.FromArgb(255, 102, 51), Icono = "📊" }
    };

            for (int i = 0; i < acciones.Length; i++)
            {
                Panel btnAccion = new Panel
                {
                    Size = new Size(220, 70),
                    Location = new Point((i % 2) * 240, 45 + (i / 2) * 80),
                    BackColor = acciones[i].Color,
                    Tag = i,
                    Cursor = Cursors.Hand
                };

                // ✅ ICONO CORREGIDO
                Label icono = new Label
                {
                    Text = acciones[i].Icono,
                    Font = new Font("Segoe UI Emoji", 24),  // ⬅️ Fuente Emoji
                    Location = new Point(15, 20),
                    Size = new Size(35, 35),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label texto = new Label
                {
                    Text = acciones[i].Texto,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(60, 25),
                    ForeColor = Color.White,
                    AutoSize = true
                };

                btnAccion.Controls.Add(icono);
                btnAccion.Controls.Add(texto);
                btnAccion.Click += BtnAccion_Click;

                contenedor.Controls.Add(btnAccion);
            }

            contenedor.Controls.Add(tituloPanel);
        }

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            Panel btn = sender as Panel;
            int index = (int)btn.Tag;

            switch (index)
            {
                case 0:
                    try
                    {
                        Frm_EditCliente frm = new Frm_EditCliente();
                        frm.lblTitle.Text = "Ingresar datos del Nuevo Cliente";

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var nuevoCliente = frm.CrearObjeto();

                            ClienteLN clienteLogica = new ClienteLN();

                            if (clienteLogica.CreateCliente(nuevoCliente))
                            {
                                MessageBox.Show("Cliente creado exitosamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error al crear el cliente.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        frm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al crear nuevo cliente: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 1:
                    try
                    {
                        frmEditCuenta frm = new frmEditCuenta();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var nuevaCuenta = frm.CrearObjeto();
                            if (cuentaLogica.CreateCuenta(nuevaCuenta))
                            {
                                MessageBox.Show("Cuenta creada exitosamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error al crear la cuenta.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        frm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al crear nueva cuenta: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case 2:
                    using (var frm = new FrmCrearPrestamo())
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    break;

                case 3:
                    using (var frm = new frmMenuReportes())
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    break;
            }
        }

        private void CrearTransaccionesRecientes(Panel contenedor)
        {
            Panel tituloPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(500, 30),
                BackColor = Color.Transparent
            };

            Label iconoTransacciones = new Label
            {
                Text = "📋",
                Font = new Font("Segoe UI", 16),
                Location = new Point(0, 5),
                Size = new Size(25, 25)
            };

            Label titulo = new Label
            {
                Text = "Transacciones Recientes",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(30, 5),
                AutoSize = true
            };

            tituloPanel.Controls.Add(iconoTransacciones);
            tituloPanel.Controls.Add(titulo);

            LinkLabel verTodas = new LinkLabel
            {
                Text = "Ver todas",
                Location = new Point(450, 25),
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                LinkColor = Color.FromArgb(54, 120, 255)
            };
            verTodas.Click += (s, e) =>
            {
                FrmAdminTransacciones frm = new FrmAdminTransacciones();
                MostrarFormularioEnPanel(frm);
            };

            try
            {
                // 📊 OBTENER TRANSACCIONES REALES
                var transaccionesReales = transaccionLogica.ObtenerTodasLasTransacciones()
                    .OrderByDescending(t => t.FechaTransaccion)
                    .Take(4)
                    .ToList();

                for (int i = 0; i < transaccionesReales.Count && i < 4; i++)
                {
                    var trans = transaccionesReales[i];

                    Panel filaTransaccion = new Panel
                    {
                        Size = new Size(550, 50),
                        Location = new Point(20, 60 + (i * 55)),
                        BackColor = Color.FromArgb(248, 249, 250),
                        Cursor = Cursors.Hand
                    };

                    string icono;
                    switch (trans.TipoTransaccionID)
                    {
                        case 1:
                            icono = "💰"; // Depósito
                            break;
                        case 2:
                            icono = "💸"; // Retiro
                            break;
                        case 3:
                            icono = "🔄"; // Transferencia
                            break;
                        case 4:
                            icono = "🏦"; // Préstamo
                            break;
                        default:
                            icono = "💳";
                            break;
                    }

                    Label lblIcono = new Label
                    {
                        Text = icono,
                        Font = new Font("Segoe UI", 16),
                        Location = new Point(10, 15),
                        Size = new Size(25, 20)
                    };

                    Label lblCuenta = new Label
                    {
                        Text = $"Cuenta: {trans.CuentaID}",
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        Location = new Point(40, 8),
                        AutoSize = true
                    };

                    Label lblDescripcion = new Label
                    {
                        Text = trans.Descripcion ?? "Sin descripción",
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.Gray,
                        Location = new Point(40, 28),
                        AutoSize = true,
                        MaximumSize = new Size(280, 20)
                    };

                    Label lblMonto = new Label
                    {
                        Text = trans.Monto.ToString("C2"),
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        Location = new Point(350, 8),
                        AutoSize = true
                    };

                    Color estadoColor = trans.Estado == "Completada" ?
                        Color.FromArgb(34, 197, 94) :
                        Color.FromArgb(255, 193, 7);

                    Label lblEstado = new Label
                    {
                        Text = trans.Estado ?? "Pendiente",
                        Font = new Font("Segoe UI", 10),
                        ForeColor = estadoColor,
                        Location = new Point(350, 28),
                        AutoSize = true
                    };

                    filaTransaccion.Controls.Add(lblIcono);
                    filaTransaccion.Controls.Add(lblCuenta);
                    filaTransaccion.Controls.Add(lblDescripcion);
                    filaTransaccion.Controls.Add(lblMonto);
                    filaTransaccion.Controls.Add(lblEstado);

                    contenedor.Controls.Add(filaTransaccion);
                }

                // Si no hay transacciones
                if (!transaccionesReales.Any())
                {
                    Label lblSinDatos = new Label
                    {
                        Text = "No hay transacciones recientes",
                        Font = new Font("Segoe UI", 11),
                        ForeColor = Color.Gray,
                        Location = new Point(20, 80),
                        AutoSize = true
                    };
                    contenedor.Controls.Add(lblSinDatos);
                }
            }
            catch (Exception ex)
            {
                Label lblError = new Label
                {
                    Text = $"Error al cargar transacciones: {ex.Message}",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Red,
                    Location = new Point(20, 80),
                    AutoSize = true
                };
                contenedor.Controls.Add(lblError);
            }

            contenedor.Controls.Add(tituloPanel);
            contenedor.Controls.Add(verTodas);
        }

        private void CrearActividadSistema(Panel contenedor)
        {
            Panel tituloPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(350, 30),
                BackColor = Color.Transparent
            };

            Label iconoActividad = new Label
            {
                Text = "📊",
                Font = new Font("Segoe UI", 16),
                Location = new Point(0, 5),
                Size = new Size(25, 25)
            };

            Label titulo = new Label
            {
                Text = "Actividad del Sistema",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(30, 5),
                AutoSize = true
            };

            tituloPanel.Controls.Add(iconoActividad);
            tituloPanel.Controls.Add(titulo);

            var actividades = new[]
            {
                new { Texto = "Usuarios Activos", Valor = "23", Icono = "👥" },
                new { Texto = "Sesiones Abiertas", Valor = "45", Icono = "🔓" },
                new { Texto = "Procesos en Cola", Valor = "3", Icono = "⏳" },
                new { Texto = "Alertas Pendientes", Valor = "2", Icono = "⚠️" }
            };

            for (int i = 0; i < actividades.Length; i++)
            {
                Label icono = new Label
                {
                    Text = actividades[i].Icono,
                    Font = new Font("Segoe UI", 12),
                    Location = new Point(20, 70 + (i * 40)),
                    Size = new Size(20, 20)
                };

                Label lblTexto = new Label
                {
                    Text = actividades[i].Texto,
                    Font = new Font("Segoe UI", 11),
                    Location = new Point(50, 70 + (i * 40)),
                    AutoSize = true
                };

                Label lblValor = new Label
                {
                    Text = actividades[i].Valor,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(320, 70 + (i * 40)),
                    AutoSize = true,
                    ForeColor = i == 3 ? Color.FromArgb(220, 53, 69) : Color.Black
                };

                contenedor.Controls.Add(icono);
                contenedor.Controls.Add(lblTexto);
                contenedor.Controls.Add(lblValor);
            }

            contenedor.Controls.Add(tituloPanel);
        }

        private void CrearContenidoExtra(Panel contenedor)
        {
            Panel tituloPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(600, 30),
                BackColor = Color.Transparent
            };

            Label iconoExtra = new Label
            {
                Text = "📊",
                Font = new Font("Segoe UI Emoji", 16),
                Location = new Point(0, 5),
                Size = new Size(25, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titulo = new Label
            {
                Text = "Estadísticas del Sistema",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(30, 5),
                AutoSize = true
            };

            tituloPanel.Controls.Add(iconoExtra);
            tituloPanel.Controls.Add(titulo);

            try
            {
                // 📊 GRÁFICO DE BARRAS - CUENTAS POR TIPO
                CrearGraficoBarrasCuentas(contenedor);

                // 📈 GRÁFICO CIRCULAR - ESTADO DE PRÉSTAMOS
                CrearGraficoCircularPrestamos(contenedor);

                // 📉 ESTADÍSTICAS DETALLADAS
                CrearEstadisticasDetalladas(contenedor);
            }
            catch (Exception ex)
            {
                Label lblError = new Label
                {
                    Text = $"Error al cargar estadísticas: {ex.Message}",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Red,
                    Location = new Point(20, 60),
                    AutoSize = true
                };
                contenedor.Controls.Add(lblError);
            }

            contenedor.Controls.Add(tituloPanel);
        }

        private void CrearGraficoBarrasCuentas(Panel contenedor)
        {
            Panel panelGrafico = new Panel
            {
                Location = new Point(20, 70),
                Size = new Size(450, 320),  // ⬅️ Aumentado para más tipos
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titulo = new Label
            {
                Text = "💰 Saldo Total por Tipo de Cuenta",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var cuentas = cuentaLogica.ShowCuenta("");
            var datosPorTipo = cuentas
                .Where(c => c.Saldo > 0)  // ⬅️ Solo cuentas con saldo
                .GroupBy(c => c.TipoCuenta)
                .Select(g => new
                {
                    TipoID = g.Key,
                    Nombre = ObtenerNombreTipo(g.Key),
                    SaldoTotal = g.Sum(c => c.Saldo),
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.SaldoTotal)
                .ToList();

            if (!datosPorTipo.Any())
            {
                Label lbl = new Label { Text = "Sin datos", Location = new Point(180, 120), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, AutoSize = true };
                panelGrafico.Controls.Add(lbl);
                panelGrafico.Controls.Add(titulo);
                contenedor.Controls.Add(panelGrafico);
                return;
            }

            decimal maxSaldo = datosPorTipo.Max(d => d.SaldoTotal);
            if (maxSaldo == 0) maxSaldo = 1;

            Color[] colores = {
        Color.FromArgb(54, 120, 255),   // Azul
        Color.FromArgb(34, 197, 94),    // Verde
        Color.FromArgb(147, 51, 234),   // Morado
        Color.FromArgb(255, 159, 64),   // Naranja
        Color.FromArgb(239, 68, 68),    // Rojo
    
    };

            int yPos = 50;
            int barraAltura = 30;
            int espaciado = 40;
            int anchoMaximo = 280;

            for (int i = 0; i < datosPorTipo.Count; i++)
            {
                var dato = datosPorTipo[i];
                int anchoBarra = (int)((double)dato.SaldoTotal / (double)maxSaldo * anchoMaximo);
                if (anchoBarra < 5) anchoBarra = 5;  // ⬅️ Mínimo visible

                // Etiqueta del tipo
                Label lblTipo = new Label
                {
                    Text = dato.Nombre,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(15, yPos + 6),
                    Size = new Size(80, 20),
                    ForeColor = Color.FromArgb(60, 60, 60),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Cantidad
                Label lblCant = new Label
                {
                    Text = $"({dato.Cantidad})",
                    Font = new Font("Segoe UI", 7),
                    Location = new Point(85, yPos + 8),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };

                // Barra
                Panel barra = new Panel
                {
                    Location = new Point(120, yPos),
                    Size = new Size(anchoBarra, barraAltura),
                    BackColor = colores[i % colores.Length]
                };

                // Valor
                Label lblValor = new Label
                {
                    Text = dato.SaldoTotal.ToString("C0"),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(125 + anchoBarra, yPos + 6),
                    AutoSize = true,
                    ForeColor = colores[i % colores.Length]
                };

                panelGrafico.Controls.Add(lblTipo);
                panelGrafico.Controls.Add(lblCant);
                panelGrafico.Controls.Add(barra);
                panelGrafico.Controls.Add(lblValor);

                yPos += espaciado;
            }

            panelGrafico.Controls.Add(titulo);
            contenedor.Controls.Add(panelGrafico);
        }

        private string ObtenerNombreTipo(int id)
        {
            switch (id)
            {
                case 1: return "Ahorro";
                case 2: return "Corriente";
                case 3: return "Nómina";
                case 4: return "Premium";
                case 5: return "Joven";
                default: return $"Tipo {id}";
            }
        }

        private void CrearGraficoCircularPrestamos(Panel contenedor)
        {
            Panel panelGrafico = new Panel
            {
                Location = new Point(490, 70),
                Size = new Size(450, 250),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titulo = new Label
            {
                Text = "🏦 Estado de Préstamos",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var prestamos = prestamoLogica.ListarPrestamosFiltro(null, null, null, null, null);
            var prestamosPorEstado = prestamos
                .GroupBy(p =>
                {
                    var tipoPrestamo = p.GetType();
                    var estadoProp = tipoPrestamo.GetProperty("Estado");
                    return estadoProp?.GetValue(p, null)?.ToString()?.Trim() ?? "Sin Estado";
                })
                .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                .ToList();

            int total = prestamosPorEstado.Sum(p => p.Cantidad);
            if (total == 0) total = 1;

            int centerX = 120;
            int centerY = 130;
            int radius = 70;
            float startAngle = 0;

            Color[] colores = {
                Color.FromArgb(34, 197, 94),    // Verde - Aprobado
                Color.FromArgb(255, 193, 7),    // Amarillo - Pendiente
                Color.FromArgb(239, 68, 68),    // Rojo - Rechazado
                Color.FromArgb(147, 51, 234)    // Morado - Otros
            };

            PictureBox circleChart = new PictureBox
            {
                Size = new Size(240, 240),
                Location = new Point(10, 40),
                BackColor = Color.White
            };

            Bitmap bmp = new Bitmap(240, 240);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                for (int i = 0; i < prestamosPorEstado.Count; i++)
                {
                    float sweepAngle = (float)prestamosPorEstado[i].Cantidad / total * 360;

                    using (SolidBrush brush = new SolidBrush(colores[i % colores.Length]))
                    {
                        g.FillPie(brush, centerX - radius, centerY - radius,
                                 radius * 2, radius * 2, startAngle, sweepAngle);
                    }

                    startAngle += sweepAngle;
                }

                using (SolidBrush centerBrush = new SolidBrush(Color.White))
                {
                    int innerRadius = radius - 25;
                    g.FillEllipse(centerBrush, centerX - innerRadius, centerY - innerRadius,
                                 innerRadius * 2, innerRadius * 2);
                }
            }

            circleChart.Image = bmp;

            int legendY = 60;
            for (int i = 0; i < prestamosPorEstado.Count && i < 4; i++)
            {
                Panel colorBox = new Panel
                {
                    Size = new Size(15, 15),
                    Location = new Point(270, legendY + (i * 30)),
                    BackColor = colores[i % colores.Length]
                };

                Label lblEstado = new Label
                {
                    Text = $"{prestamosPorEstado[i].Estado}: {prestamosPorEstado[i].Cantidad}",
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(290, legendY + (i * 30) - 2),
                    AutoSize = true
                };

                decimal porcentaje = (decimal)prestamosPorEstado[i].Cantidad / total * 100;
                Label lblPorcentaje = new Label
                {
                    Text = $"({porcentaje:F1}%)",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.Gray,
                    Location = new Point(290, legendY + (i * 30) + 15),
                    AutoSize = true
                };

                panelGrafico.Controls.Add(colorBox);
                panelGrafico.Controls.Add(lblEstado);
                panelGrafico.Controls.Add(lblPorcentaje);
            }

            panelGrafico.Controls.Add(titulo);
            panelGrafico.Controls.Add(circleChart);
            contenedor.Controls.Add(panelGrafico);
        }

        private void CrearEstadisticasDetalladas(Panel contenedor)
        {
            Panel panelStats = new Panel
            {
                Location = new Point(20, 420), // ⬅️ Posición ajustada
                Size = new Size(920, 120),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titulo = new Label
            {
                Text = "📈 Resumen Financiero",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var cuentas = cuentaLogica.ShowCuenta("");
            decimal saldoTotal = cuentas?.Sum(c => c.Saldo) ?? 0;

            var transacciones = transaccionLogica.ObtenerTodasLasTransacciones();
            decimal montoTransaccionesHoy = transacciones?
                .Where(t => t.FechaTransaccion.Date == DateTime.Now.Date)
                .Sum(t => t.Monto) ?? 0;

            var prestamos = prestamoLogica.ListarPrestamosFiltro(null, null, null, null, null);
            decimal montoPrestamosActivos = 0;

            foreach (var p in prestamos)
            {
                try
                {
                    var tipoPrestamo = p.GetType();
                    var estadoProp = tipoPrestamo.GetProperty("Estado");
                    var montoProp = tipoPrestamo.GetProperty("MontoAprobado") ??
                                   tipoPrestamo.GetProperty("Monto");

                    string estado = estadoProp?.GetValue(p, null)?.ToString()?.Trim() ?? "";

                    if (estado.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) ||
                        estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                    {
                        object montoObj = montoProp?.GetValue(p, null);
                        if (montoObj != null)
                        {
                            montoPrestamosActivos += Convert.ToDecimal(montoObj);
                        }
                    }
                }
                catch { }
            }

            var stats = new[]
            {
                new { Titulo = "Saldo Total Sistema", Valor = saldoTotal.ToString("C2"), Color = Color.FromArgb(54, 120, 255) },
                new { Titulo = "Transacciones Hoy", Valor = montoTransaccionesHoy.ToString("C2"), Color = Color.FromArgb(34, 197, 94) },
                new { Titulo = "Préstamos Activos", Valor = montoPrestamosActivos.ToString("C2"), Color = Color.FromArgb(147, 51, 234) }
            };

            for (int i = 0; i < stats.Length; i++)
            {
                Panel statBox = new Panel
                {
                    Size = new Size(280, 70),
                    Location = new Point(20 + (i * 300), 45),
                    BackColor = Color.FromArgb(248, 249, 250)
                };

                Label lblTitulo = new Label
                {
                    Text = stats[i].Titulo,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Gray,
                    Location = new Point(15, 12),
                    AutoSize = true
                };

                Label lblValor = new Label
                {
                    Text = stats[i].Valor,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = stats[i].Color,
                    Location = new Point(15, 35),
                    AutoSize = true
                };

                statBox.Controls.Add(lblTitulo);
                statBox.Controls.Add(lblValor);
                panelStats.Controls.Add(statBox);
            }

            panelStats.Controls.Add(titulo);
            contenedor.Controls.Add(panelStats);
        }

        private void InicializarTimer()
        {
            timerHora = new Timer();
            timerHora.Interval = 1000;
            timerHora.Tick += TimerHora_Tick;
            timerHora.Start();
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            this.lblFechaHora.Text = "🕐 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {
            ConfigurarAcordeones();
            CrearDashboard();
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}