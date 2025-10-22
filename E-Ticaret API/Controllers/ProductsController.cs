using E_Ticaret_API.Data;
using E_Ticaret_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret_API.Controllers
{
    [Route("api/[controller]")] // 1. Bu satır, adresin "api/products" olmasını sağlar
    [ApiController] // 2. Bu satır, bunun bir API Controller olduğunu framework'e söyler
    [Authorize]
    public class ProductsController : ControllerBase
    {
        // 1. Veritabanı (DataContext) ile konuşmak için bir değişken oluştur
        private readonly ApplicationDbContext _context;

        // 2. Constructor (Yapıcı Metot) ile DataContext'i enjekte et
        // Program.cs'te eklediğimiz servis sayesinde bu otomatik olarak çalışacak.
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- ADIM 3: ENDPOINT'LER ---

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // Tüm ürünleri veritabanından bul ve liste olarak dön
            var products = await _context.Products.ToListAsync();
            return Ok(products); // 200 OK (Başarılı) durumuyla birlikte ürünleri dön
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Verilen id'ye sahip ürünü veritabanında ara
            var product = await _context.Products.FindAsync(id);

            // Eğer ürün bulunamazsa, 404 Not Found (Bulunamadı) hatası dön
            if (product == null)
            {
                return NotFound();
            }

            // Ürün bulunduysa, 200 OK (Başarılı) durumuyla ürünü dön
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            // Gelen "product" nesnesini veritabanına ekle
            _context.Products.Add(product);
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet

            // Yeni oluşturulan ürünü 201 Created (Oluşturuldu) durumuyla geri dön
            // Bu, best practice'dir (doğru uygulama)
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            // Eğer gelen id ile product'ın id'si uyuşmuyorsa, 400 Bad Request (Kötü İstek) dön
            if (id != product.Id)
            {
                return BadRequest();
            }

            // Entity Framework'e bu ürünün durumunun "Değiştirildi" (Modified) olduğunu söyle
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Değişikliği kaydet
            }
            catch (DbUpdateConcurrencyException)
            {
                // Eğer kaydetme sırasında bir hata olursa (örn: ürün o arada silinmişse)
                // ve ürün veritabanında yoksa NotFound dön
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Başka bir hata ise fırlat
                }
            }

            // Başarılı olursa 204 No Content (İçerik Yok) dön. PUT için standarttır.
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Silinecek ürünü veritabanında bul
            var product = await _context.Products.FindAsync(id);

            // Ürün yoksa 404 NotFound (Bulunamadı) dön
            if (product == null)
            {
                return NotFound();
            }

            // Ürünü veritabanından sil
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); // Değişikliği kaydet

            // Başarılı olursa 204 No Content (İçerik Yok) dön.
            return NoContent();
        }
    }
}
