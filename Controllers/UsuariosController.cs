
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Carros.Models;
using System.Security.Claims;

public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: USUARIOSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Usuarios.ToListAsync());
    }

    // GET: USUARIOSS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuarios = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.ID == id);
        if (usuarios == null)
        {
            return NotFound();
        }

        return View(usuarios);
    }

    // GET: USUARIOSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: USUARIOSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Nome,Senha,Perfil")] Usuarios usuarios)
    {
        if (ModelState.IsValid)
        {
            usuarios.Senha = BCrypt.Net.BCrypt.HashPassword(usuarios.Senha);
            _context.Add(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(usuarios);
    }

    // GET: USUARIOSS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuarios = await _context.Usuarios.FindAsync(id);
        if (usuarios == null)
        {
            return NotFound();
        }
        return View(usuarios);
    }

    // POST: USUARIOSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ID,Nome,Senha,Perfil")] Usuarios usuarios)
    {
        if (id != usuarios.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                usuarios.Senha = BCrypt.Net.BCrypt.HashPassword(usuarios.Senha);
                _context.Update(usuarios);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(usuarios.ID))
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
        return View(usuarios);
    }

    // GET: USUARIOSS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuarios = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.ID == id);
        if (usuarios == null)
        {
            return NotFound();
        }

        return View(usuarios);
    }

    // POST: USUARIOSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var usuarios = await _context.Usuarios.FindAsync(id);
        if (usuarios != null)
        {
            _context.Usuarios.Remove(usuarios);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuariosExists(int? id)
    {
        return _context.Usuarios.Any(e => e.ID == id);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Usuarios usuario)
    {
        var dados = await _context.Usuarios.FindAsync(usuario.ID);

        if (dados == null)
        {
            ViewBag.Mensagem = "Id e/ou Senha errados";
            
        }

        bool senhaOk = BCrypt.Net.BCrypt.Verify(usuario.Senha, dados.Senha);

        if(senhaOk)
        {
            //Senha deu tudo certo
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dados.Nome),
                new Claim(ClaimTypes.NameIdentifier, dados.ID.ToString()),
                new Claim(ClaimTypes.Role, dados.Perfil.ToString())
            };

            var UsuarioIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(UsuarioIdentity);

            var props = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(12),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(principal, props);

            return Redirect("/");
        } else
        {
            ViewBag.Mensagem = "Id e/ou Senha errados";
        }

    
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Usuarios");
    }
}
