using Amazon.Runtime.Internal;
using Application.Services.Products;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Orders.Constants.OrdersOperationClaims;

namespace Application.Features.Orders.Queries.GetAnalytics;
public class GetAnalyticsOrderQuery: IRequest<GetAnalyticsOrderResponse>, ITransactionalRequest, ISecuredRequest
{
    public string[] Roles => [Admin, Read];

    public class GetAnalyticsOrderQueryHandler: IRequestHandler<GetAnalyticsOrderQuery, GetAnalyticsOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public GetAnalyticsOrderQueryHandler(IOrderRepository orderRepository, IProductService productService, IUserService userService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _userService = userService;
        }

        public async Task<GetAnalyticsOrderResponse> Handle(GetAnalyticsOrderQuery request, CancellationToken cancellationToken)
        {
            GetAnalyticsOrderResponse response = new();
            double montlyIncomeTL = 0;
            double montlyIncomeUSD = 0;

            ICollection<Order>? orders = await _orderRepository.GetAllAsync();
            response.TotalOrder = orders.Count;

            foreach (Order order in orders)
            {
                if(order.CreatedDate.Month == DateTime.Now.Month && order.CreatedDate.Year == DateTime.Now.Year)
                {
                    if (order.IsUsdPrice)
                    {
                        montlyIncomeUSD = Math.Truncate((montlyIncomeUSD + order.OrderPrice) * 100) / 100;
                    }
                    else
                    {
                        montlyIncomeTL = Math.Truncate((montlyIncomeTL + order.OrderPrice) * 100) / 100;
                    }
                }
            }

            response.MonthlyIncomeTL = montlyIncomeTL;
            response.MonthlyIncomeUSD = montlyIncomeUSD;

            IPaginate<User>? users = await _userService.GetListAsync(
                    predicate: u => u.UserState == Domain.Enums.UserState.Confirmed,
                    index:0,
                    size: 1000,
                    cancellationToken:cancellationToken
                );

            response.TotalActiveUser = users!.Items.Count;

            ICollection<Product>? products = await _productService.GetAllAsync(p => p.ProductState == Domain.Enums.ProductState.Active || p.ProductState == ProductState.Showcase);
            response.TotalProduct = products.Count;

            return response;
        }
    }
}
