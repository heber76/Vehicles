using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;
using Vehicles.API.Models;

namespace Vehicles.API.Helpers
{
public    interface IConvertertHelper
    {

        Task<Detail> ToDetailAsync(DetailViewModel model, bool isNew);
        DetailViewModel ToDetailViewModel(Detail detail);
        Task<User> ToUserAsync(UserViewModel model, Guid imageId, bool IsNew);
        UserViewModel ToUserViewModel(User user);

        Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool IsNew);
        VehicleViewModel ToVehicleViewModel(Vehicle vehicle);



    }
}
