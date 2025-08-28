namespace Domain.Entities
{
    public  class  Status
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Order> OrdersNav { get; set; }
        public ICollection<OrderItem> OrderItemsNav { get; set; }
    }
}