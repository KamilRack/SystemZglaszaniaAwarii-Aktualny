﻿using SystemZglaszaniaAwariiGlowny.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SystemZglaszaniaAwariiGlowny.Models
{
    public class InfoSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))

                if (dbContext.Database.CanConnect())
                {
                    
                    SeedRoles(dbContext);
                    SeedUsers(dbContext);
                    SeedMaszyns(dbContext);
                    SeedMagazyns(dbContext);
                    SeedMechanics(dbContext);
                    SeedZgloszenias(dbContext);
                 
                }
           
            }
        //zakładanie nowych ról
        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);

            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                }).Wait();
            }



            if (!dbContext.Roles.Any(r => r.Name == "mechanik"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "mechanik",
                    NormalizedName = "mechanik"
                }).Wait();
            }




            if (!dbContext.Roles.Any(r => r.Name == "pracownik"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "pracownik",
                    NormalizedName = "pracownik"
                }).Wait();
            }



        }


        private static void SeedUsers(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any(u => u.UserName == "pracownik@awaria.pl"))
            {
                var user = new AppUser
                {
                    UserName = "pracownik@awaria.pl",
                    NormalizedUserName = "pracownik@awaria.pl",
                    Email = "pracownik@awaria.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Jan",
                    LastName = "Blachowicz",
                    Information = "Pracownik"
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Pracownik1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "pracownik").Wait();

                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "mechanik@awaria.pl"))
            {
                var user = new AppUser
                {
                    UserName = "mechanik@awaria.pl",
                    NormalizedUserName = "mechanik@awaria.pl",
                    Email = "mechanik@awaria.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Damian",
                    LastName = "Krysiewicz",
                    Information = "Mechanik."
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Mechanik1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "mechanik").Wait();
                dbContext.SaveChanges();
            }



            if (!dbContext.Users.Any(u => u.UserName == "admin@awaria.pl"))
            {
                var user = new AppUser
                {
                    UserName = "admin@awaria.pl",
                    NormalizedUserName = "admin@awaria.pl",
                    Email = "admin@awaria.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Kamil",
                    LastName = "Racki",
                    Information = "Administrator"
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Admin1!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "admin").Wait();
                dbContext.SaveChanges();
      
            }
        }

        private static void SeedMagazyns(ApplicationDbContext dbContext)
        {
            if (!dbContext.Magazyns.Any())
            {
                for (int i = 1; i <= 1; i++) //sześć kategorii
                {
                    var idUzytkownika1 = dbContext.AppUsers
                    .Where(u => u.UserName == "admin@awaria.pl")
                     .FirstOrDefault()
                     .Id;

                    for (int j = 0; j <= 1; j++) //teksty autora1
                    {
                        var tekst = new Magazyn()
                        {

                            MagazynName = "Magazyn z opiłkami",
                            MagazynOpis = "Magazyn z składnikami",
                            Graphic = "1Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,


                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                   

                    for (int j = 2; j <= 2; j++) //teksty autora2
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn z betonem",
                            MagazynOpis = "Magazyn z substancjami",
                            Graphic = "2Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();
                }
            }
        }


        private static void SeedMaszyns(ApplicationDbContext dbContext)
        {
            if (!dbContext.Maszynas.Any())
            {
                for (int i = 1; i <= 1; i++) //sześć kategorii
                {
                    var idUzytkownika1 = dbContext.AppUsers
                    .Where(u => u.UserName == "admin@awaria.pl")
                    .FirstOrDefault()
                    .Id;

                    for (int j = 0; j <= 1; j++) //teksty autora1
                    {
                        var tekst = new Maszyna()
                        {

                            MaszynaName = "Spawarka Al-102",
                            MaszynaOpis = "Spawarka",
                            Graphic = "1Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    

                    for (int j = 2; j <= 2; j++) //teksty autora2
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Spawarka BC-300",
                            MaszynaOpis = "Spawarka",
                            Graphic = "2Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();


                  
                }
            }
        }

        private static void SeedMechanics(ApplicationDbContext dbContext)
        {
            if (!dbContext.Mechaniks.Any())
            {
                for (int i = 1; i <= 1; i++) //sześć kategorii
                {
                //    var idUzytkownika1 = dbContext.AppUsers
                //    .Where(u => u.UserName == "admin@awaria.pl")
                //     .FirstOrDefault()
                //     .Id;

                    for (int j = 1; j <= 1; j++) //teksty autora1
                    {
                        var mechanik0 = new Mechanik()
                        {

                            MechanikName = "Brak",
                            MechanikNazwisko = ".",
                            


                        };
                        dbContext.Set<Mechanik>().Add(mechanik0);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++) //teksty autora2
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Kamil",
                            MechanikNazwisko = "Lewandowski",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++) //teksty autora2
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Adrian",
                            MechanikNazwisko = "Nowak",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++) //teksty autora2
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Krzysztof",
                            MechanikNazwisko = "Kowalski",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++) //teksty autora2
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Ludwik",
                            MechanikNazwisko = "Por",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();
                }
            }
        }


        private static void SeedZgloszenias(ApplicationDbContext dbContext)
        {
            if (!dbContext.Zgloszenias.Any())
            {
                for (int i = 1; i <= 1; i++)
                {
                    var idUzytkownika1 = dbContext.AppUsers
                    .Where(u => u.UserName == "pracownik@awaria.pl")
                    .FirstOrDefault()
                     .Id;

                    for (int j = 1; j <= 1; j++) //teksty autora1
                    {
                        var awaria1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się świeca podgrzewająca drut spawalniczy",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 1,
                            MaszynaId = 2,
                            Id = idUzytkownika1,
                            MechanikId = 2




                        };
                        dbContext.Set<Zgloszenia>().Add(awaria1);
                    }

                    var idUzytkownika2 = dbContext.AppUsers
                     .Where(u => u.UserName == "pracownik@awaria.pl")
                     .FirstOrDefault()
                      .Id;

                    for (int j = 1; j <= 1; j++) //teksty autora1
                    {
                        var awaria2 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuł się kabel zasilający maszynę",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 3,
                            MaszynaId = 3,
                            Id = idUzytkownika2,
                            MechanikId = 2




                        };
                        dbContext.Set<Zgloszenia>().Add(awaria2);
                        dbContext.SaveChanges();



                    }
                }
            }
        }
    }
}