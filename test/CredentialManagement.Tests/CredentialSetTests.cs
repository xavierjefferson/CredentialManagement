using System;
using Xunit;

namespace CredentialManagement.Tests
{
    public class CredentialSetTests
    {
        [Fact]
        public void CredentialSet_Create()
        {
            Assert.NotNull(new CredentialSet());
        }

        [Fact]
        public void CredentialSet_Create_WithTarget()
        {
            Assert.NotNull(new CredentialSet("target"));
        }

        [Fact]
        public void CredentialSet_ShouldBeIDisposable()
        {
            Assert.True(new CredentialSet() is IDisposable,
                "CredentialSet needs to implement IDisposable Interface.");
        }

        [Fact]
        public void CredentialSet_Load()
        {
            var credential = new Credential
            {
                Username = "username",
                Password = "password",
                Target = "target",
                Type = CredentialType.Generic
            };
            credential.Save();

            var set = new CredentialSet();
            set.Load();
            Assert.NotNull(set);
            Assert.NotEmpty(set);


            credential.Delete();

            set.Dispose();
        }

        [Fact]
        public void CredentialSet_Load_ShouldReturn_Self()
        {
            var set = new CredentialSet();
            Assert.IsAssignableFrom<CredentialSet>(set.Load());

            set.Dispose();
        }

        [Fact]
        public void CredentialSet_Load_With_TargetFilter()
        {
            var credential = new Credential
            {
                Username = "filteruser",
                Password = "filterpassword",
                Target = "filtertarget"
            };
            credential.Save();

            var set = new CredentialSet("filtertarget");
            Assert.Single(set.Load());
            set.Dispose();
        }
    }
}