﻿using Application.DataTransfers.Request;
using Application.Exceptions;
using Application.Interfaces.DishInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class DishValidator : IDishValidator
    {
        private readonly IDishQuery _query;

        public DishValidator(IDishQuery query)
        {
            _query = query;
        }

        public async Task ValidateCreate(DishRequest request)
        {
            var category = await _query.GetCategoryById(request.Category);
            if (category == null)
                throw new CategoryNotFoundException("La categoria no existe");

            var dishConNombre = await _query.GetAllDish(name: request.Name);
            if (dishConNombre.Any())
                throw new DishNameAlreadyExistsException("Ya existe un platillo con ese nombre");

            if (request.Price <= 0)
                throw new InvalidDishPriceException("El precio del platillo debe ser mayor a 0");
        }

        public async Task ValidateUpdate(Guid idDish, DishRequest request)
        {
            if(await _query.GetDishById(idDish) == null)
                throw new DishNotFoundException("El platillo no existe");
            await  ValidateCreate(request);
        }
    }
}
