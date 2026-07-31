<%@ Page Title="Inicio - Hotel APM Grand" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Capara_Presentacion_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="py-3">

        <!-- ========================================== -->
        <!-- BANNER DE AUTENTICACIÓN / BIENVENIDA VIP   -->
        <!-- ========================================== -->
        <!-- BANNER BIENVENIDA (Solo visible cuando hay sesión) -->
        <asp:Panel ID="pnlBienvenidaUsuario" runat="server" Visible="false" CssClass="mb-5">
            <div class="card border-0 shadow-sm rounded-4 p-4 text-white" style="background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%); border-left: 5px solid #d97706 !important;">
                <div class="d-flex align-items-center justify-content-between flex-wrap gap-3">
                    <div class="d-flex align-items-center gap-3">
                        <div class="bg-warning text-dark rounded-circle d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
                            <i class="fas fa-user-check fs-4"></i>
                        </div>
                        <div>
                            <span class="badge bg-warning text-dark mb-1">Huésped Distinguido</span>
                            <h4 class="fw-bold mb-0 text-white">¡Hola,
                                <asp:Label ID="lblNombreBienvenida" runat="server"></asp:Label>!</h4>
                            <p class="text-muted small mb-0">Tu sesión está activa. Ya puedes realizar tus reservas directamente.</p>
                        </div>
                    </div>
                    <div class="d-flex gap-2">
                        <a href="frmHistorialCliente.aspx" class="btn btn-outline-warning btn-sm rounded-pill px-3">
                            <i class="fas fa-calendar-check me-1"></i>Mis Reservas
                        </a>
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
                            <span class="badge bg-gold text-dark fw-bold mb-1">Huésped Distinguido</span>
                            <h4 class="fw-bold mb-0">¡Hola,
                                    <asp:Label ID="lblUsuarioSesion" runat="server" CssClass="text-warning"></asp:Label>!
                            </h4>
                            <small class="text-light opacity-75">Tu sesión está activa. Ya puedes realizar tus reservas directamente.</small>
                        </div>
                    </div>
                    <div class="d-flex gap-2 align-items-center flex-wrap">
                        <a href="frmReservas.aspx" class="btn btn-sm btn-outline-light rounded-pill px-3 py-2 fw-semibold">📋 Mis Reservas</a>
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
                <span class="badge bg-gold text-dark mb-3 px-3 py-2 fw-bold text-uppercase tracking-wider">Experiencia De Lujo</span>
                <h1 class="display-3 fw-bold text-white mb-3">Hotel APM Grand</h1>
                <p class="col-md-8 fs-5 text-light opacity-90 fw-light">
                    Descubre el máximo confort y elegancia. Explora nuestras habitaciones, servicios exclusivos y reserva tu próxima estadía inolvidable.
                </p>
                <div class="d-flex gap-3 mt-4 flex-wrap">
                    <a href="#habitaciones" class="btn btn-warning btn-lg px-4 fw-bold shadow-sm rounded-pill btn-auth-gold">🛏️ Ver Habitaciones</a>
                    <a href="#servicios" class="btn btn-outline-light btn-lg px-4 rounded-pill">✨ Ver Servicios Adicionales</a>
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

        <!-- ========================================== -->
        <!-- SERVICIOS ADICIONALES (DINÁMICO DESDE BD)  -->
        <!-- ========================================== -->
        <section id="servicios" class="p-5 bg-light rounded-4 border mb-5">
            <div class="text-center mb-4">
                <span class="badge bg-warning text-dark px-3 py-2 fw-bold text-uppercase">Complementa tu Estancia</span>
                <h2 class="fw-bold text-dark mt-2">Servicios del Hotel</h2>
                <p class="text-muted">Añade estos servicios a tu experiencia durante el proceso de reserva.</p>
            </div>

            <div class="row g-3">
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
        <!-- ========================================== -->
        <!-- SECCIÓN DE RESEÑAS Y VALORACIONES          -->
        <!-- ========================================== -->
        <section id="resenas" class="mb-5">
            <div class="text-center mb-5">
                <span class="badge bg-gold text-dark px-3 py-2 fw-bold text-uppercase tracking-wider">Experiencias Reales</span>
                <h2 class="fw-bold text-dark mt-2">Lo que opinan nuestros huéspedes</h2>
                <p class="text-muted">Conoce las valoraciones y comentarios de quienes ya han disfrutado de nuestro hotel.</p>
            </div>

            <!-- Resumen Promedio Flotante Integrado -->
            <div class="card border-0 shadow-sm rounded-4 p-4 mb-5 bg-white">
                <div class="row align-items-center justify-content-center g-4 text-center text-md-start">
                    <div class="col-md-3 border-end-md text-center">
                        <div class="display-3 fw-bolder text-dark mb-0 leading-none">
                            <asp:Label ID="lblPromedio" runat="server" Text="0.0"></asp:Label>
                        </div>
                        <div class="text-warning fs-5 my-1">
                            <i class="fas fa-star"></i><i class="fas fa-star"></i><i class="fas fa-star"></i><i class="fas fa-star"></i><i class="fas fa-star"></i>
                        </div>
                        <small class="text-muted d-block">Promedio General</small>
                    </div>

                    <div class="col-md-5">
                        <h5 class="fw-bold text-dark mb-1">Satisfacción Garantizada</h5>
                        <p class="text-muted small mb-0">
                            Basado en <strong>
                                <asp:Label ID="lblTotalResenas" runat="server" Text="0"></asp:Label></strong> opiniones de clientes verificados. Nos esforzamos día a día por darte la mejor experiencia VIP.
                        </p>
                    </div>

                    <div class="col-md-4 text-center text-md-end">
                        <%-- Botón que desliza suavemente al formulario o alerta si no está logueado --%>
                        <a href="#formResena" class="btn btn-warning fw-bold rounded-pill px-4 py-2 shadow-sm btn-auth-gold">✍️ Dejar una Opinión
                        </a>
                    </div>
                </div>
            </div>

            <!-- Grid Principal: Formulario y Listado de Reseñas -->
            <div class="row g-4" id="formResena">

                <!-- Columna Formulario / Invitación -->
                <div class="col-lg-5">
                    <!-- Formulario de Reseña (Usuario Logueado) -->
                    <asp:Panel ID="pnlCrearResena" runat="server" Visible="false" CssClass="card border-0 shadow-sm rounded-4 p-4 bg-white sticky-form">
                        <div class="d-flex align-items-center gap-2 mb-3">
                            <div class="icon-avatar-badge p-2">
                                <i class="fas fa-pen-alt text-warning fs-5"></i>
                            </div>
                            <div>
                                <h5 class="fw-bold text-dark mb-0">Tu Opinión Cuenta</h5>
                                <small class="text-muted">Comparte los detalles de tu estancia</small>
                            </div>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-secondary">Nº / ID de Reserva</label>
                            <div class="input-group">
                                <span class="input-group-text bg-light border-end-0"><i class="fas fa-hashtag text-muted"></i></span>
                                <asp:TextBox ID="txtIdReserva" runat="server" CssClass="form-control border-start-0" TextMode="Number" Placeholder="Ej: 102"></asp:TextBox>
                            </div>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-secondary">Calificación</label>
                            <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="form-select border-1">
                                <asp:ListItem Value="5">⭐⭐⭐⭐⭐ (5/5) - Excelente</asp:ListItem>
                                <asp:ListItem Value="4">⭐⭐⭐⭐ (4/5) - Muy Bueno</asp:ListItem>
                                <asp:ListItem Value="3">⭐⭐⭐ (3/5) - Aceptable</asp:ListItem>
                                <asp:ListItem Value="2">⭐⭐ (2/5) - Regular</asp:ListItem>
                                <asp:ListItem Value="1">⭐ (1/5) - Mala</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label class="form-label small fw-bold text-secondary">Tu Comentario</label>
                            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Placeholder="¿Qué fue lo que más te gustó de las instalaciones o el servicio?"></asp:TextBox>
                        </div>

                        <asp:Button ID="btnEnviarResena" runat="server" Text="Publicar Reseña" CssClass="btn btn-warning w-100 fw-bold rounded-pill py-2 shadow-sm btn-auth-gold" OnClick="btnEnviarResena_Click" />
                        <asp:Label ID="lblMensajeResena" runat="server" CssClass="d-block text-center mt-2 small fw-bold"></asp:Label>
                    </asp:Panel>

                    <!-- Tarjeta para Usuarios Invitados -->
                    <asp:Panel ID="pnlInvitadoResena" runat="server" CssClass="card border-0 shadow-sm rounded-4 p-4 bg-white text-center">
                        <div class="mb-3">
                            <span class="badge bg-light text-muted p-3 rounded-circle">
                                <i class="fas fa-user-lock fa-2x text-warning"></i>
                            </span>
                        </div>
                        <h5 class="fw-bold text-dark mb-2">¿Estuviste alojado con nosotros?</h5>
                        <p class="text-muted small mb-4">Inicia sesión en tu cuenta de huésped para poder calificar tu estancia y dejarnos tus comentarios.</p>
                        <a href="Login.aspx" class="btn btn-outline-dark rounded-pill px-4 fw-semibold btn-sm">
                            <i class="fas fa-sign-in-alt me-1"></i>Iniciar Sesión para Comentar
                        </a>
                    </asp:Panel>
                </div>

                <!-- Columna Listado Dinámico de Reseñas -->
                <div class="col-lg-7">
                    <div class="resenas-scroll pe-2">
                        <asp:Repeater ID="rptResenas" runat="server">
                            <ItemTemplate>
                                <div class="card border-0 shadow-sm rounded-4 p-4 mb-3 bg-white resena-card">
                                    <div class="d-flex justify-content-between align-items-start mb-2">
                                        <div class="d-flex align-items-center gap-3">
                                            <div class="avatar-circle">
                                                <i class="fas fa-user text-secondary"></i>
                                            </div>
                                            <div>
                                                <h6 class="fw-bold text-dark mb-0">Huésped Verificado</h6>
                                                <small class="text-muted"><i class="far fa-calendar-alt me-1"></i><%# Eval("FechaResena", "{0:dd MMM, yyyy}") %></small>
                                            </div>
                                        </div>
                                        <div class="text-warning small bg-light px-2 py-1 rounded-pill border">
                                            <%# GenerarEstrellas(Convert.ToInt32(Eval("Calificacion"))) %>
                                        </div>
                                    </div>
                                    <p class="text-dark small mb-0 mt-2 resena-texto">
                                        "<%# Eval("Comentario") %>"
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

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

        /* ESTILOS PARA LA SECCIÓN DE RESEÑAS RESTRUCTURADA */
        .resenas-scroll {
            max-height: 520px;
            overflow-y: auto;
            padding-right: 8px;
        }

            /* Estilo personalizado para el scrollbar */
            .resenas-scroll::-webkit-scrollbar {
                width: 6px;
            }

            .resenas-scroll::-webkit-scrollbar-track {
                background: #f1f1f1;
                border-radius: 10px;
            }

            .resenas-scroll::-webkit-scrollbar-thumb {
                background: #cbd5e1;
                border-radius: 10px;
            }

                .resenas-scroll::-webkit-scrollbar-thumb:hover {
                    background: var(--apm-gold);
                }

        .avatar-circle {
            width: 42px;
            height: 42px;
            background-color: #f1f5f9;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            border: 1px solid #e2e8f0;
        }

        .resena-card {
            transition: border-color 0.2s ease, transform 0.2s ease;
            border-left: 4px solid var(--apm-gold) !important;
        }

            .resena-card:hover {
                transform: translateX(4px);
            }

        .resena-texto {
            line-height: 1.6;
            color: #334155 !important;
            font-style: italic;
        }

        .leading-none {
            line-height: 1;
        }

        .sticky-form {
            position: sticky;
            top: 20px;
        }

        @media (min-width: 768px) {
            .border-end-md {
                border-right: 1px solid #e2e8f0 !important;
            }
        }
    </style>

</asp:Content>
