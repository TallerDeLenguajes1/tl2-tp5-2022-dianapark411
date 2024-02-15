using System;
using System.Collections.Generic;

namespace WebCadeteria.Models{
    
    public class Usuario
    {
        private int id;
        private string nombre;
        private string user;
        private string passwd;
        private string rol;


        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string User { get => user; set => user = value; }
        public string Passwd { get => passwd; set => passwd = value; }
        public string Rol { get => rol; set => rol = value; }

        public Usuario(){}

        public Usuario(int _id, string _nombre, string _usuario, string _passwd, string _rol){
            Id = _id;
            Nombre = _nombre;
            User = _usuario;
            Passwd = _passwd;
            Rol = _rol;
        }
    }
}