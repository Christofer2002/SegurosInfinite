using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SegurosInfinite.Data;
using SegurosInfinite.Models.User;
using SegurosInfinite.Repositories.Interfaces;

namespace SegurosInfinite.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 Retrieve all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
                return new List<User>(); // Return empty list to avoid null reference exceptions
            }
        }

        // 📌 Retrieve a user by ID
        public async Task<User> GetUserByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Invalid user ID.");
                    return null;
                }

                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user with ID {id}: {ex.Message}");
                return null;
            }
        }

        // 📌 Add a new user
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    Console.WriteLine("Invalid user data.");
                    return false;
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return false;
            }
        }

        // 📌 Update an existing user
        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                if (user == null || string.IsNullOrWhiteSpace(user.Id))
                {
                    Console.WriteLine("Invalid user data.");
                    return false;
                }

                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    Console.WriteLine($"User with ID {user.Id} not found.");
                    return false;
                }

                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user with ID {user.Id}: {ex.Message}");
                return false;
            }
        }

        // 📌 Delete a user by ID
        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Invalid user ID.");
                    return false;
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    Console.WriteLine($"User with ID {id} not found.");
                    return false;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
