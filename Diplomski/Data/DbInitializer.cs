using Diplomski.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomski.Data
{
    public class DbInitializer
    {
        // Loads initial data into database
        public static void InitializeDatabase(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if(!context.Admins.Any())
            {
                Admin a = new Admin() { FirstName = "Miljan", LastName = "Grcic", Username = "admin", Password = "112233445566" };
                context.Admins.Add(a);
                context.SaveChanges();
            }

            if(!context.Categories.Any())
            {
                Category c1 = new Category() { Name = "Alkoholna pića" };
                Category c2 = new Category() { Name = "Bezalkoholna pića" };
                Category c3 = new Category() { Name = "Vina", ParentCategory = c1 };
                Category c4 = new Category() { Name = "Piva", ParentCategory = c1 };
                Category c5 = new Category() { Name = "Bela vina", ParentCategory = c3 };
                Category c6 = new Category() { Name = "Crvena vina", ParentCategory = c3 };
                Category c7 = new Category() { Name = "Vode", ParentCategory = c2 };
                Category c8 = new Category() { Name = "Gazirane vode", ParentCategory = c7 };
                Category c9 = new Category() { Name = "Negazirane vode", ParentCategory = c7 };
                context.Categories.AddRange(c1, c2, c3, c4, c5, c6,c7, c8, c9);
                context.SaveChanges();
            }

            if(!context.Manufacturers.Any())
            {
                Manufacturer m1 = new Manufacturer()
                {
                    Name = "Vinarija Despotika",
                    Address = "Vlaški Do, 11423 Smederevska palanka",
                    BannerImageFileName = "a9dab210-f3d7-4970-aa39-3efb7b03a602.jpg",
                    LogoImageFileName = "0192954f-afc0-48ae-b5ee-1d7ad8120bbb.jpg",
                    Email = "vinarija@vinarijadespotika.rs",
                    Phone = "+381 026 302 126",
                    Website = "https://vinarijadespotika.rs",
                };

                Manufacturer m2 = new Manufacturer()
                {
                    Name = "Apatinska pivara",
                    Address = "Trg Oslobođenja 5, 25260 Apatin",
                    BannerImageFileName = "18b3ceaa-b693-4f2d-bd2e-ec3957df9d94.jpg",
                    LogoImageFileName = "b44a0a85-b04c-4323-b9f9-bdf6ec92e5e1.png",
                    Email = " info.apa@molsoncoors.com",
                    Phone = "+381 11 30 72 400 ",
                    Fax = "+381 11 30 72 444",
                    Website = "http://www.apatinskapivara.rs/sr-latn-rs",
                };

                Manufacturer m3 = new Manufacturer()
                {
                    Name = "Knjaz Miloš",
                    Address = "Krćevački put 26, Aranđelovac",
                    BannerImageFileName = "knjaz_milos_banner.jpg",
                    LogoImageFileName = "knjaz_milos_logo.png",
                    Email = "оffice@knjaz.co.rs",
                    Phone = "+381 34 700 700",
                    Fax = "+381 34 727 211",
                    Website = "https://knjaz.rs/",
                };

                context.Manufacturers.AddRange(m1, m2, m3);
                context.SaveChanges();
            }

            if(!context.PackagingMaterials.Any())
            {
                PackagingMaterial p1 = new PackagingMaterial() { Name = "PET" };
                PackagingMaterial p2 = new PackagingMaterial() { Name = "Staklo" };
                PackagingMaterial p3 = new PackagingMaterial() { Name = "Box" };
                context.PackagingMaterials.AddRange(p1, p2, p3);
                context.SaveChanges();
            }

            if(!context.Volumes.Any())
            {
                Volume v1 = new Volume() { Amount = 0.5M, MeasuringUnitAbbreviation = "L" };
                Volume v2 = new Volume() { Amount = 0.75M, MeasuringUnitAbbreviation = "L" };
                Volume v3 = new Volume() { Amount = 1, MeasuringUnitAbbreviation = "L" };
                Volume v4 = new Volume() { Amount = 1.5M, MeasuringUnitAbbreviation = "L" };
                Volume v5 = new Volume() { Amount = 2, MeasuringUnitAbbreviation = "L" };
                Volume v6 = new Volume() { Amount = 3, MeasuringUnitAbbreviation = "L" };
                Volume v7 = new Volume() { Amount = 5, MeasuringUnitAbbreviation = "L" };
                context.Volumes.AddRange(v1, v2, v3, v4, v5, v6, v7);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                Manufacturer despotika = context.Manufacturers.SingleOrDefault(m => m.Name == "Vinarija Despotika");
                Manufacturer apatinskaPivara = context.Manufacturers.SingleOrDefault(m => m.Name == "Apatinska pivara");
                Manufacturer knjazMilos = context.Manufacturers.SingleOrDefault(m => m.Name == "Knjaz Miloš");

                Category belaVina = context.Categories.SingleOrDefault(c => c.Name == "Bela vina");
                Category crvenaVina = context.Categories.SingleOrDefault(c => c.Name == "Crvena vina");
                Category piva = context.Categories.SingleOrDefault(c => c.Name == "Piva");
                Category gaziraneVode = context.Categories.SingleOrDefault(c => c.Name == "Gazirane vode");
                Category negaziraneVode = context.Categories.SingleOrDefault(c => c.Name == "Negazirane vode");

                PackagingMaterial pet = context.PackagingMaterials.SingleOrDefault(p => p.Name == "PET");
                PackagingMaterial staklo = context.PackagingMaterials.SingleOrDefault(p => p.Name == "Staklo");

                Volume zapremina05 = context.Volumes.SingleOrDefault(v => v.Amount.Equals(0.5M));
                Volume zapremina075 = context.Volumes.SingleOrDefault(v => v.Amount.Equals(0.75M));
                Volume zapremina15 = context.Volumes.SingleOrDefault(v => v.Amount.Equals(1.5M));
                Volume zapremina2 = context.Volumes.SingleOrDefault(v => v.Amount.Equals(2));


                Product p1 = new Product()
                {
                    Name = "Despotika Morava 0.75L",
                    Category = belaVina,
                    Price = 899,
                    EANCode = "8606107067162",
                    Manufacturer = despotika,
                    PackagingMaterial = staklo,
                    Volume = zapremina075,
                    ImageFileName = "morava.jpg"
                };

                Product p2 = new Product()
                {
                    Name = "Despotika Dokaz 0.75L",
                    Category = crvenaVina,
                    Price = 999,
                    EANCode = "8606107067056",
                    Manufacturer = despotika,
                    PackagingMaterial = staklo,
                    Volume = zapremina075,
                    ImageFileName = "dokaz.jpg"
                };

                Product p3 = new Product()
                {
                    Name = "Jelen pivo 2L",
                    Category = piva,
                    Price = 169,
                    EANCode = "8600105001494",
                    Manufacturer = apatinskaPivara,
                    PackagingMaterial = pet,
                    Volume = zapremina2,
                    ImageFileName = "jelen2l.jpg"
                };

                Product p4 = new Product()
                {
                    Name = "Knjaz Miloš 1.5L",
                    Category = gaziraneVode,
                    Price = 45,
                    EANCode = "8600105001494",
                    Manufacturer = knjazMilos,
                    PackagingMaterial = pet,
                    Volume = zapremina15,
                    ImageFileName = "knjaz1.5l.jpg"
                };

                Product p5 = new Product()
                {
                    Name = "Aqua viva 1.5L",
                    Category = negaziraneVode,
                    Price = 45,
                    EANCode = "8600105001494",
                    Manufacturer = knjazMilos,
                    PackagingMaterial = pet,
                    Volume = zapremina15,
                    ImageFileName = "aqua1.5l.jpg"
                };

                context.Products.AddRange(p1, p2, p3, p4, p5);
                context.SaveChanges();
            }

        
        }
    }
}
