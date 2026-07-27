<%@ Page Title="Inicio - Hotel APM Grand" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="py-3">

        <!-- ========================================== -->
        <!-- BANNER DE AUTENTICACIÓN / BIENVENIDA VIP   -->
        <!-- ========================================== -->
        <div class="mb-5">
            <!-- Panel 1: Usuario Invitado (Sin Sesión) -->
            <asp:Panel ID="pnlInvitado" runat="server" CssClass="card border-0 shadow-sm overflow-hidden top-auth-card">
                <div class="card-body p-4 bg-white">
                    <div class="row align-items-center g-3">
                        <div class="col-lg-7 d-flex align-items-center gap-3">
                            <div class="icon-avatar-badge">
                                <img src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png" alt="Usuario" width="40" height="40" />
                            </div>
                            <div>
                                <span class="badge bg-gold text-dark mb-1 text-uppercase fw-bold px-2 py-1 fs-xs">Bienvenido a Hotel APM Grand</span>
                                <h5 class="fw-bold text-dark mb-0">¿Listo para vivir la mejor experiencia?</h5>
                                <p class="text-muted small mb-0">Inicia sesión o crea tu cuenta para gestionar reservas y beneficios VIP.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- Panel 2: Usuario Autenticado (Muestra Nombre del Cliente) -->
            <asp:Panel ID="pnlAutenticado" runat="server" Visible="false" CssClass="card border-0 shadow-lg overflow-hidden auth-logged-card">
                <div class="card-body p-4 text-white">
                    <div class="d-flex align-items-center justify-content-between flex-wrap gap-3">
                        <div class="d-flex align-items-center gap-3">
                            <div class="vip-badge-icon">
                                <img src="https://cdn-icons-png.flaticon.com/512/3177/3177440.png" width="42" height="42" alt="VIP" />
                            </div>
                            <div>
                                <span class="badge bg-gold text-dark fw-bold mb-1">Huésped Distinguido
                                </span>
                                <h4 class="fw-bold mb-0">¡Hola,
                                    <asp:Label ID="lblUsuarioSesion" runat="server" CssClass="text-warning"></asp:Label>!
                                </h4>
                                <small class="text-light opacity-75">Tu sesión está activa. Ya puedes realizar tus reservas directamente.</small>
                            </div>
                        </div>
                        <div class="d-flex gap-2 align-items-center flex-wrap">
                            <a href="frmReservas.aspx" class="btn btn-sm btn-outline-light rounded-pill px-3 py-2 fw-semibold">📋 Mis Reservas
                            </a>
                            <asp:Button ID="btnLogout" runat="server" Text="Cerrar Sesión" CssClass="btn btn-danger btn-sm rounded-pill px-3 py-2 fw-bold" OnClick="btnLogout_Click" CauseValidation="false" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <!-- ========================================== -->
        <!-- SECCIÓN HERO PRINCIPAL                    -->
        <!-- ========================================== -->
        <section class="p-5 mb-5 text-white rounded-4 shadow-lg style-hero position-relative overflow-hidden">
            <div class="hero-overlay"></div>
            <div class="container-fluid py-4 position-relative" style="z-index: 2;">
                <span class="badge bg-gold text-dark mb-3 px-3 py-2 fw-bold text-uppercase tracking-wider">Experiencia De Lujo
                </span>
                <h1 class="display-3 fw-bold text-white mb-3">Hotel APM Grand</h1>
                <p class="col-md-8 fs-5 text-light opacity-90 fw-light">
                    Descubre el máximo confort y elegancia. Explora nuestras habitaciones, servicios exclusivos y reserva tu próxima estadía inolvidable.
                </p>
                <div class="d-flex gap-3 mt-4 flex-wrap">
                    <a href="#habitaciones" class="btn btn-warning btn-lg px-4 fw-bold shadow-sm rounded-pill btn-auth-gold">🛏️ Ver Habitaciones
                    </a>
                    <a href="#servicios" class="btn btn-outline-light btn-lg px-4 rounded-pill">✨ Ver Servicios Adicionales
                    </a>
                </div>
            </div>
        </section>

        <!-- ========================================== -->
        <!-- CATÁLOGO DE HABITACIONES (DINÁMICO DESDE BD) -->
        <!-- ========================================== -->
        <section id="habitaciones" class="mb-5">
            <div class="text-center mb-4">
                <h2 class="fw-bold text-dark">Nuestras Habitaciones</h2>
                <p class="text-muted">Espacios diseñados para tu comodidad con tarifas claras y transparentes.</p>
            </div>

            <div class="row g-4">
                <asp:Repeater ID="rptHabitaciones" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 col-lg-3">
                            <div class="card h-100 border-0 shadow-sm hover-top overflow-hidden rounded-4">
                                <img src='<%# ObtenerImagenHabitacion(Eval("Tipo")) %>' class="card-img-top room-img" alt="Habitación" />
                                <div class="card-body p-4 d-flex flex-column">
                                    <div class="d-flex justify-content-between align-items-center mb-2">
                                        <span class="badge bg-secondary">Nº <%# Eval("Numero") %></span>
                                        <span class="fw-bold text-warning fs-5">$<%# FormatearPrecio(Eval("PrecioPorNoche")) %> <small class="fs-6 text-muted">/ noche</small></span>
                                    </div>
                                    <h5 class="card-title fw-bold text-capitalize"><%# Eval("Tipo") %></h5>
                                    <p class="card-text text-muted small flex-grow-1">
                                        Habitación tipo <%# Eval("Tipo") %> ideal para tu descanso y confort.
                                    </p>
                                    <div class="mb-3">
                                        <small class="text-muted d-block">👤 Capacidad: <strong><%# Eval("Capacidad") %> Personas</strong></small>
                                        <small class="text-muted d-block">🏢 Piso: <strong><%# Eval("Piso") %></strong></small>
                                    </div>
                                    <asp:Button ID="btnReserva" runat="server" Text="Solicitar Reserva" CssClass="btn btn-dark w-100 fw-bold rounded-pill" OnClick="btnSolicitarReserva_Click" CommandArgument='<%# Eval("IdHabitacion") %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

        <!-- SERVICIOS ADICIONALES (DINÁMICO DESDE BD)  -->
        <!-- ========================================== -->
        <section id="servicios" class="p-5 bg-light rounded-4 border mb-5">
            <div class="text-center mb-4">
                <span class="badge bg-warning text-dark px-3 py-2 fw-bold text-uppercase">Complementa tu Estancia</span>
                <h2 class="fw-bold text-dark mt-2">Servicios del Hotel</h2>
                <p class="text-muted">Añade estos servicios a tu experiencia durante el proceso de reserva.</p>
            </div>

            <div class="row g-3">
                <!-- AQUÍ ESTÁ EL REPEATER DE SERVICIOS -->
                <asp:Repeater ID="rptServicios" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="p-3 bg-white rounded-3 border d-flex align-items-center justify-content-between shadow-sm">
                                <div class="d-flex align-items-center gap-3">
                                    <span class="fs-3"><%# ObtenerIconoServicio(Eval("Nombre")) %></span>
                                    <div>
                                        <h6 class="fw-bold mb-0"><%# Eval("Nombre") %></h6>
                                        <small class="text-muted"><%# Eval("Descripcion") %></small>
                                    </div>
                                </div>
                                <span class="fw-bold text-warning fs-5">$<%# FormatearPrecio(Eval("Precio")) %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

    <style>
        :root {
            --apm-gold: #f59e0b;
            --apm-gold-hover: #d97706;
            --apm-dark: #0f172a;
            --apm-slate: #1e293b;
        }

        .bg-gold {
            background-color: var(--apm-gold) !important;
        }

        .fs-xs {
            font-size: 0.75rem;
        }

        .top-auth-card {
            border-left: 5px solid var(--apm-gold) !important;
            border-radius: 1rem !important;
        }

        .icon-avatar-badge {
            background: #fffbe2;
            padding: 12px;
            border-radius: 50%;
            border: 1px solid rgba(245, 158, 11, 0.3);
        }

        .auth-logged-card {
            background: linear-gradient(135deg, var(--apm-dark) 0%, var(--apm-slate) 100%) !important;
            border-left: 6px solid var(--apm-gold) !important;
            border-radius: 1rem !important;
        }

        .vip-badge-icon {
            background: rgba(255, 255, 255, 0.1);
            padding: 10px;
            border-radius: 50%;
            border: 1px solid rgba(245, 158, 11, 0.4);
        }

        .btn-auth-gold {
            background-color: var(--apm-gold) !important;
            border-color: var(--apm-gold) !important;
            color: #000 !important;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

            .btn-auth-gold:hover {
                background-color: var(--apm-gold-hover) !important;
                transform: translateY(-2px);
                box-shadow: 0 6px 15px rgba(245, 158, 11, 0.4) !important;
            }

        .btn-auth-action {
            transition: transform 0.2s ease;
        }

            .btn-auth-action:hover {
                transform: translateY(-2px);
            }

        .style-hero {
            background: linear-gradient(135deg, #0b1120 0%, #1a2332 60%, #2a1805 100%);
            border-left: 6px solid var(--apm-gold);
        }

        .hero-overlay {
            position: absolute;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: radial-gradient(circle at top right, rgba(245, 158, 11, 0.2), transparent 60%);
            pointer-events: none;
        }

        .tracking-wider {
            letter-spacing: 1px;
        }

        .room-img {
            height: 180px;
            object-fit: cover;
        }

        .hover-top {
            transition: transform 0.25s ease, box-shadow 0.25s ease;
        }

            .hover-top:hover {
                transform: translateY(-6px);
                box-shadow: 0 12px 24px rgba(0,0,0,0.15) !important;
            }
    </style>

</asp:Content>
