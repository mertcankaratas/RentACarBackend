using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetlListModelByDynamicQuery:IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListModelByDynamicQueryHandler : IRequestHandler<GetlListModelByDynamicQuery, ModelListModel>
        {
            private readonly IModelRepository _modelRepository;
            private readonly IMapper _mapper;

            public GetListModelByDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
            }

            public async Task<ModelListModel> Handle(GetlListModelByDynamicQuery request, CancellationToken cancellationToken)
            {
                //car models
                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,include:
                     m => m.Include(c => c.Brand),
                     index: request.PageRequest.Page,
                     size: request.PageRequest.PageSize
                     );


                //data model

                ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);

                return mappedModels;

            }
        }
    }
}
