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

        public UserViewModel ToUserViewModel(User user) => new UserViewModel
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
}
