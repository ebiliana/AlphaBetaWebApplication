#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlphaBetaWebApplication.Data;
using AlphaBetaWebApplication.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace AlphaBetaWebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly AlphaBetaWebApplicationContext _context;

        public UsersController(AlphaBetaWebApplicationContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        //    private void Mailsender(User user)
        //    {
        //    MimeMessage message = new MimeMessage();

        //    MailboxAddress from = new MailboxAddress("Admin",
        //    "admin@example.com");
        //    message.From.Add(from);

        //    MailboxAddress to = new MailboxAddress("User",
        //    user.Email);
        //    message.To.Add(to);

        //    message.Subject = "User Successfully Registered";
        //    BodyBuilder bodyBuilder = new BodyBuilder();
        //    bodyBuilder.HtmlBody = "<h1>Hi, you are Successfully Registered </h1>";
        //    bodyBuilder.TextBody = " please create password to login    Username: " + user.userName;
        //    message.Body = BodyBuilder.ToMessageBody();
        //    client.Send(message);
        //    client.Disconnect(true);
        //    client.Dispose();
        //}

        public bool Mailsender(User user)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("aduku.lilian@gmail.com");
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.Subject = "User Successfully Registered";
            BodyBuilder bodyBuilder = new BodyBuilder();           
            bodyBuilder.TextBody = "Hi, you are Successfully Registered please create password to login    Username: " + user.userName;            
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = bodyBuilder.TextBody;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential("aduku.lilian@gmail.com", "wealth3.");
            client.Host = "smtpout.secureserver.net";
            client.Port = 578;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId,userName,Email,phoneNo")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                Mailsender(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId,userName,Email,phoneNo")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.userId == id);
        }
    }
}
