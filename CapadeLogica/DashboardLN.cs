using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapadeLogica
{
    public class DashboardLN
    {
        // 1. Obtener KPIs completos del Administrador
        public DashboardKPIsAdmin ObtenerKPIsAdmin()
        {
            try
            {
                sp_Dashboard_KPIsAdminResult obj = DashboardCD.KPIsAdmin();
                if (obj == null) return new DashboardKPIsAdmin();

                return new DashboardKPIsAdmin(
                    obj.IngresosMes ?? 0m,
                    obj.HabitacionesOcupadas ?? 0,
                    obj.HabitacionesDisponibles ?? 0,
                    obj.HabitacionesMantenimiento ?? 0,
                    obj.HabitacionesTotal ?? 0,
                    obj.ReservasMes ?? 0,
                    obj.CancelacionesMes ?? 0,
                    obj.TotalClientes ?? 0,
                    obj.CalificacionPromedio ?? 0m,
                    obj.TotalResenas ?? 0
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al procesar los KPIs del administrador", ex);
            }
        }

        // 2. Obtener los indicadores del turno del Empleado
        public DashboardResumenEmpleado ObtenerResumenEmpleado()
        {
            try
            {
                // LINQ to SQL mapea el primer Result Set por defecto
                List<sp_Dashboard_ResumenEmpleadoResult> auxLista = DashboardCD.ResumenEmpleado();

                if (auxLista != null && auxLista.Count > 0)
                {
                    sp_Dashboard_ResumenEmpleadoResult obj = auxLista.FirstOrDefault();
                    return new DashboardResumenEmpleado(
                        obj.HabitacionesOcupadas ?? 0,
                        obj.HabitacionesTotal ?? 0,
                        obj.ReservasActivas ?? 0,
                        obj.ServiciosHoy ?? 0,
                        obj.CheckoutsHoy ?? 0
                    );
                }

                return new DashboardResumenEmpleado();
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el resumen de turno del empleado", ex);
            }
        }
    }
}