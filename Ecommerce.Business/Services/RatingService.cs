﻿using AutoMapper;
using Ecommerce.Business.Interfaces;
using Ecommerce.Contracts.Dtos.Rating;
using Ecommerce.DataAccessor.Entities;
using Ecommerce.DataAccessor.Interfaces;
using Ecommerce.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Services
{
    public class RatingService : IRatingService
    {
        private readonly IBaseRepository<Rating> _baseRepository;
        private readonly IMapper _mapper;
        private readonly AspNetIdentityDbContext _aspNetIdentityDbContext;

        public RatingService(IBaseRepository<Rating> baseRepository, IMapper mapper, AspNetIdentityDbContext aspNetIdentityDbContext)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _aspNetIdentityDbContext = aspNetIdentityDbContext;
        }

        public async Task<RatingDto> AddRatingAsync(CreateRatingDto createProductRatingDto)
        {
            Rating productRating = _mapper.Map<Rating>(createProductRatingDto);
            productRating = await _baseRepository.AddAsync(productRating);
            return _mapper.Map<RatingDto>(productRating);
        }
        public async Task<List<RatingDto>> AddRangeRatingAsync(List<CreateRatingDto> createProductRatingDtos)
        {
            IEnumerable<Rating> productRatings = _mapper.Map<IEnumerable<Rating>>(createProductRatingDtos);
            productRatings = await _baseRepository.AddRangeAsync(productRatings);
            return _mapper.Map<List<RatingDto>>(productRatings.ToList());
        }

        public async Task<RatingDto> GetRatingAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.Id == id).Include(p => p.Product).ThenInclude(o => o.ProductImages);
            return _mapper.Map<RatingDto>(await query.FirstOrDefaultAsync());
        }

        public async Task<List<RatingDto>> GetListRatingByProductIdAsync(Guid productId)
        {
            var query = _baseRepository.Entities;
            query = query.Where(o => o.ProductId == productId).Where(o => o.IsRated == true);

            return _mapper.Map<List<RatingDto>>(await query.ToListAsync());
        }
        public async Task RatingAsync(UpdateRatingDto updateProductRatingDto)
        {
            var productRating = await _baseRepository.GetByIdAsync(updateProductRatingDto.Id);
            productRating.Star = updateProductRatingDto.Star;
            productRating.Comment = updateProductRatingDto.Comment;
            productRating.IsRated = updateProductRatingDto.IsRated;
            productRating.UpdatedDate = updateProductRatingDto.UpdateDate;
            await _baseRepository.UpdateAsync(productRating);
        }

        public async Task<int> GetTotalRatingByUserId(string userId)
        {
            return await _baseRepository.Entities.Where(pr => pr.CreatedBy == userId).Where(pr => pr.IsRated).CountAsync();
        }
    }
}
