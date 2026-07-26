using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Mantenimiento = HotelAPMGrand.Entidades.Mantenimiento;

namespace CapadeLogica
{
    public class MantenimientoLN
    {
        public List<Mantenimiento> ListarMantenimientos()
        {
            List<Mantenimiento> lista = new List<Mantenimiento>();
            try
            {
                List<sp_Mantenimiento_ListarResult> auxLista = MantenimientoCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Mantenimiento_ListarResult obj in auxLista)
                    {
                        Mantenimiento mantenimiento = new Mantenimiento(
                            obj.id_mantenimiento,
                            0,
                            0,
                            obj.tipo_trabajo ?? "",
                            obj.fecha_inicio,
                            obj.fecha_fin ?? DateTime.MinValue,
                            obj.estado ?? "",
                            obj.observaciones ?? ""
                        );
                        lista.Add(mantenimiento);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar la lista general de mantenimientos.", ex);
            }
            return lista;
        }

        // 2. Listar únicamente las tareas de mantenimiento que estén 'en_progreso'
        public List<Mantenimiento> ListarMantenimientosEnProgreso()
        {
            List<Mantenimiento> lista = new List<Mantenimiento>();
            try
            {
                List<sp_Mantenimiento_EnProgresoResult> auxLista = MantenimientoCD.ListarEnProgreso();
                if (auxLista != null)
                {
                    foreach (sp_Mantenimiento_EnProgresoResult obj in auxLista)
                    {
                        Mantenimiento mantenimiento = new Mantenimiento(
                            obj.id_mantenimiento,
                            0,
                            0,
                            obj.tipo_trabajo ?? "",
                            obj.fecha_inicio,
                            DateTime.MinValue,
                            obj.estado ?? "en_progreso",
                            ""
                        );
                        lista.Add(mantenimiento);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar los mantenimientos en progreso.", ex);
            }
            return lista;
        }

        // 3. Obtener un registro de mantenimiento completo por ID
        public Mantenimiento ObtenerPorId(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
                throw new LogicaExcepciones("El ID del mantenimiento debe ser un número entero positivo.");

            try
            {
                sp_Mantenimiento_ObtenerPorIdResult obj = MantenimientoCD.ObtenerPorId(idMantenimiento);
                if (obj == null) return null;

                return new Mantenimiento(
                    obj.id_mantenimiento,
                    obj.id_habitacion,
                    obj.id_empleado,
                    obj.tipo_trabajo ?? "",
                    obj.fecha_inicio,
                    obj.fecha_fin ?? DateTime.MinValue,
                    obj.estado ?? "",
                    obj.observaciones ?? ""
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener el detalle del mantenimiento por ID.", ex);
            }
        }

        // 4. Crear un nuevo registro de mantenimiento (Actualiza la habitación a 'mantenimiento')
        public bool CrearMantenimiento(Mantenimiento mantenimiento)
        {
            if (mantenimiento == null)
                throw new LogicaExcepciones("Los datos del mantenimiento no pueden ser nulos.");

            if (mantenimiento.IdHabitacion <= 0)
                throw new LogicaExcepciones("Debe seleccionar una habitación válida.");

            if (mantenimiento.IdEmpleado <= 0)
                throw new LogicaExcepciones("Debe asignar un empleado responsable válido.");

            if (string.IsNullOrWhiteSpace(mantenimiento.TipoTrabajo))
                throw new LogicaExcepciones("El tipo de trabajo de mantenimiento es obligatorio.");

            if (mantenimiento.FechaInicio == DateTime.MinValue)
                mantenimiento.FechaInicio = DateTime.Now;

            try
            {
                MantenimientoCD.Crear(
                    mantenimiento.IdHabitacion,
                    mantenimiento.IdEmpleado,
                    mantenimiento.TipoTrabajo.Trim(),
                    mantenimiento.FechaInicio,
                    string.IsNullOrWhiteSpace(mantenimiento.Observaciones) ? null : mantenimiento.Observaciones.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar la orden de mantenimiento.", ex);
            }
        }

        // 5. Actualizar tipo de trabajo y observaciones de un mantenimiento existente
        public bool ActualizarMantenimiento(Mantenimiento mantenimiento)
        {
            if (mantenimiento == null)
                throw new LogicaExcepciones("Los datos del mantenimiento no pueden ser nulos.");

            if (mantenimiento.IdMantenimiento <= 0)
                throw new LogicaExcepciones("El ID del mantenimiento es inválido.");

            if (string.IsNullOrWhiteSpace(mantenimiento.TipoTrabajo))
                throw new LogicaExcepciones("El tipo de trabajo no puede estar vacío.");

            try
            {
                MantenimientoCD.Actualizar(
                    mantenimiento.IdMantenimiento,
                    mantenimiento.TipoTrabajo.Trim(),
                    string.IsNullOrWhiteSpace(mantenimiento.Observaciones) ? null : mantenimiento.Observaciones.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información del mantenimiento.", ex);
            }
        }

        // 6. Finalizar la orden de mantenimiento (Cambia estado a 'completado' y la habitación a 'disponible')
        public bool FinalizarMantenimiento(int idMantenimiento, DateTime? fechaFin = null)
        {
            if (idMantenimiento <= 0)
                throw new LogicaExcepciones("El ID del mantenimiento es inválido.");

            DateTime fechaCierre = fechaFin.HasValue ? fechaFin.Value : DateTime.Now;

            try
            {
                MantenimientoCD.Finalizar(idMantenimiento, fechaCierre);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al finalizar la orden de mantenimiento.", ex);
            }
        }

        // 7. Eliminar registro de mantenimiento
        public bool EliminarMantenimiento(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
                throw new LogicaExcepciones("El ID del mantenimiento es inválido.");

            try
            {
                MantenimientoCD.Eliminar(idMantenimiento);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar el registro de mantenimiento.", ex);
            }
        }
    }
}