using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteCrud.Models
{
    public class Contato
    {
        [Required]
        public string Nome { get; set; }
        public char Sexo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Cidade { get; set; }
        public int Id { get; set; }

        public Contato(string nome, char sexo, DateTime data, string cidade, int id)
        {
            Nome = nome;
            Sexo = sexo;
            Data = data;
            Cidade = cidade;
            Id = id;
        }

        public Contato()
        {
        }
    }

}
