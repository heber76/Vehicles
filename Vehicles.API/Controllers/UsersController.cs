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

            await _userHelper.DeleteUserAsync(user);
            
            return RedirectToAction(nameof(Index));

        }



    }
}
