﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using tp4p1.Models.EntityFramework;

namespace Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {

        UtilisateursController controller;


        [TestMethod()]
        public async Task GetUtilisateurSTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=dbtp4; uid=postgres; password=postgres;");
            await using var context = new FilmRatingsDBContext(builder.Options);
            var controller = new UtilisateursController(context);

            // Act
            var actionResult = await controller.GetUtilisateurs();

            // Assert
            var result = actionResult.Value;
            Assert.IsNotNull(result, "Le résultat de GetUtilisateurs() est null.");

            var expectedUsers = await context.Utilisateurs.ToListAsync();
            Assert.AreEqual(expectedUsers.Count, result.Count(), "Le nombre d'utilisateurs ne correspond pas.");

            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.AreEqual(expectedUsers[i].UtilisateurId, result.ElementAt(i).UtilisateurId);
                Assert.AreEqual(expectedUsers[i].Mail, result.ElementAt(i).Mail);

            }
        }


        [TestMethod()]
        public async Task GetUtilisateurByIdTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=dbtp4; uid=postgres; password=postgres;");
            await using var context = new FilmRatingsDBContext(builder.Options);
            var controller = new UtilisateursController(context);

            // Act
            var actionResult = await controller.GetUtilisateurById(1);

            // Assert
            Assert.IsNotNull(actionResult.Value, "Le résultat de GetUtilisateurById() est null.");

            var expectedUser = await context.Utilisateurs.FirstOrDefaultAsync(a => a.UtilisateurId == 1);
            var actualUser = actionResult.Value;

            Assert.IsNotNull(expectedUser, "L'utilisateur attendu est null");
            Assert.AreEqual(expectedUser.UtilisateurId, actualUser.UtilisateurId);
            Assert.AreEqual(expectedUser.Nom, actualUser.Nom);
            Assert.AreEqual(expectedUser.Mail, actualUser.Mail);
        }

        [TestMethod()]
        public async Task GetUtilisateurBymailTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=dbtp4; uid=postgres; password=postgres;");
            await using var context = new FilmRatingsDBContext(builder.Options);
            var controller = new UtilisateursController(context);

            // Act
            var actionResult = await controller.GetUtilisateurByEmail("durand@gmail.net");

            // Assert
            Assert.IsNotNull(actionResult.Value, "Le résultat de GetUtilisateurById() est null.");

            var expectedUser = await context.Utilisateurs.FirstOrDefaultAsync(a => a.Mail == "durand@gmail.net");
            var actualUser = actionResult.Value;

            Assert.IsNotNull(expectedUser, "L'utilisateur attendu est null");
            Assert.AreEqual(expectedUser.UtilisateurId, actualUser.UtilisateurId);
            Assert.AreEqual(expectedUser.Nom, actualUser.Nom);
            Assert.AreEqual(expectedUser.Mail, actualUser.Mail);
        }


        [TestMethod]
        public async Task PostUtilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=dbtp4; uid=postgres; password=postgres;");
            await using var context = new FilmRatingsDBContext(builder.Options);
            var controller = new UtilisateursController(context);

            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            string uniqueMail = "machin" + chiffre + "@gmail.com";


            Utilisateur userAtester = new Utilisateur
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            try
            {
                // Act
                var actionResult = await controller.PostUtilisateur(userAtester);

                // Assert
                Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Le résultat devrait être CreatedAtActionResult");

                var createdResult = actionResult.Result as CreatedAtActionResult;
                var createdUser = createdResult.Value as Utilisateur;
                Assert.IsNotNull(createdUser, "L'utilisateur créé ne devrait pas être null");

                // Vérification en base de données
                var userFromDb = await context.Utilisateurs
                    .FirstOrDefaultAsync(u => u.Mail.ToUpper() == uniqueMail.ToUpper());

                Assert.IsNotNull(userFromDb, "L'utilisateur n'a pas été trouvé dans la base de données");
                Assert.AreEqual(userAtester.Nom, userFromDb.Nom);
                Assert.AreEqual(userAtester.Prenom, userFromDb.Prenom);
                Assert.AreEqual(userAtester.Mail, userFromDb.Mail);
                Assert.AreEqual(userAtester.Mobile, userFromDb.Mobile);


                // Cleanup - Suppression de l'utilisateur de test
                context.Utilisateurs.Remove(userFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Le test a échoué avec l'erreur : {ex.Message}");
            }
        }
    }
}