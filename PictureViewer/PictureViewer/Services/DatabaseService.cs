using Core.Models;
using Microsoft.EntityFrameworkCore;
using PictureViewer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.Services
{
    public class DatabaseService
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Customer CheckLogin(LoginData loginData)
        {
            return _databaseContext.Customers.FirstOrDefault(c => c.Username == loginData.Username && c.Password == loginData.Password);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            _databaseContext.Customers.Add(customer);
            await _databaseContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomer(int customerId)
        {
            var upgradeAbleCustomer = _databaseContext.Customers.Find(customerId);

            if (upgradeAbleCustomer == null)
            {
                return null;
            }

            _databaseContext.Customers.Remove(upgradeAbleCustomer);
            await _databaseContext.SaveChangesAsync();
            return upgradeAbleCustomer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _databaseContext.Customers.Include(c => c.Pictures).ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            return _databaseContext.Customers.Find(customerId);
        }

        public async Task<Picture> AddPicture(int customerId, Picture picture)
        {
            _databaseContext.Customers.Find(customerId).Pictures.Add(picture);
            await _databaseContext.SaveChangesAsync();
            return picture;
        }

        public async Task UpdatePicture(Picture picture)
        {
            var upgradeAblePicture = _databaseContext.Pictures.Find(picture.Id);

            if (upgradeAblePicture == null)
            {
                return;
            }

            upgradeAblePicture.Title = picture.Title;
            upgradeAblePicture.Description = picture.Description;

            if(picture.Image != null && picture.Image.Any())
            {
                upgradeAblePicture.Image = picture.Image;
            }
            //_databaseContext.Pictures.Update(upgradeAblePicture);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Picture> DeletePicture(int pictureId)
        {
            var picture = _databaseContext.Pictures.Find(pictureId);

            if (picture == null)
            {
                return null;
            }

            _databaseContext.Pictures.Remove(picture);
            await _databaseContext.SaveChangesAsync();
            return picture;
        }

        public IEnumerable<Picture> GetPictures()
        {
            return _databaseContext.Pictures.Include(p => p.Uploader).ToList();
        }

        public Picture GetPicture(int pictureId)
        {
            return _databaseContext.Pictures.Include(p => p.Uploader).Single(p => p.Id == pictureId);
        }
    }
}
