﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace CredentialManagement
{
    public abstract class BaseCredentialsPrompt : ICredentialsPrompt
    {
        #region Constructor(s)

        static BaseCredentialsPrompt()
        {
            lock (_lockObject)
            {
                _unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
            }
        }

        #endregion


        public virtual DialogResult ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }

        public abstract DialogResult ShowDialog(IntPtr owner);

        #region Private Methods

        protected void CheckNotDisposed()
        {
            if (_disposed) throw new ObjectDisposedException("CredentialsPrompt object is already disposed.");
        }

        #endregion

        #region Fields

        private bool _disposed;
        private static readonly SecurityPermission _unmanagedCodePermission;
        private static readonly object _lockObject = new object();

        private string _username;
        private SecureString _password;
        private bool _saveChecked;
        private string _message;
        private string _title;
        private int _errorCode;

        #endregion

        #region Protected Methods

        protected void AddFlag(bool add, int flag)
        {
            if (add)
                DialogFlags |= flag;
            else
                DialogFlags &= ~flag;
        }

        protected virtual NativeMethods.CREDUI_INFO CreateCREDUI_INFO(IntPtr owner)
        {
            var credUI = new NativeMethods.CREDUI_INFO();
            credUI.cbSize = Marshal.SizeOf(credUI);
            credUI.hwndParent = owner;
            credUI.pszCaptionText = Title;
            credUI.pszMessageText = Message;
            return credUI;
        }

        #endregion

        #region Dispose Members

        public void Dispose()
        {
            Dispose(true);

            // Prevent GC Collection since we have already disposed of this object
            GC.SuppressFinalize(this);
        }

        ~BaseCredentialsPrompt()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                }

            _disposed = true;
        }

        #endregion

        #region Properties

        public bool SaveChecked
        {
            get
            {
                CheckNotDisposed();
                return _saveChecked;
            }
            set
            {
                CheckNotDisposed();
                _saveChecked = value;
            }
        }

        public string Message
        {
            get
            {
                CheckNotDisposed();
                return _message;
            }
            set
            {
                CheckNotDisposed();
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
                if (value.Length > NativeMethods.CREDUI_MAX_MESSAGE_LENGTH)
                    throw new ArgumentOutOfRangeException("value");
                _message = value;
            }
        }

        public string Title
        {
            get
            {
                CheckNotDisposed();
                return _title;
            }
            set
            {
                CheckNotDisposed();
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
                if (value.Length > NativeMethods.CREDUI_MAX_CAPTION_LENGTH)
                    throw new ArgumentOutOfRangeException("value");
                _title = value;
            }
        }

        public string Username
        {
            get
            {
                CheckNotDisposed();
                return _username ?? string.Empty;
            }
            set
            {
                CheckNotDisposed();
                if (null == value) throw new ArgumentNullException("value");
                if (value.Length > NativeMethods.CREDUI_MAX_USERNAME_LENGTH)
                    throw new ArgumentOutOfRangeException("value");
                _username = value;
            }
        }

        public string Password
        {
            get => SecureStringHelper.CreateString(SecurePassword);
            set
            {
                CheckNotDisposed();
                if (null == value) throw new ArgumentNullException("value");
                if (value.Length > NativeMethods.CREDUI_MAX_PASSWORD_LENGTH)
                    throw new ArgumentOutOfRangeException("value");
                SecurePassword =
                    SecureStringHelper.CreateSecureString(string.IsNullOrEmpty(value) ? string.Empty : value);
            }
        }

        public SecureString SecurePassword
        {
            get
            {
                CheckNotDisposed();
                _unmanagedCodePermission.Demand();
                return null == _password ? new SecureString() : _password.Copy();
            }
            set
            {
                CheckNotDisposed();
                if (null != _password)
                {
                    _password.Clear();
                    _password.Dispose();
                }

                _password = null == value ? new SecureString() : value.Copy();
            }
        }

        public int ErrorCode
        {
            get
            {
                CheckNotDisposed();
                return _errorCode;
            }
            set
            {
                CheckNotDisposed();
                _errorCode = value;
            }
        }

        public abstract bool ShowSaveCheckBox { get; set; }

        public abstract bool GenericCredentials { get; set; }

        protected int DialogFlags { get; private set; }

        #endregion
    }
}