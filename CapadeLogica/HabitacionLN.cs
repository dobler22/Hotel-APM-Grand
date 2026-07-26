using CapaDatos;
using CapadeDatos;
using CapaEntidades.Habitacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habitacion = CapaEntidades.Habitacion.Habitacion;

namespace CapadeLogica
{
    public class HabitacionLN
    {
        // 1. Listar todas las habitaciones
        public List<Habitacion> ListarHabitaciones()
        {
            List<Habitacion> lista = new List<Habitacion>();
            try
            {
                List<sp_Habitacion_ListarResult> auxLista = HabitacionCD.Listar();
                if (auxLista != null)
                {
                    foreach (sp_Habitacion_ListarResult obj in auxLista)
                    {
                        Habitacion hab = new Habitacion(
                            obj.id_habitacion,
                            obj.numero ?? "",
                            obj.tipo ?? "",
                            obj.capacidad,
                            obj.piso,
                            obj.precio_por_noche,
                            obj.estado ?? ""
                        );
                        lista.Add(hab);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar la lista de habitaciones", ex);
            }
            return lista;
        }

        // 2. Listar habitaciones disponibles por rango de fechas y/o tipo
        public List<Habitacion> ListarHabitacionesDisponibles(DateTime? entrada, DateTime? salida, string tipo)
        {
            if (entrada.HasValue && salida.HasValue && entrada.Value >= salida.Value)
                throw new LogicaExcepciones("La fecha de entrada debe ser anterior a la fecha de salida.");

            List<Habitacion> lista = new List<Habitacion>();
            try
            {
                string tipoFiltro = string.IsNullOrWhiteSpace(tipo) ? null : tipo.Trim();
                List<sp_Habitacion_DisponiblesResult> auxLista = HabitacionCD.ListarDisponibles(entrada, salida, tipoFiltro);

                if (auxLista != null)
                {
                    foreach (sp_Habitacion_DisponiblesResult obj in auxLista)
                    {
                        Habitacion hab = new Habitacion(
                            obj.id_habitacion,
                            obj.numero ?? "",
                            obj.tipo ?? "",
                            obj.capacidad,
                            obj.piso,
                            obj.precio_por_noche,
                            "disponible" // Cambiado obj.estado por la cadena fija "Disponible"
                        );
                        lista.Add(hab);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar habitaciones disponibles", ex);
            }
            return lista;
        }

        // 3. Obtener habitación por ID
        public Habitacion ObtenerPorId(int idHabitacion)
        {
            if (idHabitacion <= 0)
                throw new LogicaExcepciones("El ID de la habitación debe ser un número positivo.");

            try
            {
                sp_Habitacion_ObtenerPorIdResult obj = HabitacionCD.ObtenerPorId(idHabitacion);
                if (obj == null) return null;

                return new Habitacion(
                    obj.id_habitacion,
                    obj.numero ?? "",
                    obj.tipo ?? "",
                    obj.capacidad,
                    obj.piso,
                    obj.precio_por_noche,
                    obj.estado ?? ""
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener la habitación por ID", ex);
            }
        }

        // 4. Crear nueva habitación
        public bool CrearHabitacion(Habitacion habitacion)
        {
            if (habitacion == null)
                throw new LogicaExcepciones("Los datos de la habitación no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(habitacion.Numero))
                throw new LogicaExcepciones("El número de habitación es obligatorio.");

            if (string.IsNullOrWhiteSpace(habitacion.Tipo))
                throw new LogicaExcepciones("El tipo de habitación es obligatorio.");

            if (habitacion.Capacidad <= 0)
                throw new LogicaExcepciones("La capacidad debe ser mayor a 0.");

            if (habitacion.Piso <= 0)
                throw new LogicaExcepciones("El número de piso debe ser mayor a 0.");

            if (habitacion.PrecioPorNoche <= 0)
                throw new LogicaExcepciones("El precio por noche debe ser mayor a 0.");

            try
            {
                HabitacionCD.Crear(
                    habitacion.Numero.Trim(),
                    habitacion.Tipo.Trim(),
                    habitacion.Capacidad,
                    habitacion.Piso,
                    habitacion.PrecioPorNoche
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al registrar la nueva habitación", ex);
            }
        }

        // 5. Actualizar información de habitación (Tipo, Capacidad y Precio)
        public bool ActualizarHabitacion(Habitacion habitacion)
        {
            if (habitacion == null)
                throw new LogicaExcepciones("Los datos de la habitación no pueden ser nulos.");

            if (habitacion.IdHabitacion <= 0)
                throw new LogicaExcepciones("El ID de la habitación es inválido.");

            if (string.IsNullOrWhiteSpace(habitacion.Tipo))
                throw new LogicaExcepciones("El tipo de habitación es obligatorio.");

            if (habitacion.Capacidad <= 0)
                throw new LogicaExcepciones("La capacidad debe ser mayor a 0.");

            if (habitacion.PrecioPorNoche <= 0)
                throw new LogicaExcepciones("El precio por noche debe ser mayor a 0.");

            try
            {
                HabitacionCD.Actualizar(
                    habitacion.IdHabitacion,
                    habitacion.Tipo.Trim(),
                    habitacion.Capacidad,
                    habitacion.PrecioPorNoche
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la información de la habitación", ex);
            }
        }

        // 6. Cambiar estado de la habitación
        public bool CambiarEstadoHabitacion(int idHabitacion, string estado)
        {
            if (idHabitacion <= 0)
                throw new LogicaExcepciones("El ID de la habitación es inválido.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new LogicaExcepciones("El estado de la habitación es obligatorio.");

            try
            {
                HabitacionCD.CambiarEstado(idHabitacion, estado.Trim());
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al cambiar el estado de la habitación", ex);
            }
        }

        // 7. Eliminar habitación
        public bool EliminarHabitacion(int idHabitacion)
        {
            if (idHabitacion <= 0)
                throw new LogicaExcepciones("El ID de la habitación es inválido.");

            try
            {
                HabitacionCD.Eliminar(idHabitacion);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar la habitación", ex);
            }
        }




    }
}
