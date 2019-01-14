using Shop.Infrastructure.Data;
using Shop.Services.Dto;
using Shop.Services.Dto.Products;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Services.Interfaces.Exceptions;
using Shop.Domain.Core;

namespace Shop.Infrastructure.Business.Services
{
    public class AttributesService : IAttributesService
    {
        private readonly ApplicationDbContext _dbContext;

        public AttributesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AttributeUnitDto> GetAttributeUnitAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var attributeUnit = await _dbContext.AttributeUnits
                .Select(b => new AttributeUnitDto
                {
                    Id = b.Id,
                    Name = b.Name
                }).FirstOrDefaultAsync(cancellationToken);

            if (attributeUnit == null)
            {
                throw new ItemNotFoundException(unitId, "Attribute unit");
            }

            return attributeUnit;
        }

        public async Task<IEnumerable<AttributeUnitDto>> GetAttributeUnitsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.AttributeUnits
                .Select(b => new AttributeUnitDto
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToListAsync(cancellationToken);
        }

        public async Task AddAttributeUnitAsync(AttributeUnitDto unit, CancellationToken cancellationToken = default(CancellationToken))
        {
            var a = new AttributeUnit
            {                
                Name = unit.Name,                
            };

            try
            {
                _dbContext.Add(a);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(a, ex);
            }
        }

        public async Task UpdateAttributeUnitAsync(int unitId, AttributeUnitDto unit, CancellationToken cancellationToken = default(CancellationToken))
        {
            var b = new AttributeUnit
            {
                Id = unitId,
                Name = unit.Name                
            };
            _dbContext.Attach(b).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(unitId, "Attribute unit", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(b, ex);
            }
        }

        public async Task DeleteAttributeUnitAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var attribute = new AttributeUnit
            {
                Id = unitId
            };

            _dbContext.Attach(attribute).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(unitId, "Attribute unit", ex);
            }
        }


        public async Task<AttributeDto> GetAttributeAsync(int unitId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var attribute = await _dbContext.Attributes
                .Select(a => new AttributeDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    UnitId = a.UnitId
                }).FirstOrDefaultAsync(cancellationToken);

            if (attribute == null)
            {
                throw new ItemNotFoundException(unitId, "Attribute");
            }

            return attribute;
        }

        public async Task<IEnumerable<AttributeDto>> GetAttributesAsync(PaginationDto pagination, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbContext.Attributes
                .Select(a => new AttributeDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    UnitId = a.UnitId
                }).ToListAsync(cancellationToken);
        }

        public async Task AddAttributeAsync(AttributeDto attribute, CancellationToken cancellationToken = default(CancellationToken))
        {
            var a = new Domain.Core.Attribute
            {
                Name = attribute.Name,
                UnitId = attribute.UnitId
            };

            try
            {
                _dbContext.Add(a);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(a, ex);
            }
        }

        public async Task UpdateAttributeAsync(int attributeId, AttributeDto attribute, CancellationToken cancellationToken = default(CancellationToken))
        {
            var b = new Domain.Core.Attribute
            {
                Id = attributeId,
                Name = attribute.Name,
                UnitId = attribute.UnitId
            };
            _dbContext.Attach(b).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(attributeId, "Attribute unit", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidItemException(b, ex);
            }
        }

        public async Task DeleteAttributeAsync(int attributeId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var attribute = new Domain.Core.Attribute
            {
                Id = attributeId
            };

            _dbContext.Attach(attribute).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ItemNotFoundException(attributeId, "Attribute", ex);
            }
        }

    }
}
