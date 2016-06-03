using System;

namespace EfEntityTest
{
    public class OrderStateHistoryItem
    {
        protected OrderStateHistoryItem()
        {
        }

        public OrderStateHistoryItem(Order order, OrderState state)
        {
            Order = order;
            State = state;
        }
        public virtual long Id { get; set; }
        public virtual Order Order { get; set; }
        public virtual DateTime Created { get; set; } = DateTime.Now;
        public virtual OrderState State { get; set; }
    }
}