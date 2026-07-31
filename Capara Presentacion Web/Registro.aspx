<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Capara_Presentacion_Web.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Hotel APM Grand - Crear Cuenta</title>
    <!-- Bootstrap CSS CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <style>
        body {
            background: linear-gradient(135deg, #0f172a 0%, #1e293b 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 2rem 0;
        }
        .register-card {
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.3);
            width: 100%;
            max-width: 500px;
            padding: 2.5rem;
        }
        .brand-title {
            color: #0f172a;
            font-weight: 700;
            letter-spacing: -0.5px;
        }
        .btn-hotel {
            background-color: #f59e0b;
            color: #0f172a;
            font-weight: 600;
            border: none;
            transition: all 0.3s ease;
        }
        .btn-hotel:hover {
            background-color: #d97706;
            color: #ffffff;
        }
    </style>
</head>
<body>
    <form id="formRegistro" runat="server">
        <div class="register-card">
            <div class="text-center mb-4">
                <span class="fs-1">🏨</span>
                <h3 class="brand-title mt-2">Hotel APM Grand</h3>
                <p class="text-muted fs-7">Crea tu cuenta de Cliente</p>
            </div>

            <!-- Panel de Alertas para Errores -->
            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
                <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
            </asp:Panel>

            <!-- Panel de Alertas para Éxito -->
            <asp:Panel ID="pnlExito" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
                <asp:Label ID="lblMensajeExito" runat="server"></asp:Label>
            </asp:Panel>

            <div class="mb-3">
                <label for="txtCedula" class="form-label font-weight-bold text-secondary">Cédula / Identificación</label>
                <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control" placeholder="1001234567" Required="true"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="txtNombre" class="form-label font-weight-bold text-secondary">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Juan" Required="true"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label for="txtApellido" class="form-label font-weight-bold text-secondary">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Pérez" Required="true"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3">
                <label for="txtTelefono" class="form-label font-weight-bold text-secondary">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="0991234567" Required="true"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label font-weight-bold text-secondary">Correo Electrónico</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="ejemplo@correo.com" Required="true"></asp:TextBox>
            </div>

            <div class="mb-4">
                <label for="txtPassword" class="form-label font-weight-bold text-secondary">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••••" Required="true"></asp:TextBox>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-hotel btn-lg" OnClick="btnRegistrar_Click" />
            </div>

            <div class="text-center mt-4">
                <small class="text-muted">¿Ya tienes una cuenta? <a href="Login.aspx" class="text-warning fw-bold text-decoration-none">Inicia Sesión aquí</a></small>
            </div>
        </div>
    </form>
</body>
</html>