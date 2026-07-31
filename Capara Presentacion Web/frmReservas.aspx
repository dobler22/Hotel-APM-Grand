<%@ Page Title="Reservar Habitación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservas.aspx.cs" Inherits="Capara_Presentacion_Web.frmReservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container py-4">

        <!-- ALERTA SI NO ESTÁ LOGUEADO -->
        <asp:Panel ID="pnlNoAutenticado" runat="server" Visible="false" CssClass="alert alert-warning text-center p-4 rounded-4 shadow-sm my-5">
            <h4 class="fw-bold">🔒 Inicia Sesión para Continuar</h4>
            <p class="mb-3">Para seleccionar fechas y confirmar tu reserva en Hotel APM Grand, necesitas estar registrado.</p>
            <a href="Login.aspx" class="btn btn-dark rounded-pill px-4 fw-bold">Iniciar Sesión</a>
            <a href="Registro.aspx" class="btn btn-outline-dark rounded-pill px-4 ms-2">Crear Cuenta</a>
        </asp:Panel>

        <!-- CONTENIDO PRINCIPAL DE RESERVA (SOLO SI TIENE SESIÓN) -->
        <asp:Panel ID="pnlFormularioReserva" runat="server">

            <div class="row g-4">
                
                <!-- DETALLES DE LA HABITACIÓN SELECCIONADA -->
                <div class="col-lg-5">
                    <div class="card border-0 shadow-sm rounded-4 overflow-hidden">
                        <asp:Image ID="imgHabitacion" runat="server" CssClass="card-img-top room-header-img" ImageUrl="https://images.unsplash.com/photo-1566665797739-1674de7a421a?auto=format&fit=crop&w=600&q=80" />
                        <div class="card-body p-4">
                            <span class="badge bg-warning text-dark mb-2 px-3 py-1 fw-bold">Habitación Seleccionada</span>
                            <h3 class="fw-bold text-dark"><asp:Label ID="lblTipoHabitacion" runat="server" Text="Habitación"></asp:Label></h3>
                            
                            <hr />

                            <div class="d-flex justify-content-between mb-2">
                                <span class="text-muted">Número:</span>
                                <strong>Nº <asp:Label ID="lblNumero" runat="server"></asp:Label></strong>
                            </div>
                            <div class="d-flex justify-content-between mb-2">
                                <span class="text-muted">Piso:</span>
                                <strong>Piso <asp:Label ID="lblPiso" runat="server"></asp:Label></strong>
                            </div>
                            <div class="d-flex justify-content-between mb-2">
                                <span class="text-muted">Capacidad Max:</span>
                                <strong><asp:Label ID="lblCapacidad" runat="server"></asp:Label> Personas</strong>
                            </div>
                            <div class="d-flex justify-content-between fs-5 mt-3 pt-2 border-top">
                                <span>Precio por noche:</span>
                                <strong class="text-warning">$<asp:Label ID="lblPrecio" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- SELECTOR DE FECHAS Y CALENDARIO -->
                <div class="col-lg-7">
                    <div class="card border-0 shadow-sm rounded-4 p-4">
                        <h4 class="fw-bold mb-1">📅 Selecciona las Fechas de tu Estancia</h4>
                        <p class="text-muted small mb-4">Elige tu fecha de check-in y check-out para validar disponibilidad.</p>

                        <!-- Mensajes de Alerta -->
                        <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-danger d-block mb-3" Visible="false"></asp:Label>
                        <asp:Label ID="lblMensajeExito" runat="server" CssClass="alert alert-success d-block mb-3" Visible="false"></asp:Label>

                        <div class="row g-3 mb-4">
                            <div class="col-md-6">
                                <label class="form-label fw-bold small text-uppercase">Fecha Entrada (Check-In)</label>
                                <asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" CssClass="form-control rounded-3" AutoPostBack="true" OnTextChanged="txtFechas_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-bold small text-uppercase">Fecha Salida (Check-Out)</label>
                                <asp:TextBox ID="txtFechaSalida" runat="server" TextMode="Date" CssClass="form-control rounded-3" AutoPostBack="true" OnTextChanged="txtFechas_TextChanged"></asp:TextBox>
                            </div>
                        </div>

                        <!-- CALENDARIO INTERACTIVO DE ESTADO -->
                        <div class="p-3 bg-light rounded-4 mb-4 border">
                            <h6 class="fw-bold mb-3">Historial de Disponibilidad del Mes</h6>
                            <div class="d-flex justify-content-center">
                                <asp:Calendar ID="calDisponibilidad" runat="server" CssClass="w-100 bg-white rounded-3 shadow-sm p-2 border-0" 
                                    OnDayRender="calDisponibilidad_DayRender" 
                                    NextPrevFormat="FullMonth" 
                                    TitleStyle-CssClass="fw-bold text-dark bg-light"
                                    DayHeaderStyle-CssClass="text-muted fw-bold small">
                                </asp:Calendar>
                            </div>
                            <div class="d-flex gap-3 mt-3 justify-content-center small">
                                <span><span class="badge bg-success">■</span> Disponible</span>
                                <span><span class="badge bg-danger">■</span> Ocupado / Reservado</span>
                            </div>
                        </div>

                        <!-- RESUMEN DE COMPRA Y BOTÓN DE CONFIRMACIÓN -->
                        <div class="border-top pt-3 d-flex justify-content-between align-items-center">
                            <div>
                                <small class="text-muted d-block">Total Estimado:</small>
                                <h3 class="fw-bold text-warning mb-0">$<asp:Label ID="lblTotalEstimado" runat="server" Text="0.00"></asp:Label></h3>
                            </div>
                            <asp:Button ID="btnConfirmarReserva" runat="server" Text="Confirmar Reserva" CssClass="btn btn-warning rounded-pill px-4 py-2 fw-bold shadow-sm" OnClick="btnConfirmarReserva_Click" Enabled="false" />
                        </div>

                    </div>
                </div>

            </div>

        </asp:Panel>

    </main>

    <style>
        .room-header-img { height: 220px; object-fit: cover; }
    </style>

</asp:Content>