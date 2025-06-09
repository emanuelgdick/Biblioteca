
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Socio
    {

        public int Id { get; set; }


        [ForeignKey("IdTipoSocio")]
        public TipoSocio? TipoSocio { get; set; }

        [ForeignKey("IdEstadoSocio")]
        public EstadoSocio? EstadoSocio { get; set; }

        [ForeignKey("IdCategoriaSocio")]
        public CategoriaSocio? CategoriaSocio { get; set; }

        [ForeignKey("IdTipoDocumento")]
        public TipoDocumento? TipoDocumento { get; set; }

        [ForeignKey("IdCalle")]
        public Calle? Calle { get; set; }

        [ForeignKey("IdProfesion")]
        public Profesion? Profesion { get; set; }

        [ForeignKey("IdLocalidad")]
        public Localidad? Localidad { get; set; }
        public int NroSocio { get; set; }
        public string ApeyNom { get; set; }
        public string? Nro { get; set; }
        public string? Piso { get; set; }
        public string? Depto { get; set; }
        public string? TelFijo { get; set; }
        public string? TelCelular { get; set; }
        public string? Email { get; set; }
        public DateTime? Fnac { get; set; }
        public DateTime? FIngreso { get; set; }
        public DateTime? FEgreso { get; set; }
        public string? Observaciones { get; set; }
        public string? Documento { get; set; }
        public bool Vitalicio { get; set; }
        public bool PagaAca { get; set; }
        public bool? Sexo { get; set; }
        public string? foto { get; set; }

    }
}
