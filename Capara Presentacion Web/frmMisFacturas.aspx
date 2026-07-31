<%@ Page Title="Mis Facturas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMisFacturas.aspx.cs" Inherits="Capara_Presentacion_Web.Facturacion.frmMisFacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <!-- Encabezado -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <div>
                <h2 class="fw-bold mb-1"><i class="fas fa-file-invoice-dollar text-primary me-2"></i>Mis Facturas</h2>
                <p class="text-muted small mb-0">Consulta el estado de tus comprobantes, consumos y pagos realizados.</p>
            </div>
            <div>
                <asp:Button ID="btnRefrescar" runat="server" CssClass="btn btn-outline-primary btn-sm" Text="🔄 Actualizar Lista" OnClick="btnRefrescar_Click" />
            </div>
        </div>

        <!-- Panel de Mensajes / Notificaciones -->
        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mb-4" role="alert">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <!-- GridView con la lista de facturas -->
        <div class="card shadow-sm border-0">
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-hover align-middle mb-0" GridLines="None"
                        DataKeyNames="IdFactura" OnRowCommand="gvFacturas_RowCommand" OnRowDataBound="gvFacturas_RowDataBound"
                        EmptyDataText="No se encontraron comprobantes de pago registrados a su nombre.">
                        
                        <HeaderStyle CssClass="table-light border-bottom text-secondary text-uppercase small fs-7" />
                        
                        <Columns>
                            <%-- N° Factura --%>
                            <asp:TemplateField HeaderText="N° Comprobante">
                                <ItemTemplate>
                                    <span class="fw-bold text-dark">#<%# Eval("IdFactura", "{0:D6}") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- N° Reserva --%>
                            <asp:TemplateField HeaderText="Reserva">
                                <ItemTemplate>
                                    <span class="badge bg-light text-dark border">
                                        <i class="fas fa-bookmark me-1 text-muted"></i>Ref: <%# Eval("IdReserva") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Fecha Emisión --%>
                            <asp:TemplateField HeaderText="Fecha Emisión">
                                <ItemTemplate>
                                    <i class="far fa-calendar-alt me-1 text-muted"></i>
                                    <%# Eval("FechaEmision", "{0:dd/MM/yyyy HH:mm}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Monto Alojamiento --%>
                            <asp:TemplateField HeaderText="Alojamiento">
                                <ItemTemplate>
                                    $<%# Eval("MontoAlojamiento", "{0:N2}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Monto Servicios --%>
                            <asp:TemplateField HeaderText="Servicios">
                                <ItemTemplate>
                                    $<%# Eval("MontoServicios", "{0:N2}") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Total --%>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <strong class="text-dark">$<%# Eval("Total", "{0:N2}") %></strong>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Estado --%>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstadoBadge" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Acciones --%>
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnVerDetalle" runat="server" CommandName="VerDetalle" 
                                        CommandArgument='<%# Eval("IdFactura") %>' 
                                        CssClass="btn btn-sm btn-outline-info me-1" title="Ver Detalle">
                                        <i class="fas fa-eye"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Detalle Factura -->
    <div class="modal fade" id="modalDetalleFactura" tabindex="-1" aria-labelledby="modalDetalleLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content border-0 shadow">
                <div class="modal-header bg-light">
                    <h5 class="modal-title fw-bold" id="modalDetalleLabel">
                        <i class="fas fa-receipt text-primary me-2"></i>Detalle de Factura <asp:Label ID="lblModalIdFactura" runat="server"></asp:Label>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body p-4">
                    <div class="row g-3">
                        <div class="col-6">
                            <span class="text-muted d-block small">Fecha de Emisión</span>
                            <asp:Label ID="lblModalFecha" runat="server" CssClass="fw-semibold"></asp:Label>
                        </div>
                        <div class="col-6">
                            <span class="text-muted d-block small">Estado de Pago</span>
                            <asp:Label ID="lblModalEstado" runat="server"></asp:Label>
                        </div>
                        <hr class="my-2" />
                        <div class="col-12">
                            <div class="d-flex justify-content-between py-1">
                                <span>Subtotal Alojamiento:</span>
                                <asp:Label ID="lblModalAlojamiento" runat="server" CssClass="fw-semibold"></asp:Label>
                            </div>
                            <div class="d-flex justify-content-between py-1">
                                <span>Subtotal Consumos/Servicios:</span>
                                <asp:Label ID="lblModalServicios" runat="server" CssClass="fw-semibold"></asp:Label>
                            </div>
                            <div class="d-flex justify-content-between py-2 mt-2 border-top border-2">
                                <strong class="fs-5">Total Facturado:</strong>
                                <strong class="fs-5 text-primary"><asp:Label ID="lblModalTotal" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="window.print();"><i class="fas fa-print me-1"></i>Imprimir</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Script para desplegar el modal desde servidor -->
    <script type="text/javascript">
function abrirModalDetalle() {
    var myModal = new bootstrap.Modal(document.getElementById('modalDetalleFactura'));
    myModal.show();
        }
    </script>
</asp:Content>