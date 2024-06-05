using System;
using System.Text;
using Xunit;

namespace CredentialManagement.Tests
{
    public class XPPromptTests : IDisposable
    {
        private static string MAX_LENGTH_VALIDATION_TEXT;


        public XPPromptTests()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 50000; i++) sb.Append('A');
            MAX_LENGTH_VALIDATION_TEXT = sb.ToString();
        }

        public void Dispose()
        {
            MAX_LENGTH_VALIDATION_TEXT = null;
        }

        [Fact]
        public void XPPrompt_Create_ShouldNotBeNull()
        {
            Assert.NotNull(new XPPrompt());
        }

        [Fact]
        public void XPPrompt_ShouldImplement_IDisposable()
        {
            Assert.True(new XPPrompt() is IDisposable, "XPPrompt should implement IDisposable interface.");
        }

        [Fact]
        public void XPPrompt_Username_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new XPPrompt { Username = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void XPPrompt_Username_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new XPPrompt { Username = null });
        }

        [Fact]
        public void XPPrompt_Password_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new XPPrompt { Password = null });
        }

        [Fact]
        public void XPPrompt_Target_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new XPPrompt { Target = null });
        }

        [Fact]
        public void XPPrompt_Message_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new XPPrompt { Message = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void XPPrompt_Message_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new XPPrompt { Message = null });
        }

        [Fact]
        public void XPPrompt_Title_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new XPPrompt { Title = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void XPPrompt_Title_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new XPPrompt { Title = null });
        }

        [Fact]
        public void XPPrompt_ShowDialog_ShouldNotThrowError()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowDialog();
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_WithParent_ShouldNotThrowError()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_Without_Target_ShouldThrowError()
        {
            var prompt = new XPPrompt();
            Assert.Throws<InvalidOperationException>(() => prompt.ShowDialog());
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_With_Username()
        {
            var prompt = GetDefaultPrompt();
            prompt.Username = "username";
            prompt.Title = "Enter enter valid credentials.";
            prompt.Message = "Please enter valid credentials.";
            prompt.ShowSaveCheckBox = true;
            prompt.GenericCredentials = true;
            Assert.Equal(DialogResult.OK, prompt.ShowDialog());
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_AlwaysShowUI()
        {
            var prompt = GetDefaultPrompt();
            prompt.AlwaysShowUI = true;
            prompt.GenericCredentials = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_AlwaysShowUI_Without_GenericCredentials_ShouldThrowError()
        {
            var prompt = GetDefaultPrompt();
            prompt.AlwaysShowUI = true;
            Assert.Throws<InvalidOperationException>(() => prompt.ShowDialog(IntPtr.Zero));
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_CompleteUsername()
        {
            var prompt = GetDefaultPrompt();
            prompt.CompleteUsername = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_DoNotPersist()
        {
            var prompt = GetDefaultPrompt();
            prompt.DoNotPersist = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_ExcludeCertificates()
        {
            var prompt = GetDefaultPrompt();
            prompt.ExcludeCertificates = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_ExpectConfirmation()
        {
            var prompt = GetDefaultPrompt();
            prompt.ExpectConfirmation = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_GenericCredentials()
        {
            var prompt = GetDefaultPrompt();
            prompt.GenericCredentials = true;
            Assert.Equal(DialogResult.OK, prompt.ShowDialog(IntPtr.Zero));
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_IncorrectPassword()
        {
            var prompt = GetDefaultPrompt();
            prompt.IncorrectPassword = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_Persist()
        {
            var prompt = GetDefaultPrompt();
            prompt.Persist = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_RequestAdministrator()
        {
            var prompt = GetDefaultPrompt();
            prompt.RequestAdministrator = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_RequreCertificate()
        {
            var prompt = GetDefaultPrompt();
            prompt.RequireCertificate = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_RequreSmartCard()
        {
            var prompt = GetDefaultPrompt();
            prompt.RequireSmartCard = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_ShowSaveCheckBox()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowSaveCheckBox = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_UsernameReadOnly()
        {
            var prompt = GetDefaultPrompt();
            prompt.UsernameReadOnly = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void XPPrompt_ShowDialog_ValidateUsername()
        {
            var prompt = GetDefaultPrompt();
            prompt.ValidateUsername = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        private XPPrompt GetDefaultPrompt()
        {
            var prompt = new XPPrompt { Target = "target" };
            return prompt;
        }
    }
}