using System;

namespace CapaEntidades.Usuario
{
    public class Usuario
    {
        private int    id;
        private string email;
        private string passwordHash;
        private string rol;
        private bool   activo;
        private DateTime createdAt;
        private DateTime updatedAt;

        public int      Id           { get => id;           set => id           = value; }
        public string   Email        { get => email;        set => email        = value; }
        public string   PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string   Rol          { get => rol;          set => rol          = value; }
        public bool     Activo       { get => activo;       set => activo       = value; }
        public DateTime CreatedAt    { get => createdAt;    set => createdAt    = value; }
        public DateTime UpdatedAt    { get => updatedAt;    set => updatedAt    = value; }

        public Usuario(int id, string email, string passwordHash, string rol,
                       bool activo, DateTime createdAt, DateTime updatedAt)
        {
            this.Id           = id;
            this.Email        = email;
            this.PasswordHash = passwordHash;
            this.Rol          = rol;
            this.Activo       = activo;
            this.CreatedAt    = createdAt;
            this.UpdatedAt    = updatedAt;
        }

        public Usuario() { }
    }
}
