using SystemZglaszaniaAwariiGlowny.Data;
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
                    SeedZgloszeniaAdd(dbContext);
                 
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

            if (!dbContext.Users.Any(u => u.UserName == "pracownik2@awaria.pl"))
            {
                var user = new AppUser
                {
                    UserName = "pracownik2@awaria.pl",
                    NormalizedUserName = "pracownik2@awaria.pl",
                    Email = "pracownik2@awaria.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Jan",
                    LastName = "Konrad",
                    Information = "Nowak"
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
                for (int i = 1; i <= 1; i++)
                {
                    var idUzytkownika1 = dbContext.AppUsers
                    .Where(u => u.UserName == "admin@awaria.pl")
                     .FirstOrDefault()
                     .Id;

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {

                            MagazynName = "Magazyn prętów stalowych.",
                            MagazynOpis = "Magazyn dostosowany pod przechowywanie prętów stalowych rożnych długośći. Wyposażony w odpowiednie regały. Posiada odpowiednie tory poruszania się dla pracowników jak i wózków widłowych. ",
                            Graphic = "1Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,


                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++) 
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn części spawalniczych",
                            MagazynOpis = "Magazyn z częściami spawalniczymi. Przystosowany do przechowywania dużych ilości części przy minimalnym zagospodarowaniu przestrzeni.",
                            Graphic = "3Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++) 
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn maszyn ",
                            MagazynOpis = "Magazyn z maszynami wykorzystywanych na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "2Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn robotów ",
                            MagazynOpis = "Magazyn z robotami wykorzystywanych na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "1Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn konstrukcji stalowych ",
                            MagazynOpis = "Magazyn z konstrukcjami stalowymi wykorzystywanymi na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "2Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn konstrukcji drewnianych ",
                            MagazynOpis = "Magazyn z konstrukcjami drewnianymi wykorzystywanymi na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "3Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {

                            MagazynName = "Magazyn prętów stalowych.",
                            MagazynOpis = "Magazyn dostosowany pod przechowywanie prętów stalowych rożnych długośći. Wyposażony w odpowiednie regały. Posiada odpowiednie tory poruszania się dla pracowników jak i wózków widłowych. ",
                            Graphic = "1Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,


                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn części spawalniczych",
                            MagazynOpis = "Magazyn z częściami spawalniczymi. Przystosowany do przechowywania dużych ilości części przy minimalnym zagospodarowaniu przestrzeni.",
                            Graphic = "3Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn maszyn ",
                            MagazynOpis = "Magazyn z maszynami wykorzystywanych na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "2Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn robotów ",
                            MagazynOpis = "Magazyn z robotami wykorzystywanych na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "1Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn konstrukcji stalowych ",
                            MagazynOpis = "Magazyn z konstrukcjami stalowymi wykorzystywanymi na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "2Mag.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1,
                        };
                        dbContext.Set<Magazyn>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Magazyn()
                        {
                            MagazynName = "Magazyn konstrukcji drewnianych ",
                            MagazynOpis = "Magazyn z konstrukcjami drewnianymi wykorzystywanymi na magazynach produkcyjnych. Ze względów na duże rozmiary maszyn produkcyjnych jest ściśle pod monitoringiem w celu zachowania BHP przez pracowników.",
                            Graphic = "3Mag.jpg",
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
                for (int i = 1; i <= 1; i++) 
                {
                    var idUzytkownika1 = dbContext.AppUsers
                    .Where(u => u.UserName == "admin@awaria.pl")
                    .FirstOrDefault()
                    .Id;

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {

                            MaszynaName = "Spawarka model AL-320",
                            MaszynaOpis = "Spawarka model z roku 2022. Pełni funkcje tig, mig.",
                            Graphic = "1Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Betoniarka AG3050",
                            MaszynaOpis = "Betoniarka służąca do wylewania betonu",
                            Graphic = "2Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna pakująca",
                            MaszynaOpis = "Maszyna służąca do pakowania części do kartonów",
                            Graphic = "4Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna sortująca",
                            MaszynaOpis = "Maszyna służąca do sortowania i oddzielania części",
                            Graphic = "3Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {

                            MaszynaName = "Spawarka model AL-320",
                            MaszynaOpis = "Spawarka model z roku 2022. Pełni funkcje tig, mig.",
                            Graphic = "1Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Betoniarka AG3050",
                            MaszynaOpis = "Betoniarka służąca do wylewania betonu",
                            Graphic = "2Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna pakująca",
                            MaszynaOpis = "Maszyna służąca do pakowania części do kartonów",
                            Graphic = "4Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna sortująca",
                            MaszynaOpis = "Maszyna służąca do sortowania i oddzielania części",
                            Graphic = "3Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {

                            MaszynaName = "Spawarka model AL-320",
                            MaszynaOpis = "Spawarka model z roku 2022. Pełni funkcje tig, mig.",
                            Graphic = "1Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Betoniarka AG3050",
                            MaszynaOpis = "Betoniarka służąca do wylewania betonu",
                            Graphic = "2Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna pakująca",
                            MaszynaOpis = "Maszyna służąca do pakowania części do kartonów",
                            Graphic = "4Masz.jpg",
                            Active = true,
                            Display = true,
                            Id = idUzytkownika1
                        };
                        dbContext.Set<Maszyna>().Add(tekst);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var tekst = new Maszyna()
                        {
                            MaszynaName = "Maszyna sortująca",
                            MaszynaOpis = "Maszyna służąca do sortowania i oddzielania części",
                            Graphic = "3Masz.jpg",
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
                for (int i = 1; i <= 1; i++) 

                    for (int j = 1; j <= 1; j++) 
                    {
                        var mechanik0 = new Mechanik()
                        {

                            MechanikName = "Brak",
                            MechanikNazwisko = "",
                            


                        };
                        dbContext.Set<Mechanik>().Add(mechanik0);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++) 
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Kamil",
                            MechanikNazwisko = "Lewandowski",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++) 
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Adrian",
                            MechanikNazwisko = "Nowak",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();



                    for (int j = 1; j <= 1; j++) 
                    {
                        var mechanik1 = new Mechanik()
                        {

                            MechanikName = "Krzysztof",
                            MechanikNazwisko = "Kowalski",



                        };
                        dbContext.Set<Mechanik>().Add(mechanik1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++) 
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
        private static void SeedZgloszeniaAdd(ApplicationDbContext dbContext)
        {
            if (!dbContext.Zgloszenias.Any())
            {
                var idUzytkownikaZ1 = dbContext.AppUsers
                   .Where(u => u.UserName == "pracownik@awaria.pl")
                    .FirstOrDefault()
                    .Id;

                var idUzytkownikaZ2 = dbContext.AppUsers
                .Where(u => u.UserName == "pracownik2@awaria.pl")
                 .FirstOrDefault()
                 .Id;



                for (int i = 1; i <= 1; i++)
                {

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka robota spawającego",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 1,
                            MaszynaId = 1,
                            Id = idUzytkownikaZ1,
                            MechanikId = 2,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka betoniarki",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 2,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ1,
                            MechanikId = 3,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się koło zębate w maszynie",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 3,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ1,
                            MechanikId = 4,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Brak drutu spawalniczego w celi nr3",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 4,
                            MaszynaId = 4,
                            Id = idUzytkownikaZ1,
                            MechanikId = 5,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka robota spawającego",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 1,
                            MaszynaId = 1,
                            Id = idUzytkownikaZ1,
                            MechanikId = 2,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka betoniarki",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 2,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ1,
                            MechanikId = 3,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się koło zębate w maszynie",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 3,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ1,
                            MechanikId = 4,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Brak drutu spawalniczego w celi nr3",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 4,
                            MaszynaId = 4,
                            Id = idUzytkownikaZ1,
                            MechanikId = 5,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka robota spawającego",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 1,
                            MaszynaId = 1,
                            Id = idUzytkownikaZ2,
                            MechanikId = 2,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();

                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się rączka betoniarki",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 2,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ2,
                            MechanikId = 3,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();


                    for (int j = 1; j <= 1; j++)
                    {
                        var zgloszenie1 = new Zgloszenia()
                        {

                            AwariaOpis = "Zepsuła się koło zębate w maszynie",
                            Active = true,
                            AddedDate = DateTime.Now,
                            MagazynId = 3,
                            MaszynaId = 3,
                            Id = idUzytkownikaZ2,
                            MechanikId = 4,






                        };
                        dbContext.Set<Zgloszenia>().Add(zgloszenie1);
                    }
                    dbContext.SaveChanges();
                }
            }
        }
    }


     
}


