using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetListAll;
public class GetListAllOrderQuery: IRequest<ICollection<GetListAllOrderListItemDto>>
{
    public class GetListAllOrderQueryHandler: IRequestHandler<GetListAllOrderQuery, ICollection<GetListAllOrderListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private IMapper _mapper;

        public GetListAllOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListAllOrderListItemDto>> Handle(GetListAllOrderQuery request, CancellationToken cancellationToken)
        {
            ICollection<Order>? orders = await _orderRepository.GetAllAsync();

            ICollection<GetListAllOrderListItemDto> response = _mapper.Map<ICollection<GetListAllOrderListItemDto>>(orders);
            return response;
        }
    }
}
