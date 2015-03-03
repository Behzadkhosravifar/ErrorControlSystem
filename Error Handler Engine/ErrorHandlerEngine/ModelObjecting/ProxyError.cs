﻿using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace ErrorHandlerEngine.ModelObjecting
{
    [Serializable]
    public class ProxyError : IError, IDisposable, ICloneable, IEquatable<ProxyError>, ISerializable
    {
        #region Properties

        #region Snapshot Image Object
        public Lazy<System.Drawing.Image> Snapshot { get; set; }
        #endregion

        #region Get Error

        public Error GetErrorObject
        {
            get
            {
                return new Error()
                    {
                        Id = this.Id,
                        IsHandled = this.IsHandled,
                        ErrorDateTime = this.ErrorDateTime,
                        ServerDateTime = this.ServerDateTime,
                        HResult = this.HResult,
                        AppName = this.AppName,
                        ClrVersion = this.ClrVersion,
                        CurrentCulture = this.CurrentCulture,
                        ErrorType = this.ErrorType,
                        Host = this.Host,
                        IPv4Address = this.IPv4Address,
                        MacAddress = this.MacAddress,
                        MemberType = this.MemberType,
                        Message = this.Message,
                        Method = this.Method,
                        ModuleName = this.ModuleName,
                        OS = this.OS,
                        Processes = this.Processes,
                        SnapshotAddress = this.SnapshotAddress,
                        Source = this.Source,
                        StackTrace = this.StackTrace,
                        User = this.User,
                        ErrorLineColumn = this.ErrorLineColumn,
                        Duplicate = this.Duplicate
                    };
            }
        }
        #endregion

        #endregion

        #region Constructor
        public ProxyError() { }

        public ProxyError(Error error)
        {
            #region Initialize IError Properties

            Id = error.Id;
            IsHandled = error.IsHandled;
            ErrorDateTime = error.ErrorDateTime;
            ServerDateTime = error.ServerDateTime;
            HResult = error.HResult;
            AppName = error.AppName;
            ClrVersion = error.ClrVersion;
            CurrentCulture = error.CurrentCulture;
            ErrorType = error.ErrorType;
            Host = error.Host;
            IPv4Address = error.IPv4Address;
            MacAddress = error.MacAddress;
            MemberType = error.MemberType;
            Message = error.Message;
            Method = error.Method;
            ModuleName = error.ModuleName;
            OS = error.OS;
            Processes = error.Processes;
            SnapshotAddress = error.SnapshotAddress;
            Source = error.Source;
            StackTrace = error.StackTrace;
            User = error.User;
            ErrorLineColumn = error.ErrorLineColumn;
            Duplicate = error.Duplicate;
            #endregion

            #region Initialize Lazy<Image> Snapshot
            // Initialize by invoking a specific constructor on Order when Value property is accessed
            Snapshot = new Lazy<Image>(() => string.IsNullOrEmpty(SnapshotAddress) || !File.Exists(SnapshotAddress)
                ? null
                : ScreenCapture.FromFile(SnapshotAddress));
            #endregion
        }

        public ProxyError(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            Id = (int)info.GetValue("Id", typeof(int));
            IsHandled = (bool)info.GetValue("IsHandled", typeof(bool));
            ErrorDateTime = (DateTime)info.GetValue("ErrorDateTime", typeof(DateTime));
            ServerDateTime = (DateTime)info.GetValue("ServerDateTime", typeof(DateTime));
            HResult = (int)info.GetValue("HResult", typeof(int));
            AppName = (string)info.GetValue("AppName", typeof(string));
            ClrVersion = (string)info.GetValue("ClrVersion", typeof(string));
            CurrentCulture = (string)info.GetValue("CurrentCulture", typeof(string));
            ErrorType = (string)info.GetValue("ErrorType", typeof(string));
            Host = (string)info.GetValue("Host", typeof(string));
            IPv4Address = (string)info.GetValue("IPv4Address", typeof(string));
            MacAddress = (string)info.GetValue("MacAddress", typeof(string));
            MemberType = (string)info.GetValue("MemberType", typeof(string));
            Message = (string)info.GetValue("Message", typeof(string));
            Method = (string)info.GetValue("Method", typeof(string));
            ModuleName = (string)info.GetValue("ModuleName", typeof(string));
            OS = (string)info.GetValue("OS", typeof(string));
            Processes = (string)info.GetValue("Processes", typeof(string));
            SnapshotAddress = (string)info.GetValue("SnapshotAddress", typeof(string));
            Source = (string)info.GetValue("Source", typeof(string));
            StackTrace = (string)info.GetValue("StackTrace", typeof(string));
            User = (string)info.GetValue("User", typeof(string));
            ErrorLineColumn = (CodeLocation)info.GetValue("ErrorLineColumn", typeof(CodeLocation));
            Duplicate = (int)info.GetValue("Duplicate", typeof(int));
        }

        #endregion

        #region IError Implement
        public int Id { get; set; }
        public bool IsHandled { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public DateTime ServerDateTime { get; set; }
        public int HResult { get; set; }
        public string AppName { get; set; }
        public string ClrVersion { get; set; }
        public string CurrentCulture { get; set; }
        public string ErrorType { get; set; }
        public string Host { get; set; }
        public string IPv4Address { get; set; }
        public string MacAddress { get; set; }
        public string MemberType { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public string ModuleName { get; set; }
        public string OS { get; set; }
        public string Processes { get; set; }
        public string SnapshotAddress { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string User { get; set; }
        public CodeLocation ErrorLineColumn { get; set; }
        public int Duplicate { get; set; }
        #endregion

        #region IDisposable Implement
        public void Dispose()
        {
            Id = -1;
            AppName = String.Empty;
            ClrVersion = String.Empty;
            CurrentCulture = String.Empty;
            ErrorDateTime = DateTime.MinValue;
            ErrorType = String.Empty;
            Host = String.Empty;
            HResult = 0;
            IPv4Address = String.Empty;
            IsHandled = false;
            MacAddress = String.Empty;
            MemberType = String.Empty;
            Message = String.Empty;
            Method = String.Empty;
            ModuleName = String.Empty;
            OS = null;
            Processes = null;
            ServerDateTime = DateTime.MinValue;
            Snapshot = null;
            SnapshotAddress = String.Empty;
            Source = String.Empty;
            StackTrace = String.Empty;
            User = String.Empty;
            ErrorLineColumn = CodeLocation.Empty;
            Duplicate = 0;
        }
        #endregion

        #region ICloneable Implement
        object ICloneable.Clone()
        {
            return Clone(false);
        }

        public object Clone(bool lightCopy = true)
        {
            return lightCopy ? GetLightCopy() : this.MemberwiseClone();
        }

        public IError GetLightCopy()
        {
            var instance = new Error();

            foreach (var property in typeof(IError).GetProperties())
            {
                typeof(Error).GetProperty(property.Name)
                    .SetValue(instance, typeof(Error).GetProperty(property.Name).GetValue(this));
            }

            return instance;
        }
        #endregion

        #region IEquatable<LazyError> Implement

        public bool Equals(ProxyError other)
        {
            if (other == null) return false;

            // Note value types can't have derived classes, so we don't need 
            return this.ErrorLineColumn == other.ErrorLineColumn &&
                   this.HResult == other.HResult;
        }

        public bool Equals(ProxyError x, ProxyError y)
        {
            if (x == null) return false;
            if (y == null) return false;

            // Note value types can't have derived classes, so we don't need 
            return x.ErrorLineColumn == y.ErrorLineColumn &&
                   x.HResult == y.HResult;
        }

        /// <devdoc>
        ///    <para>
        ///       Specifies whether this <see cref='ProxyError'/> contains
        ///       the same coordinates as the specified <see cref='System.Object'/>.
        ///    </para>
        /// </devdoc>
        public override bool Equals(object obj)
        {
            if (!(obj is ProxyError)) return false;
            var comp = (ProxyError)obj;
            // Note value types can't have derived classes, so we don't need 
            // to check the types of the objects here.  -- [....], 2/21/2001
            return comp.ErrorLineColumn == this.ErrorLineColumn &&
                   comp.HResult == this.HResult;
        }

        /// <devdoc>
        ///    <para>
        ///       Returns a hash code.
        ///    </para>
        /// </devdoc>
        public override int GetHashCode()
        {
            // Unique ID  =  Line×1000   XOR   Column   XOR   |HResult|
            return unchecked(this.ErrorLineColumn.Line * 1000 ^ this.ErrorLineColumn.Column ^ Math.Abs(this.HResult));
        }

        #endregion

        #region Implement ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.Id);
            info.AddValue("IsHandled", this.IsHandled);
            info.AddValue("ErrorDateTime", this.ErrorDateTime);
            info.AddValue("ServerDateTime", this.ServerDateTime);
            info.AddValue("HResult", this.HResult);
            info.AddValue("AppName", this.AppName);
            info.AddValue("ClrVersion", this.ClrVersion);
            info.AddValue("CurrentCulture", this.CurrentCulture);
            info.AddValue("ErrorType", this.ErrorType);
            info.AddValue("Host", this.Host);
            info.AddValue("IPv4Address", this.IPv4Address);
            info.AddValue("MacAddress", this.MacAddress);
            info.AddValue("MemberType", this.MemberType);
            info.AddValue("Message", this.Message);
            info.AddValue("Method", this.Method);
            info.AddValue("ModuleName", this.ModuleName);
            info.AddValue("OS", this.OS);
            info.AddValue("Processes", this.Processes);
            info.AddValue("SnapshotAddress", this.SnapshotAddress);
            info.AddValue("Source", this.Source);
            info.AddValue("StackTrace", this.StackTrace);
            info.AddValue("User", this.User);
            info.AddValue("ErrorLineColumn", this.ErrorLineColumn);
            info.AddValue("Duplicate", this.Duplicate);
        }

        #endregion
    }
}