using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EfEntityTest
{
    public class Order
    {
        private OrderState _state = OrderState.Placed;

        public virtual OrderState State
        {
            get
            {
                return _state;
            }

            protected set { _state = value; }
        }

        public virtual ICollection<OrderStateHistoryItem> OrderStateHistory { get; set; } = new List<OrderStateHistoryItem>();

        public virtual Guid Id { get; set; } = Guid.NewGuid();

        protected virtual void Progress(OrderState state)
        {
            if (OrderStateHistory.Any(o => o.State == state)) return;
            _state = state;
            OrderStateHistory.Add(new OrderStateHistoryItem(this, state));
        }

        public static Expression<Func<Order, bool>>  IsPlaced => order => order.State == OrderState.Placed;
        public static Expression<Func<Order, bool>>  IsAccepted => order => order.State == OrderState.Accepted;
        public static Expression<Func<Order, bool>>  IsDispatched => order => order.State == OrderState.Dispatched;
        public static Expression<Func<Order, bool>>  IsDelivered => order => order.State == OrderState.Delivered;


        public virtual void Accept() => Progress(OrderState.Accepted);

        public virtual void Dispatch() => Progress(OrderState.Dispatched);

        public virtual void Complete() => Progress(OrderState.Delivered);
    }
}