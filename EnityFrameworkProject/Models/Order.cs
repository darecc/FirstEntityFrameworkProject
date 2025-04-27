namespace EnityFrameworkProject.Models
{
    /// <summary>
    /// Klasa przechowująca zamówienie dla konkretnego klienta sklepu
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Identyfikator zamówienia
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Czas złożenia zamówienia
        /// </summary>
        public DateTime OrderPlaced { get; set; }
        /// <summary>
        /// Czas realizacji zamówienia
        /// </summary>
        public DateTime? OrderFulfiled { get; set; }
        /// <summary>
        /// Identyfikator klienta, który złożył zamówienie
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Klient, który złożył zamówienie
        /// </summary>
        public Customer Customer { get; set; } = null!;
        /// <summary>
        /// Szczegóły zamówienia (nie działa tu mechanizm lazy loading -> potrzebne jest załadowanie szczegółów zamówienia na początku programu
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
    }
}