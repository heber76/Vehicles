using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Models;

namespace Vehicles.API.Helpers
{
    public class ConvertertHelper : IConvertertHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConvertertHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<User> ToUserAsync(UserViewModel model, Guid imageId, bool IsNew)
        {

            return new User
            {
                Address = model.Address,
                Document = model.Document,
                DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId),
                Email = model.Email,
                FirstName = model.FirstName,
                Id = IsNew ? Guid.NewGuid().ToString() : model.Id,
                ImageId = imageId,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                UserType = model.UserType,
                
                
            };

        }

        public UserViewModel ToUserViewModel(User user)
        {
          return  new UserViewModel
            {

                Address = user.Address,
                Document = user.Document,
                //DocumentType = user.DocumentType,
                DocumentTypeId = user.DocumentType.Id,
                DocumentTypes = _combosHelper.GetComboDocumnetTypes(),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                ImageId = user.ImageId,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
            };
        }

        public async Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool IsNew)
        {
            return new Vehicle
            {
                Brand = await _context.Brands.FindAsync(model.BrandId),
                Color = model.Color,
                Id = IsNew? 0 : model.Id,
                Line=model.Line,
                Model= model.Model,
                Plaque= model.Plaque.ToUpper(),
                Remarks= model.Remarks,
                //User = await _context.Users.FindAsync(model.UserId),
                VehicleType  = await _context.VehicleTipes.FindAsync(model.VehicleTypeId),
            };
        }

        public VehicleViewModel ToVehicleViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                BrandId = vehicle.Brand.Id,
                Brands = _combosHelper.GetComboBrand(),
                Id = vehicle.Id,
                Color = vehicle.Color,
                Line = vehicle.Line,
                Model = vehicle.Model,
                Plaque = vehicle.Plaque,
                Remarks = vehicle.Remarks,
                UserId = vehicle.User.Id,
                VehicleTypeId = vehicle.VehicleType.Id,
                VehicleTypes = _combosHelper.GetComboVehicleTypes(),
                VehiclePhotos = vehicle.VehiclePhotos,
            };
        
        }
    }
}
