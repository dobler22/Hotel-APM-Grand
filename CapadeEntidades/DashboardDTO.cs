using System;
using System.Collections.Generic;

namespace HotelAPMGrand.Entidades
{
    // DTO para los KPIs del Administrador
    public class DashboardKPIsAdmin
    {
        public decimal IngresosMes { get; set; }
        public int HabitacionesOcupadas { get; set; }
        public int HabitacionesDisponibles { get; set; }
        public int HabitacionesMantenimiento { get; set; }
        public int HabitacionesTotal { get; set; }
        public int ReservasMes { get; set; }
        public int CancelacionesMes { get; set; }
        public int TotalClientes { get; set; }
        public decimal CalificacionPromedio { get; set; }
        public int TotalResenas { get; set; }

        public DashboardKPIsAdmin(decimal ingresosMes, int habitacionesOcupadas, int habitacionesDisponibles,
                                  int habitacionesMantenimiento, int habitacionesTotal, int reservasMes,
                                  int cancelacionesMes, int totalClientes, decimal calificacionPromedio,
                                  int totalResenas)
        {
            IngresosMes = ingresosMes;
            HabitacionesOcupadas = habitacionesOcupadas;
            HabitacionesDisponibles = habitacionesDisponibles;
            HabitacionesMantenimiento = habitacionesMantenimiento;
            HabitacionesTotal = habitacionesTotal;
            ReservasMes = reservasMes;
            CancelacionesMes = cancelacionesMes;
            TotalClientes = totalClientes;
            CalificacionPromedio = calificacionPromedio;
            TotalResenas = totalResenas;
        }

        public DashboardKPIsAdmin() { }
    }

    // DTO para el resumen numérico del Empleado (Métricas del turno)
    public class DashboardResumenEmpleado
    {
        public int HabitacionesOcupadas { get; set; }
        public int HabitacionesTotal { get; set; }
        public int ReservasActivas { get; set; }
        public int ServiciosHoy { get; set; }
        public int CheckoutsHoy { get; set; }

        public DashboardResumenEmpleado(int habitacionesOcupadas, int habitacionesTotal,
                                        int reservasActivas, int serviciosHoy, int checkoutsHoy)
        {
            HabitacionesOcupadas = habitacionesOcupadas;
            HabitacionesTotal = habitacionesTotal;
            ReservasActivas = reservasActivas;
            ServiciosHoy = serviciosHoy;
            CheckoutsHoy = checkoutsHoy;
        }

        public DashboardResumenEmpleado() { }
    }

    // DTO para las Reservas del Día que ve el Empleado
    public class ReservaDiaDTO
    {
        public int IdReserva { get; set; }
        public string Cliente { get; set; }
        public string Habitacion { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string Estado { get; set; }

        public ReservaDiaDTO(int idReserva, string cliente, string habitacion,
                             string tipo, DateTime fechaEntrada, DateTime fechaSalida, string estado)
        {
            IdReserva = idReserva;
            Cliente = cliente;
            Habitacion = habitacion;
            Tipo = tipo;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            Estado = estado;
        }

        public ReservaDiaDTO() { }
    }
}