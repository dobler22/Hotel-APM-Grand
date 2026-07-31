<%@ Page Title="Mis Reservas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHistorialCliente.aspx.cs" Inherits="Capara_Presentacion_Web.frmHistorialCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-3">
        <!-- Encabezado de la página -->
        <div class="d-flex align-items-center justify-content-between mb-4 pb-2 border-bottom">
            <div>
                <h2 class="fw-bold text-dark mb-1">
                    <i class="fas fa-history text-warning me-2"></i>Historial de Mis Reservas
                </h2>
                <p class="text-muted mb-0">Consulta tus estancias y gestiona tus reservaciones del Hotel APM Grand.</p>
            </div>
            <a href="Default.aspx" class="btn btn-outline-secondary btn-sm rounded-pill">
                <i class="fas fa-arrow-left me-1"></i>Volver al Inicio
            </a>
        </div>

        <!-- Panel de Mensajes / Alertas -->
        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-info alert-dismissible fade show" role="alert">
            <i class="fas fa-info-circle me-2"></i>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <!-- Tarjeta Principal con el GridView -->
        <div class="card shadow-sm border-0 rounded-3">
            <div class="card-header bg-dark text-white py-3">
                <h5 class="mb-0 fw-semibold">
                    <i class="fas fa-list me-2 text-warning"></i>Tus Reservaciones Registradas
                </h5>
            </div>
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvHistorial" runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover align-middle mb-0"
                        GridLines="None"
                        EmptyDataText="No tienes reservaciones registradas en tu historial por el momento."
                        OnRowDataBound="gvHistorial_RowDataBound"
                        OnRowCommand="gvHistorial_RowCommand">

                        <HeaderStyle CssClass="table-light text-secondary border-bottom" />
                        <EmptyDataRowStyle CssClass="text-center text-muted py-5" />

                        <Columns>
                            <%-- ID Reserva --%>
                            <asp:BoundField DataField="id_reserva" HeaderText="# Reserva" ItemStyle-CssClass="fw-bold text-center" HeaderStyle-CssClass="text-center" />

                            <%-- Habitación / Tipo --%>
                            <asp:TemplateField HeaderText="Habitación">
                                <ItemTemplate>
                                    <div class="d-flex align-items-center">
                                        <div class="badge bg-dark me-2 fs-6">
                                            <%# Eval("Habitacion") %>
                                        </div>
                                        <span class="text-capitalize text-muted small">
                                            <%# Eval("tipo") %>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Fecha de Entrada --%>
                            <asp:BoundField DataField="fecha_entrada" HeaderText="Entrada" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />

                            <%-- Fecha de Salida --%>
                            <asp:BoundField DataField="fecha_salida" HeaderText="Salida" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />

                            <%-- Estado --%>
                            <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("estado") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Total Factura --%>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end fw-bold text-success">
                                <ItemTemplate>
                                    <%# Eval("TotalFactura") != null ? string.Format("${0:N2}", Eval("TotalFactura")) : "$0.00" %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Acciones --%>
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAbrirCancelar" runat="server"
                                        CommandName="SolicitarCancelacion"
                                        CommandArgument='<%# Eval("id_reserva") %>'
                                        CssClass="btn btn-outline-danger btn-sm rounded-pill px-3">
                                        <i class="fas fa-ban me-1"></i>Cancelar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL DE CANCELACIÓN DE RESERVA -->
    <div class="modal fade" id="modalCancelarReserva" tabindex="-1" aria-labelledby="modalCancelarLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title fw-bold" id="modalCancelarLabel">
                        <i class="fas fa-exclamation-triangle me-2"></i>Cancelar Reservación
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfIdReservaCancelar" runat="server" />

                    <p class="mb-3">
                        Estás a punto de cancelar la reserva <strong>#<asp:Label ID="lblIdReservaModal" runat="server"></asp:Label></strong>. Por favor, indícanos el motivo:
                    </p>

                    <div class="mb-3">
                        <label for="txtMotivoCancelacion" class="form-label fw-semibold">Motivo de la cancelación <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMotivoCancelacion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Ej. Cambio de planes de viaje, motivos de salud, etc."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-secondary rounded-pill" data-bs-dismiss="modal">Volver</button>
                    <asp:Button ID="btnConfirmarCancelacion" runat="server" Text="Confirmar Cancelación" CssClass="btn btn-danger rounded-pill" OnClick="btnConfirmarCancelacion_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Script para abrir el Modal desde C# -->
    <script type="text/javascript">
        function AbrirModalCancelacion() {
            var myModal = new bootstrap.Modal(document.getElementById('modalCancelarReserva'));
            myModal.show();
        }
    </script>
</asp:Content>