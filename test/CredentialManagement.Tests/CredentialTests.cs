using System;
using Xunit;

namespace CredentialManagement.Tests
{
    public class CredentialTests
    {
        [Fact]
        public void Credential_Create_ShouldNotThrowNull()
        {
            Assert.NotNull(new Credential());
        }

        [Fact]
        public void Credential_Create_With_Username_ShouldNotThrowNull()
        {
            Assert.NotNull(new Credential("username"));
        }

        [Fact]
        public void Credential_Create_With_Username_And_Password_ShouldNotThrowNull()
        {
            Assert.NotNull(new Credential("username", "password"));
        }

        [Fact]
        public void Credential_Create_With_Username_Password_Target_ShouldNotThrowNull()
        {
            Assert.NotNull(new Credential("username", "password", "target"));
        }

        [Fact]
        public void Credential_ShouldBe_IDisposable()
        {
            Assert.IsAssignableFrom<IDisposable>(new Credential());
        }

        [Fact]
        public void Credential_Dispose_ShouldNotThrowException()
        {
            new Credential().Dispose();
        }

        [Fact]
        public void Credential_ShouldThrowObjectDisposedException()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                var disposed = new Credential { Password = "password" };
                disposed.Dispose();
                disposed.Username = "username";
            });
        }

        [Fact]
        public void Credential_Save()
        {
            var saved = new Credential("username", "password", "target", CredentialType.Generic);
            saved.PersistenceType = PersistenceType.LocalComputer;
            Assert.True(saved.Save());
        }

        [Fact]
        public void Credential_Delete()
        {
            new Credential("username", "password", "target").Save();
            Assert.True(new Credential("username", "password", "target").Delete());
        }

        [Fact]
        public void Credential_Delete_NullTerminator()
        {
            var credential = new Credential(null, null, "\0", CredentialType.None);
            credential.Description = null;
            Assert.False(credential.Delete());
        }

        [Fact]
        public void Credential_Load()
        {
            var setup = new Credential("username", "password", "target", CredentialType.Generic);
            setup.Save();

            var credential = new Credential { Target = "target", Type = CredentialType.Generic };
            Assert.True(credential.Load());

            Assert.NotEmpty(credential.Username);
            Assert.NotNull(credential.Password);
            Assert.Equal("username", credential.Username);
            Assert.Equal("password", credential.Password);
            Assert.Equal("target", credential.Target);
        }

        [Fact]
        public void Credential_Exists_Target_ShouldNotBeNull()
        {
            new Credential { Username = "username", Password = "password", Target = "target" }.Save();

            var existingCred = new Credential { Target = "target" };
            Assert.True(existingCred.Exists());

            existingCred.Delete();
        }
    }
}