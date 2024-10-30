using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JordyAguilar_ExamenP1.Models;

namespace JordyAguilar_ExamenP1.Data
{
    public class JordyAguilar_ExamenP1Context : DbContext
    {
        public JordyAguilar_ExamenP1Context (DbContextOptions<JordyAguilar_ExamenP1Context> options)
            : base(options)
        {
        }

        public DbSet<JordyAguilar_ExamenP1.Models.JAguilar> JAguilar { get; set; } = default!;
        public DbSet<JordyAguilar_ExamenP1.Models.Celulares> Celulares { get; set; } = default!;
    }
}
