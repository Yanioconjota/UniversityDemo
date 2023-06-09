﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    /*
     Here's what each part of a method does:

    public: This is an access modifier that means the method can be accessed from any other method in any class.

    async: This keyword is used to specify that a method, lambda expression, or anonymous method is asynchronous. In this context, it means the method runs asynchronously, allowing the server to handle other requests while waiting for GetUsers() to complete.

    Task<ActionResult<IEnumerable<User>>>: This is the return type of the method.

    Task indicates that the method is asynchronous, and it's used with async.
    ActionResult is a type that represents an HTTP response. It has several derived classes like OkResult, NotFoundResult, ContentResult and others, each representing a different HTTP status code.
    IEnumerable<User> is an interface that represents a forward-only cursor of User. It is wrapped within ActionResult meaning this method can either return an HTTP result (like NotFound) or a collection of User objects.
    GetUsers(): This is the name of the method. By convention in REST APIs, a method starting with Get is expected to retrieve data and not modify it. The name suggests that it retrieves users.

    Inside a method, you'd usually find logic to retrieve users, likely from a database. Since it's marked as async, it likely involves an asynchronous database operation. The method will return an HTTP response with a list of users in the body.

    However, without the implementation body of the method, I can only provide a high-level idea of what it might do based on conventions and the method signature.
     */

    [Route("api/[controller]")] //controller for Requests to https://localhost:7211/api/users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        public UsersController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7211/api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: https://localhost:7211/api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: https://localhost:7211/api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // POST: https://localhost:7211/api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UniversityDBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(user => user.Id == id)).GetValueOrDefault();
        }

        // GET: https://localhost:7211/api/users/email
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByEmail(string email)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Where(u => u.Email == email).ToListAsync();

            if (user.Count == 0)
            {
                return NotFound();
            }

            return user;
        }
    }
}
