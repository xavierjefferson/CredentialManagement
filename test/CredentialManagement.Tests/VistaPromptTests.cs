using System;
using System.Text;
using Xunit;

namespace CredentialManagement.Tests
{
    public class VistaPromptTests : IDisposable
    {
        private static string MAX_LENGTH_VALIDATION_TEXT;

        public VistaPromptTests()
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
        public void VistaPrompt_Create_ShouldNotBeNull()
        {
            Assert.NotNull(new VistaPrompt());
        }

        [Fact]
        public void VistaPrompt_ShouldImplement_IDisposable()
        {
            Assert.True(new VistaPrompt() is IDisposable, "VistaPrompt should implement IDisposable interface.");
        }

        [Fact]
        public void VistaPrompt_Username_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new VistaPrompt { Username = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void VistaPrompt_Username_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new VistaPrompt { Username = null });
        }

        [Fact]
        public void VistaPrompt_Password_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new VistaPrompt { Password = null });
        }

        [Fact]
        public void VistaPrompt_Message_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new VistaPrompt { Message = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void VistaPrompt_Message_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new VistaPrompt { Message = null });
        }

        [Fact]
        public void VistaPrompt_Title_MaxLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new VistaPrompt { Title = MAX_LENGTH_VALIDATION_TEXT });
        }

        [Fact]
        public void VistaPrompt_Title_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new VistaPrompt { Title = null });
        }

        [Fact]
        public void VistaPrompt_ShowDialog_ShouldNotThrowError()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowDialog();
            prompt.Dispose();
        }

        [Fact]
        public void VistaPrompt_ShowDialog_WithParent_ShouldNotThrowError()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }


        [Fact]
        public void VistaPrompt_ShowDialog_With_Username()
        {
            var prompt = GetDefaultPrompt();
            prompt.Username = "username";
            Assert.Equal(DialogResult.OK, prompt.ShowDialog());
            prompt.Dispose();
        }

        [Fact]
        public void VistaPrompt_ShowDialog_GenericCredentials()
        {
            var prompt = GetDefaultPrompt();
            prompt.Title = "Please provide credentials";
            prompt.GenericCredentials = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        [Fact]
        public void VistaPrompt_ShowDialog_ShowSaveCheckBox()
        {
            var prompt = GetDefaultPrompt();
            prompt.ShowSaveCheckBox = true;
            prompt.ShowDialog(IntPtr.Zero);
            prompt.Dispose();
        }

        private VistaPrompt GetDefaultPrompt()
        {
            return new VistaPrompt();
        }
    }
}