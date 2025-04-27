namespace EnityFrameworkProject.Models
{
    /// <summary>
    /// Klasa określające szczegóły zamówienia dla konkretnego produktu (Product)
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// Identyfikator szczegółów zamówienia
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Liczba (ilość) zamówionego produktu
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Numer zamówienia
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Identyfikator produktu
        /// </summary>
        public int ProductId { get; set; }

        public Order Order { get; set; } = null!;
        /// <summary>
        /// Obiekt zamawianego produktu
        /// </summary>
        public Product Product { get; set; } = null!;
    }
}