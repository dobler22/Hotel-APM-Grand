using CapaDatos;
using CapadeDatos;
using HotelAPMGrand.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Resena = HotelAPMGrand.Entidades.Resena;

namespace CapadeLogica
{
    public class ResenaLN
    {
        // 1. Listar reseñas filtrando opcionalmente por calificación mínima (por defecto 1)
        public List<Resena> ListarResenas(int calificacionMinima = 1)
        {
            if (calificacionMinima < 1 || calificacionMinima > 5)
                throw new LogicaExcepciones("La calificación mínima de filtro debe estar entre 1 y 5.");

            List<Resena> lista = new List<Resena>();
            try
            {
                List<sp_Resena_ListarResult> auxLista = ResenaCD.Listar(calificacionMinima);
                if (auxLista != null)
                {
                    foreach (sp_Resena_ListarResult obj in auxLista)
                    {
                        // Nota: sp_Resena_Listar realiza JOIN con Clientes y Habitaciones para mostrar nombres en UI
                        Resena resena = new Resena(
                            0, // No retornado en la consulta resumida
                            0,
                            0,
                            obj.calificacion,
                            obj.comentario ?? "",
                            obj.fecha_resena
                        );
                        lista.Add(resena);
                    }
                }
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al consultar la lista de reseñas.", ex);
            }
            return lista;
        }

        // 2. Obtener una reseña específica por ID
        public Resena ObtenerPorId(int idResena)
        {
            if (idResena <= 0)
                throw new LogicaExcepciones("El ID de la reseña debe ser un número entero positivo.");

            try
            {
                sp_Resena_ObtenerPorIdResult obj = ResenaCD.ObtenerPorId(idResena);
                if (obj == null) return null;

                return new Resena(
                    obj.id_resena,
                    obj.id_reserva,
                    obj.id_cliente,
                    obj.calificacion,
                    obj.comentario ?? "",
                    obj.fecha_resena
                );
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al obtener la reseña por ID.", ex);
            }
        }

        // 3. Obtener el promedio general de calificación y total de reseñas registradas
        public decimal ObtenerPromedioGeneral(out int totalResenas)
        {
            totalResenas = 0;
            try
            {
                sp_Resena_PromedioGeneralResult resultado = ResenaCD.PromedioGeneral();
                if (resultado != null)
                {
                    totalResenas = (int)resultado.TotalResenas;
                    return resultado.PromedioGeneral ?? 0m;
                }
                return 0m;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al calcular el promedio general de reseñas.", ex);
            }
        }

        // 4. Crear una nueva reseña para una reserva realizada por un cliente
        public bool CrearResena(Resena resena)
        {
            if (resena == null)
                throw new LogicaExcepciones("Los datos de la reseña no pueden ser nulos.");

            if (resena.IdReserva <= 0)
                throw new LogicaExcepciones("El ID de la reserva es obligatorio.");

            if (resena.IdCliente <= 0)
                throw new LogicaExcepciones("El ID del cliente es obligatorio.");

            if (resena.Calificacion < 1 || resena.Calificacion > 5)
                throw new LogicaExcepciones("La calificación debe otorgarse en un rango de 1 a 5 estrellas.");

            try
            {
                ResenaCD.Crear(
                    resena.IdReserva,
                    resena.IdCliente,
                    resena.Calificacion,
                    string.IsNullOrWhiteSpace(resena.Comentario) ? null : resena.Comentario.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al guardar la reseña.", ex);
            }
        }

        // 5. Actualizar puntuación y/o comentario de una reseña existente
        public bool ActualizarResena(Resena resena)
        {
            if (resena == null)
                throw new LogicaExcepciones("Los datos de la reseña no pueden ser nulos.");

            if (resena.IdResena <= 0)
                throw new LogicaExcepciones("El ID de la reseña es inválido.");

            if (resena.Calificacion < 1 || resena.Calificacion > 5)
                throw new LogicaExcepciones("La calificación debe estar comprendida entre 1 y 5 estrellas.");

            try
            {
                ResenaCD.Actualizar(
                    resena.IdResena,
                    resena.Calificacion,
                    string.IsNullOrWhiteSpace(resena.Comentario) ? null : resena.Comentario.Trim()
                );
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al actualizar la reseña.", ex);
            }
        }

        // 6. Eliminar una reseña por ID
        public bool EliminarResena(int idResena)
        {
            if (idResena <= 0)
                throw new LogicaExcepciones("El ID de la reseña es inválido.");

            try
            {
                ResenaCD.Eliminar(idResena);
                return true;
            }
            catch (DatosExcepciones ex)
            {
                throw new LogicaExcepciones(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al eliminar la reseña.", ex);
            }
        }
    }
}