# 🏨 Hotel APM Grand

> Sistema de gestión hotelera completo para el **Hotel APM Grand** — administración de clientes, empleados, reservaciones, facturación y reseñas en una sola plataforma.

---

## 📋 Tabla de Contenidos

- [Descripción General](#descripción-general)
- [Módulos del Sistema](#módulos-del-sistema)
- [Estructura de la Base de Datos](#estructura-de-la-base-de-datos)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Instalación](#instalación)
- [Uso](#uso)
- [Contribución](#contribución)

---

## 📖 Descripción General

**Hotel APM Grand** es un sistema de gestión hotelera diseñado para centralizar y optimizar todas las operaciones del hotel. Desde el momento en que un cliente realiza una reservación hasta que se genera su factura y deja su reseña, el sistema cubre cada etapa del proceso de forma integrada.

---

## 🗂️ Módulos del Sistema

### 👤 Clientes
Gestión completa del perfil de cada huésped del hotel.

| Campo         | Descripción                        |
|--------------|------------------------------------|
| `id_cliente` | Identificador único del cliente     |
| `nombre`     | Nombre completo                    |
| `email`      | Correo electrónico                 |
| `telefono`   | Número de contacto                 |
| `documento`  | Cédula / pasaporte                 |
| `fecha_registro` | Fecha de primer ingreso al sistema |

---

### 👨‍💼 Empleados
Registro del personal que opera el hotel.

| Campo | Descripción |
|---|---|
| `id_empleado` | Identificador único del empleado |
| `nombre` | Nombre completo |
| `cargo` | Puesto o rol (recepcionista, housekeeping, etc.) |
| `turno` | Horario de trabajo |
| `email` | Correo institucional |
| `fecha_contrato` | Fecha de ingreso al hotel |

---

### 📅 Reservaciones
Control de todas las reservas realizadas en el hotel.

| Campo | Descripción |
|---|---|
| `id_reservacion` | Identificador único de la reserva |
| `id_cliente` | Cliente que realizó la reserva |
| `id_habitacion` | Habitación asignada |
| `fecha_entrada` | Fecha de check-in |
| `fecha_salida` | Fecha de check-out |
| `estado` | Pendiente / Activa / Completada / Cancelada |
| `id_empleado` | Empleado que gestionó la reserva |

---

### 🧾 Facturas
Registro financiero de cada estadía del huésped.

| Campo | Descripción |
|---|---|
| `id_factura` | Identificador único de la factura |
| `id_reservacion` | Reservación asociada |
| `id_cliente` | Cliente al que se factura |
| `total` | Monto total a cobrar |
| `fecha_emision` | Fecha de generación de la factura |
| `metodo_pago` | Efectivo / Tarjeta / Transferencia |
| `estado_pago` | Pagado / Pendiente |

---

### ⭐ Reseñas
Opiniones y calificaciones dejadas por los huéspedes tras su estadía.

| Campo | Descripción |
|---|---|
| `id_reseña` | Identificador único de la reseña |
| `id_cliente` | Cliente que dejó la reseña |
| `id_reservacion` | Reservación relacionada |
| `calificacion` | Puntaje del 1 al 5 |
| `comentario` | Opinión escrita del huésped |
| `fecha` | Fecha en que se publicó la reseña |

---

## 🗄️ Estructura de la Base de Datos

```
Hotel APM Grand
│
├── clientes
├── empleados
├── habitaciones
├── reservaciones  ──→  clientes
│                  ──→  habitaciones
│                  ──→  empleados
├── facturas       ──→  reservaciones
│                  ──→  clientes
└── reseñas        ──→  clientes
                   ──→  reservaciones
```

---

## 🛠️ Tecnologías Utilizadas

| Tecnología | Uso |
|---|---|
| **SQL Server 2025** | Motor de base de datos principal |
| **SSMS** | Gestión y administración de la base de datos |
| **T-SQL** | Consultas, procedimientos almacenados y vistas |

---

## ⚙️ Instalación

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/tu-usuario/hotel-apm-grand.git
   ```

2. **Abre SQL Server Management Studio (SSMS)**

3. **Ejecuta el script de creación de la base de datos:**
   ```
   /database/create_db.sql
   ```

4. **Ejecuta el script de inserción de datos de prueba (opcional):**
   ```
   /database/seed_data.sql
   ```

---

## 🚀 Uso

- Registrar clientes y empleados antes de crear reservaciones.
- Una reservación debe existir antes de generar una factura.
- Las reseñas solo pueden ser registradas por clientes con reservaciones completadas.

---

## 🤝 Contribución

Este proyecto es de uso interno para el **Hotel APM Grand**. Para sugerencias o mejoras, contactar al equipo de desarrollo.

---

<div align="center">

**Hotel APM Grand** · Sistema de Gestión Hotelera  
© 2025 — Todos los derechos reservados

</div>