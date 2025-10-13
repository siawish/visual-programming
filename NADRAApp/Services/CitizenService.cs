using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NADRAApp.Data;
using NADRAApp.Models;

namespace NADRAApp.Services
{
    public class CitizenService
    {
        private readonly NADRAContext _context;

        public CitizenService(NADRAContext context)
        {
            _context = context;
        }

        public virtual async Task<List<Citizen>> GetAllCitizensAsync()
        {
            return await _context.Citizens.Where(c => c.IsActive).ToListAsync();
        }

        public virtual async Task<Citizen?> GetCitizenByIdAsync(int id)
        {
            return await _context.Citizens.FindAsync(id);
        }

        public virtual async Task<Citizen?> GetCitizenByCNICAsync(string cnic)
        {
            return await _context.Citizens.FirstOrDefaultAsync(c => c.CNIC == cnic);
        }

        public virtual async Task<List<Citizen>> SearchCitizensAsync(string searchTerm)
        {
            return await _context.Citizens
                .Where(c => c.IsActive && 
                    (c.FirstName.Contains(searchTerm) || 
                     c.LastName.Contains(searchTerm) || 
                     c.CNIC.Contains(searchTerm) ||
                     c.FatherName.Contains(searchTerm)))
                .ToListAsync();
        }

        public virtual async Task<bool> AddCitizenAsync(Citizen citizen)
        {
            try
            {
                var existingCitizen = await GetCitizenByCNICAsync(citizen.CNIC);
                if (existingCitizen != null)
                    return false;

                _context.Citizens.Add(citizen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateCitizenAsync(Citizen citizen)
        {
            try
            {
                _context.Citizens.Update(citizen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteCitizenAsync(int id)
        {
            try
            {
                var citizen = await GetCitizenByIdAsync(id);
                if (citizen != null)
                {
                    citizen.IsActive = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> ValidateCNICAsync(string cnic)
        {
            if (string.IsNullOrEmpty(cnic) || cnic.Length != 13)
                return false;

            if (!cnic.All(char.IsDigit))
                return false;

            var existingCitizen = await GetCitizenByCNICAsync(cnic);
            return existingCitizen == null;
        }
    }
}