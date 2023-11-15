using CadComputadoras2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnComputadoras2
{
    public class CategoriaCln
    {
        public static int insertar(Categoria cliente)
        {
            using (var context = new LabComputadoras2Entities())
            {
                context.Categoria.Add(cliente);
                context.SaveChanges();
                return cliente.id;
            }
        }
        public static int actualizar(Categoria cliente)
        {
            using (var context = new LabComputadoras2Entities())
            {
                var existente = context.Categoria.Find(cliente.id);
                existente.nombre = cliente.nombre;
                existente.descripcion = cliente.descripcion;
                existente.usuarioRegistro = cliente.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabComputadoras2Entities())
            {
                var existente = context.Categoria.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static Categoria get(int id)
        {
            using (var context = new LabComputadoras2Entities())
            {
                return context.Categoria.Find(id);
            }
        }

        public static List<Categoria> listar()
        {
            using (var context = new LabComputadoras2Entities())
            {
                return context.Categoria.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paCategoriaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabComputadoras2Entities())
            {
                return context.paCategoriaListar(parametro).ToList();
            }
        }
    }
}
