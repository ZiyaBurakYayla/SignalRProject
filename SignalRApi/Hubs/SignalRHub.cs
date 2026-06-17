using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.MediatR.Queries;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IMediator _mediator;

        public SignalRHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendStatistic()
        {
            var value1 = await _mediator.Send(new GetCategoryCountQuery());
            await Clients.Caller.SendAsync("ReceiveCategoryCount", value1);

            var value2 = await _mediator.Send(new GetProductCountQuery());
            await Clients.Caller.SendAsync("ReceiveProductCount", value2);

            var value3 = await _mediator.Send(new GetActiveCategoryCountQuery());
            await Clients.Caller.SendAsync("ReceiveActiveCategoryCount", value3);

            var value4 = await _mediator.Send(new GetPassiveCategoryCountQuery());
            await Clients.Caller.SendAsync("ReceivePassiveCategoryCount", value4);

            var value5 = await _mediator.Send(new ProductCountByCategoryNameHamburgerQuery());
            await Clients.Caller.SendAsync("ReceiveProductCountByCategoryNameHamburger", value5);

            var value6 = await _mediator.Send(new ProductCountByCategoryNameDrinksQuery());
            await Clients.Caller.SendAsync("ReceiveProductCountByCategoryNameDrinks", value6);

            var value7 = await _mediator.Send(new AverageProductPriceQuery());
            await Clients.Caller.SendAsync("ReceiveAverageProductPrice", value7.ToString("0.00")+"₺");

            var value8 = await _mediator.Send(new HighestPriceProductNameQuery());
            await Clients.Caller.SendAsync("ReceiveHighestPriceProductName", value8);

            var value9 = await _mediator.Send(new LowestPriceProductNameQuery());
            await Clients.Caller.SendAsync("ReceiveLowestPriceProductName", value9);

            var value10 = await _mediator.Send(new AveragePriceByCategoryNameHamburgerQuery());
            await Clients.Caller.SendAsync("ReceiveAveragePriceByCategoryNameHamburger", value10.ToString("0.00") + "₺");

            var value11 = await _mediator.Send(new TotalOrderCountQuery());
            await Clients.Caller.SendAsync("ReceiveTotalOrderCount", value11);

            var value12 = await _mediator.Send(new ActiveOrderCountQuery());
            await Clients.Caller.SendAsync("ReceiveActiveOrderCount", value12);

            var value13 = await _mediator.Send(new LastOrderPriceQuery());
            await Clients.Caller.SendAsync("ReceiveLastOrderPrice", value13.ToString("0.00") + "₺");

            var value14 = await _mediator.Send(new TotalMoneyCaseAmountQuery());
            await Clients.Caller.SendAsync("ReceiveTotalMoneyCaseAmount", value14.ToString("0.00") + "₺");

            var value15 = await _mediator.Send(new TodayTotalPriceQuery());
            await Clients.Caller.SendAsync("ReceiveTodayTotalPrice", value15.ToString("0.00") + "₺");

            var value16 = await _mediator.Send(new OrderTableCountQuery());
            await Clients.Caller.SendAsync("ReceiveOrderTableCount", value16);
        }

        public async Task SendProgress()
        {
            var value1 = await _mediator.Send(new TotalMoneyCaseAmountQuery());
            await Clients.Caller.SendAsync("ReceiveTotalMoneyCaseAmount", value1.ToString("0.00") + "₺");
        }

        public async Task GetBookingList()
        {
            var values = await _mediator.Send(new BookingListQuery());
            await Clients.All.SendAsync("ReceiveBookingList", values);
        }

        public async Task SendNotification()
        {
            var count = await _mediator.Send(new NotificationCountByStatusisFalseQuery());
            await Clients.Caller.SendAsync("ReceiveNotificationCountByStatusisFalse", count);

            var list = await _mediator.Send(new NotificationListByFalseQuery());
            await Clients.Caller.SendAsync("ReceiveNotificationListByFalse", list);
        }

        public async Task GetOrderTableList()
        {
            var values = await _mediator.Send(new OrderTableListQuery());
            await Clients.All.SendAsync("ReceiveOrderTableList", values);
        }

        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public static int ClientCount { get; set; } = 0;
        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
