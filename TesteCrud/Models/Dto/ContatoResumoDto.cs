using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteCrud.Models.Dto
{
    public class ContatoResumoDto
    {
        public char Sexo { get; set; }
        public string Cidade { get; set; }
        public int TotalContatos { get; set; }
        public int TotalPorSexo { get; set; }
        public string Mes { get; set; }

    }
}
