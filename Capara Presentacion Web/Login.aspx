<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Capara_Presentacion_Web.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Hotel APM Grand - Iniciar Sesión</title>
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(135deg, #0f172a 0%, #1e293b 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 1rem;
        }
        .login-card {
            background-color: #ffffff;
            border-radius: 16px;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.4);
            width: 100%;
            max-width: 420px;
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
            border-radius: 50rem;
            padding: 0.75rem 1.5rem;
            transition: all 0.3s ease;
        }
        .btn-hotel:hover {
            background-color: #d97706;
            color: #ffffff;
            transform: translateY(-1px);
        }
        .btn-outline-hotel {
            border: 2px solid #f59e0b;
            color: #d97706;
            font-weight: 600;
            border-radius: 50rem;
            padding: 0.5rem 1.5rem;
            text-decoration: none;
            display: inline-block;
            transition: all 0.3s ease;
        }
        .btn-outline-hotel:hover {
            background-color: #f59e0b;
            color: #0f172a;
        }
    </style>
</head>
<body>
    <form id="formLogin" runat="server">
        <div class="login-card">
            <!-- Header / Logo -->
            <div class="text-center mb-4">
                <div class="display-5 text-warning mb-2">
                    <i class="fas fa-hotel"></i>
                </div>
                <h3 class="brand-title mb-1">Hotel APM Grand</h3>
                <p class="text-muted small">Portal de Clientes</p>
            </div>

            <!-- Panel de Alertas para Errores -->
            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
                <i class="fas fa-exclamation-circle me-2"></i>
                <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <!-- Campo Email -->
            <div class="mb-3">
                <label for="txtEmail" class="form-label font-weight-bold text-secondary small fw-semibold">
                    <i class="fas fa-envelope me-1"></i>Correo Electrónico
                </label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg fs-6" TextMode="Email" placeholder="ejemplo@correo.com" Required="true"></asp:TextBox>
            </div>

            <!-- Campo Contraseña -->
            <div class="mb-4">
                <label for="txtPassword" class="form-label font-weight-bold text-secondary small fw-semibold">
                    <i class="fas fa-lock me-1"></i>Contraseña
                </label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-lg fs-6" TextMode="Password" placeholder="••••••••" Required="true"></asp:TextBox>
            </div>

            <!-- Botón Principal -->
            <div class="d-grid gap-2 mb-4">
                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn btn-hotel shadow-sm" OnClick="btnLogin_Click" />
            </div>

            <!-- Línea Separadora y Registro -->
            <div class="text-center border-top pt-3">
                <p class="text-muted small mb-2">¿Aún no tienes una cuenta?</p>
                <a href="Registro.aspx" class="btn btn-outline-hotel w-100">
                    <i class="fas fa-user-plus me-2"></i>Crear Cuenta / Registrarse
                </a>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>