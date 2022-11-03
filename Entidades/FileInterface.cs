using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public interface iCRUDInterface <T>
    {
        public bool Guardar(T item);
        public T Leer(string? nombre, int? id);
        public bool Borrar(int id);
        public bool Modificar(T item);
    }
}
