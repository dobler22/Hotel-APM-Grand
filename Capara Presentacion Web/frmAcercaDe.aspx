<%@ Page Title="Acerca de Nosotros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAcercaDe.aspx.cs" Inherits="Capara_Presentacion_Web.frmAcercaDe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Hero Section / Banner principal -->
    <div class="bg-primary text-white py-5 mb-5 rounded-3 shadow-sm position-relative overflow-hidden">
        <div class="container position-relative z-1 py-3 text-center">
            <span class="badge bg-light text-primary text-uppercase px-3 py-2 fw-bold mb-3">Hotel APM Grand</span>
            <h1 class="display-4 fw-bold mb-3">Hospitalidad de Excelencia</h1>
            <p class="lead max-w-700 mx-auto opacity-90">
                Diseñado para brindar experiencias inolvidables, combinando elegancia, confort y la mejor atención tecnológica para nuestros huéspedes.
            </p>
        </div>
    </div>

    <div class="container">
        <!-- Sección de Historia / Misión -->
        <div class="row align-items-center mb-5 g-4">
            <div class="col-lg-6">
                <div class="pe-lg-3">
                    <span class="text-primary fw-bold text-uppercase small">Nuestra Filosofía</span>
                    <h2 class="fw-bold my-2 text-dark">Un espacio concebido para el descanso y la tranquilidad</h2>
                    <p class="text-muted">
                        En <strong>Hotel APM Grand</strong>, nos dedicamos a transformar cada estadía en una vivencia memorable. Desde nuestra fundación, nos hemos caracterizado por integrar estándares de atención de primera categoría con instalaciones modernas pensadas tanto para viajeros de negocios como de descanso.
                    </p>
                    <p class="text-muted">
                        Nuestra plataforma digital permite a los huéspedes gestionar sus reservas, revisar sus consumos y acceder a sus comprobantes con total transparencia y facilidad desde cualquier dispositivo.
                    </p>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="card border-0 shadow-sm p-4 bg-light">
                    <div class="row g-3">
                        <div class="col-6">
                            <div class="p-3 bg-white rounded shadow-xs text-center">
                                <h3 class="fw-bold text-primary mb-1">100%</h3>
                                <p class="text-muted small mb-0">Satisfacción Garantizada</p>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="p-3 bg-white rounded shadow-xs text-center">
                                <h3 class="fw-bold text-primary mb-1">24/7</h3>
                                <p class="text-muted small mb-0">Atención al Cliente</p>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="p-3 bg-white rounded shadow-xs text-center">
                                <h3 class="fw-bold text-primary mb-1">+50</h3>
                                <p class="text-muted small mb-0">Habitaciones Equipadas</p>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="p-3 bg-white rounded shadow-xs text-center">
                                <h3 class="fw-bold text-primary mb-1">Premium</h3>
                                <p class="text-muted small mb-0">Servicios Incluidos</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <hr class="my-5 opacity-25" />

        <!-- Tarjetas de Características / Valores -->
        <div class="text-center mb-5">
            <span class="text-primary fw-bold text-uppercase small">¿Por qué elegirnos?</span>
            <h2 class="fw-bold text-dark">Lo que nos distingue</h2>
        </div>

        <div class="row g-4 mb-5">
            <!-- Valor 1 -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm text-center p-4">
                    <div class="card-body">
                        <div class="icon-shape bg-primary-subtle text-primary rounded-circle mb-3 mx-auto d-flex align-items-center justify-content-center" style="width: 60px; height: 60px;">
                            <i class="fas fa-concierge-bell fa-lg"></i>
                        </div>
                        <h5 class="fw-bold text-dark">Atención Personalizada</h5>
                        <p class="text-muted small mb-0">
                            Nuestro equipo está capacitado para responder a tus requerimientos en todo momento con amabilidad y eficiencia.
                        </p>
                    </div>
                </div>
            </div>

            <!-- Valor 2 -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm text-center p-4">
                    <div class="card-body">
                        <div class="icon-shape bg-primary-subtle text-primary rounded-circle mb-3 mx-auto d-flex align-items-center justify-content-center" style="width: 60px; height: 60px;">
                            <i class="fas fa-wifi fa-lg"></i>
                        </div>
                        <h5 class="fw-bold text-dark">Conectividad & Confort</h5>
                        <p class="text-muted small mb-0">
                            Wi-Fi de alta velocidad, zonas de trabajo en habitación y ambientes climatizados para máxima comodidad.
                        </p>
                    </div>
                </div>
            </div>

            <!-- Valor 3 -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm text-center p-4">
                    <div class="card-body">
                        <div class="icon-shape bg-primary-subtle text-primary rounded-circle mb-3 mx-auto d-flex align-items-center justify-content-center" style="width: 60px; height: 60px;">
                            <i class="fas fa-shield-alt fa-lg"></i>
                        </div>
                        <h5 class="fw-bold text-dark">Gestión Transparente</h5>
                        <p class="text-muted small mb-0">
                            Acceso directo a tus comprobantes, desgloses detallados de hospedaje y pagos de forma segura desde tu portal.
                        </p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Banner / Llamado a la acción -->
        <div class="bg-light p-4 p-md-5 rounded-3 mb-4 text-center">
            <h3 class="fw-bold text-dark mb-2">¿Tienes alguna pregunta sobre tu estancia?</h3>
            <p class="text-muted mb-4">Nuestro equipo de soporte está listo para asistirte en la planificación de tu viaje.</p>
            <a href="frmMisFacturas.aspx" class="btn btn-primary px-4 me-2"><i class="fas fa-receipt me-1"></i>Ver Mis Facturas</a>
            <a href="#" class="btn btn-outline-secondary px-4"><i class="fas fa-envelope me-1"></i>Contáctanos</a>
        </div>
    </div>
</asp:Content>