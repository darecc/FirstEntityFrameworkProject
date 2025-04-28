using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnityFrameworkProject.Models
{
    /// <summary>
    /// Obiekt Product jest daniem sprzedawanym przez restaurację
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identyfikator produktu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nazwa produktu
        /// </summary>
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(6, 2)")]
        ///<summary>Cena produktu</summary>
        public decimal Price { get; set; }
    }
}
