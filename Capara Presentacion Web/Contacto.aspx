<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Capara_Presentacion_Web.Contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- CSS de protección para evitar desbordamiento de las imágenes -->
    <style type="text/css">
        .avatar-frame {
            width: 140px !important;
            height: 140px !important;
            min-width: 140px !important;
            min-height: 140px !important;
            border-radius: 50% !important;
            border: 4px solid #f59e0b !important;
            overflow: hidden !important;
            position: relative !important;
            margin: 0 auto !important;
            box-shadow: 0 4px 10px rgba(0,0,0,0.15) !important;
            background-color: #f8f9fa !important;
        }

        .avatar-img {
            width: 100% !important;
            height: 100% !important;
            max-width: 100% !important;
            max-height: 100% !important;
            object-fit: cover !important;
            object-position: top center !important;
            display: block !important;
        }
    </style>

    <div class="container py-4">
        
        <!-- Encabezado de Página -->
        <div class="text-center mb-5">
            <h1 class="fw-bold text-dark">
                <i class="fas fa-headset text-warning me-2"></i>Contáctanos
            </h1>
            <p class="text-muted lead fs-6">Estamos aquí para atender tus dudas, sugerencias o reservaciones especiales en Hotel APM Grand.</p>
            <div class="mx-auto bg-warning" style="height: 3px; width: 60px; border-radius: 2px;"></div>
        </div>

        <!-- Panel de Mensajes / Alertas -->
        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show mb-4" role="alert">
            <i class="fas fa-check-circle me-2"></i>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <div class="row g-4 mb-5">
            <!-- Formulario de Contacto -->
            <div class="col-lg-7">
                <div class="card shadow-sm border-0 rounded-3 h-100">
                    <div class="card-header bg-dark text-white py-3">
                        <h5 class="mb-0 fw-semibold">
                            <i class="fas fa-envelope-open-text me-2 text-warning"></i>Envíanos un mensaje
                        </h5>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtNombre" class="form-label fw-semibold text-secondary small">Nombre Completo <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Carlos Mendoza" Required="true"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEmail" class="form-label fw-semibold text-secondary small">Correo Electrónico <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="ejemplo@correo.com" Required="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <label for="txtAsunto" class="form-label fw-semibold text-secondary small">Asunto <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control" placeholder="Ej. Consulta sobre reservaciones corporativas" Required="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <label for="txtMensaje" class="form-label fw-semibold text-secondary small">Mensaje <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Escribe aquí tu consulta..." Required="true"></asp:TextBox>
                            </div>
                            <div class="col-12 mt-4 text-end">
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar Mensaje" CssClass="btn btn-warning rounded-pill px-4 fw-bold shadow-sm" OnClick="btnEnviar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Información Institucional / UTMACH -->
            <div class="col-lg-5">
                <div class="card shadow-sm border-0 rounded-3 bg-dark text-white h-100">
                    <div class="card-body p-4 d-flex flex-column justify-content-between">
                        <div>
                            <h5 class="fw-bold text-warning mb-4">
                                <i class="fas fa-hotel me-2"></i>Hotel APM Grand
                            </h5>
                            
                            <ul class="list-unstyled mb-4">
                                <li class="d-flex align-items-start mb-3">
                                    <i class="fas fa-map-marker-alt text-warning mt-1 me-3 fs-5"></i>
                                    <div>
                                        <strong class="d-block">Ubicación Principal:</strong>
                                        <span class="text-light small">Av. Circunvalación Norte y Panamericana, Machala, El Oro, Ecuador</span>
                                    </div>
                                </li>
                                <li class="d-flex align-items-start mb-3">
                                    <i class="fas fa-phone-alt text-warning mt-1 me-3 fs-5"></i>
                                    <div>
                                        <strong class="d-block">Teléfonos de Atención:</strong>
                                        <span class="text-light small">+593 (07) 298-3400 / +593 99 123 4567</span>
                                    </div>
                                </li>
                                <li class="d-flex align-items-start mb-3">
                                    <i class="fas fa-envelope text-warning mt-1 me-3 fs-5"></i>
                                    <div>
                                        <strong class="d-block">Correo Electrónico:</strong>
                                        <span class="text-light small">contacto@hotelapmgrand.com</span>
                                    </div>
                                </li>
                                <li class="d-flex align-items-start">
                                    <i class="fas fa-university text-warning mt-1 me-3 fs-5"></i>
                                    <div>
                                        <strong class="d-block">Proyecto Académico:</strong>
                                        <span class="text-light small">Universidad Técnica de Machala (UTMACH)<br />Ingeniería en Tecnologías de la Información</span>
                                    </div>
                                </li>
                            </ul>
                        </div>

                        <div class="border-top border-secondary pt-3 mt-3">
                            <span class="small text-muted d-block mb-2">Síguenos en redes sociales:</span>
                            <div class="d-flex gap-3 fs-5">
                                <a href="#" class="text-warning"><i class="fab fa-facebook"></i></a>
                                <a href="#" class="text-warning"><i class="fab fa-instagram"></i></a>
                                <a href="#" class="text-warning"><i class="fab fa-linkedin"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- SECCIÓN: NUESTRO EQUIPO DE DESARROLLO -->
        <div class="pt-4 border-top">
            <div class="text-center mb-4">
                <h3 class="fw-bold text-dark">Equipo de Desarrollo</h3>
                <p class="text-muted small">Estudiantes a cargo de la arquitectura y desarrollo del sistema Web de Hotel APM Grand.</p>
            </div>

            <div class="row g-4 justify-content-center">
                <!-- Integrante 1: Anthony Quishpe -->
                <div class="col-md-5 col-lg-4">
                    <div class="card shadow-sm border-0 rounded-3 text-center p-3 h-100">
                        <div class="avatar-frame mt-2">
                            <img src="Assest/Anthony_Samuel_Quishpe_Alvarado.jpeg" alt="Anthony Quishpe" class="avatar-img" />
                        </div>
                        <div class="card-body">
                            <h5 class="fw-bold mb-1 text-dark">Anthony Samuel Quishpe Alvarado</h5>
                            <span class="badge bg-dark text-warning mb-2 px-3 py-1 rounded-pill">Desarrollador Backend & DB</span>
                            <p class="card-text text-muted small mt-2">
                                Estudiante de Ing. en Tecnologías de la Información en la UTMACH. Encargado de la arquitectura de datos, procedimientos almacenados y lógica de negocio.
                            </p>
                        </div>
                        <div class="card-footer bg-transparent border-0 pb-3">
                            <a href="mailto:aquishpe2@utmachala.edu.ec" class="btn btn-outline-dark btn-sm rounded-pill px-3">
                                <i class="fas fa-envelope me-1"></i>Contactar
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Integrante 2: Sleyder Moreno -->
                <div class="col-md-5 col-lg-4">
                    <div class="card shadow-sm border-0 rounded-3 text-center p-3 h-100">
                        <div class="avatar-frame mt-2">
                            <img src="Assest/Sleyder_Dario_Moreno_Crespo.jpeg" alt="Sleyder Moreno" class="avatar-img" />
                        </div>
                        <div class="card-body">
                            <h5 class="fw-bold mb-1 text-dark">Sleyder Darío Moreno Crespo</h5>
                            <span class="badge bg-dark text-warning mb-2 px-3 py-1 rounded-pill">Desarrollador Frontend & UI/UX</span>
                            <p class="card-text text-muted small mt-2">
                                Estudiante de Ing. en Tecnologías de la Información en la UTMACH. Especializado en el diseño de interfaces web responsivas y experiencia de usuario.
                            </p>
                        </div>
                        <div class="card-footer bg-transparent border-0 pb-3">
                            <a href="mailto:smoreno9@utmachala.edu.ec" class="btn btn-outline-dark btn-sm rounded-pill px-3">
                                <i class="fas fa-envelope me-1"></i>Contactar
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>