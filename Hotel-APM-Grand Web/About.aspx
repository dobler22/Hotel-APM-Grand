<%@ Page Title="Acerca de" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Presentacion.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-3">
        <div class="row align-items-center mb-5">
            <div class="col-md-7">
                <h1 class="fw-bold text-dark mb-3">Sobre <span class="text-warning">Hotel APM Grand</span></h1>
                <p class="lead text-muted">
                    Sistema integral de gestión hotelera diseñado para optimizar el control de reservas, 
                    gestión de habitaciones y atención a clientes en tiempo real.
                </p>
                <p>
                    Proyecto académico desarrollado para la materia de desarrollo de software, implementando 
                    arquitectura en capas (Entidades, Datos, Lógica y Presentación) para garantizar un sistema escalable y mantenible.
                </p>
            </div>
            <div class="col-md-5 text-center">
                <div class="p-4 bg-light rounded-3 border shadow-sm">
                    <i class="fas fa-hotel fa-5x text-warning mb-3"></i>
                    <h4 class="fw-bold">Hotel APM Grand</h4>
                    <span class="badge bg-dark">Versión 1.0</span>
                </div>
            </div>
        </div>

        <!-- SECCIÓN DEL EQUIPO DE DESARROLLO -->
        <h3 class="fw-bold mb-4 text-center">Equipo de Desarrollo</h3>
        <div class="row g-4 justify-content-center">
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm text-center p-3">
                    <div class="card-body">
                        <i class="fas fa-user-graduate fa-3x text-secondary mb-3"></i>
                        <h5 class="card-title fw-bold">Estudiante / Desarrollador</h5>
                        <p class="card-text text-muted">Desarrollo Frontend & Backend</p>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>