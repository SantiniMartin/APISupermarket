using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ComparadorPreciosAPI.DTOs;
using ComparadorPreciosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ComparadorPreciosAPI.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "seed_data.json");
            if (!File.Exists(filePath)) return;

            var json = await File.ReadAllTextAsync(filePath);
            var data = JsonConvert.DeserializeObject<RootSeedDto>(json);

            foreach (var sm in data.supermarkets)
            {
                // Supermercado
                var supermercado = await context.Supermercados.FirstOrDefaultAsync(s => s.Nombre == sm.name);
                if (supermercado == null)
                {
                    supermercado = new Supermercado
                    {
                        Nombre = sm.name,
                        Logo = sm.logo_url
                    };
                    context.Supermercados.Add(supermercado);
                    await context.SaveChangesAsync();
                }

                foreach (var prod in sm.products)
                {
                    // Marca
                    var marca = await context.Marcas.FirstOrDefaultAsync(m => m.Nombre == prod.brand);
                    if (marca == null)
                    {
                        marca = new Marca { Nombre = prod.brand };
                        context.Marcas.Add(marca);
                        await context.SaveChangesAsync();
                    }

                    // Categoría
                    var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Nombre == prod.category);
                    if (categoria == null)
                    {
                        categoria = new Categoria
                        {
                            Nombre = prod.category,
                            Icono = prod.category_image_url
                        };
                        context.Categorias.Add(categoria);
                        await context.SaveChangesAsync();
                    }

                    // Producto
                    var producto = await context.Productos.FirstOrDefaultAsync(p => p.Nombre == prod.name && p.MarcaId == marca.Id);
                    if (producto == null)
                    {
                        producto = new Producto
                        {
                            Nombre = prod.name,
                            Imagen = prod.image_url,
                            MarcaId = marca.Id,
                            CategoriaId = categoria.Id
                        };
                        context.Productos.Add(producto);
                        await context.SaveChangesAsync();
                    }

                    // ProductoSupermercado
                    var prodSuper = await context.ProductoSupermercados.FirstOrDefaultAsync(ps =>
                        ps.ProductoId == producto.Id && ps.SupermercadoId == supermercado.Id);
                    if (prodSuper == null)
                    {
                        prodSuper = new ProductoSupermercado
                        {
                            ProductoId = producto.Id,
                            SupermercadoId = supermercado.Id,
                            Precio = prod.price,
                            Stock = prod.stock,
                            Url = null,
                            UltimaActualizacion = DateTime.UtcNow
                        };
                        context.ProductoSupermercados.Add(prodSuper);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
} 