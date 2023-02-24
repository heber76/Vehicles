using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vehicles.API.Data;
using System.Linq;
using Vehicles.Common.Enums;
using Vehicles.API.Helpers;
using Vehicles.API.Models;
using System;
using Vehicles.API.Data.Entities;
using System.Collections.Generic;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConvertertHelper _convertertHelper;
        private readonly IBlobHelper _blobHelper;

        public UsersController(DataContext context,IUserHelper userHelper, ICombosHelper combosHelper,
            IConvertertHelper convertertHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _convertertHelper = convertertHelper;
            _blobHelper = blobHelper;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Vehicles)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync());
        }


        public IActionResult Create()
        {

            UserViewModel model = new UserViewModel
            {
                DocumentTypes = _combosHelper.GetComboDocumnetTypes()
            };
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            //model.DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId);
            if (ModelState.IsValid)
            {

                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UpLoadBlobAsync(model.ImageFile, "users");
                }
                User user = await _convertertHelper.ToUserAsync(model, imageId, true);
                user.UserType = UserType.User;
                //user.UserName = model.Email;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRole(user, user.UserType.ToString());

                return RedirectToAction(nameof(Index));
                
            }
            model.DocumentTypes = _combosHelper.GetComboDocumnetTypes();
            return View(model);
        }



        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
               return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model =  _convertertHelper.ToUserViewModel(user);
            
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    Guid imageId = model.ImageId;
                    if (model.ImageFile != null)
                    {
                        imageId = await _blobHelper.UpLoadBlobAsync(model.ImageFile, "users");
                    }
                    User user = await _convertertHelper.ToUserAsync(model,imageId,false);
                    await _userHelper.UpdateUserAsync(user);
                    return RedirectToAction(nameof(Index));
            }
            model.DocumentTypes = _combosHelper.GetComboDocumnetTypes();
            return View(model);
        }


        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            await _blobHelper.DeletBlobAsync(user.ImageId, "users");
            await _userHelper.DeleteUserAsync(user);
            
            return RedirectToAction(nameof(Index));

        }


        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> DeleteVehicle(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles
                .Include(x => x.User)
                .Include(x =>x.VehiclePhotos)
                .Include(x => x.Histories)
                .ThenInclude(x=> x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details),new { id = vehicle.User.Id});

        }

        // GET: VehicleTypes/Delete/5
        public async Task<IActionResult> DeleteImageVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiclePhoto vehiclePhoto = await _context.VehiclePhotos
                .Include(x => x.Vehicle)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (vehiclePhoto == null)
            {
                return NotFound();
            }
            await _blobHelper.DeletBlobAsync(vehiclePhoto.ImageId, "vehicles");
            _context.VehiclePhotos.Remove(vehiclePhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = vehiclePhoto.Vehicle.Id });

        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user=  await _context.Users
               
               .Include(x => x.DocumentType)
               .Include(x => x.Vehicles)
               .ThenInclude(x => x.VehicleType)
               .Include(x => x.Vehicles)
               .ThenInclude(x => x.Brand)
               .Include(x => x.Vehicles)
               .ThenInclude(x => x.VehiclePhotos)
               .Include(x => x.Vehicles)
               .ThenInclude(x => x.Histories)
               .ThenInclude(x => x.Details)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        public async Task<IActionResult> AddVehicle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
               .Include(x => x.Vehicles)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new VehicleViewModel
            {
                Brands = _combosHelper.GetComboBrand(),
                VehicleTypes = _combosHelper.GetComboVehicleTypes(),
                UserId = user.Id,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel vehicleViewModel)
        {
            
            //if (ModelState.IsValid)
            //{

                User user = await _context.Users
                    .Include(x => x.Vehicles)
                    .FirstOrDefaultAsync(x=> x.Id == vehicleViewModel.UserId);

                if (user == null)
                {
                    return NotFound();
                }

                Guid imageId = Guid.Empty;
                if (vehicleViewModel.ImageFile != null)
                {
                    imageId = await _blobHelper.UpLoadBlobAsync(vehicleViewModel.ImageFile, "vehicles");
                }
                Vehicle vehicle = await _convertertHelper.ToVehicleAsync(vehicleViewModel,  true);

                if (vehicle.VehiclePhotos == null)
                {
                    vehicle.VehiclePhotos = new List<VehiclePhoto>();

                }

                vehicle.VehiclePhotos.Add(new VehiclePhoto
                {
                    ImageId =imageId,
                });


                try
                {
                    user.Vehicles.Add(vehicle);
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { id = user.Id });
                }
                catch(DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esa placa");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);

                    }
                }
                catch (Exception exception)
                {

                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            //}

            vehicleViewModel.Brands = _combosHelper.GetComboBrand();
            vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();
            return View(vehicleViewModel);
        }



        
        
        public async Task<IActionResult> EditVehicle(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }

            Vehicle vehicle = await _context.Vehicles
                .Include(x => x.User)
                .Include(x => x.Brand)
                .Include(x => x.VehicleType)
                .Include(x => x.VehiclePhotos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {

                return NotFound();
            }
            VehicleViewModel model = _convertertHelper.ToVehicleViewModel(vehicle);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicle(int id, VehicleViewModel vehicleViewModel)
         {

            if (id != vehicleViewModel.Id)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle vehicle = await _convertertHelper.ToVehicleAsync(vehicleViewModel, false);
                    _context.Vehicles.Update(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details),new { id= vehicleViewModel.UserId });
                    

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esa placa");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);

                    }
                }
                catch (Exception exception)
                {

                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            vehicleViewModel.Brands = _combosHelper.GetComboBrand();
            vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();
            return View(vehicleViewModel);

        }



    }
}
