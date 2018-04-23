using System;
using System.IO;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace FileSystemData.BusinessObjects {
    /// <summary>
    /// This class enables you to add soft links to real files instead of saving their contents to the database. It is intended for use in Windows Forms applications only.
    /// </summary>
    [DefaultProperty("FileName")]
    public class FileSystemLinkObject : BaseObject, IFileData, IEmptyCheckable, ISupportFullName {
        public FileSystemLinkObject(Session session) : base(session) { }
        #region IFileData Members
        [Size(260), Custom("AllowEdit", "False")]
        public string FileName {
            get { return GetPropertyValue<string>("FileName"); }
            set { SetPropertyValue("FileName", value); }
        }
        void IFileData.Clear() {
            Size = 0;
            FileName = string.Empty;
        }
        //Dennis: Fires when uploading a file.
        void IFileData.LoadFromStream(string fileName, Stream source) {
            Size = (int)source.Length;
            FileName = fileName;
        }
        //Dennis: Fires when saving or opening a file.
        void IFileData.SaveToStream(Stream destination) {
            try {
                if (destination == null)
                    FileSystemDataModule.OpenFileWithDefaultProgram(FullName);
                else
                    FileSystemDataModule.CopyFileToStream(FullName, destination);
            } catch (Exception exc) {
                throw new UserFriendlyException(exc);
            }
        }
        [Persistent]
        public int Size {
            get { return GetPropertyValue<int>("Size"); }
            private set { SetPropertyValue<int>("Size", value); }
        }
        #endregion
        #region IEmptyCheckable Members
        public bool IsEmpty {
            get { return !File.Exists(FullName); }
        }
        #endregion
        #region ISupportFullName Members
        [Size(260), Custom("AllowEdit", "False")]
        public string FullName {
            get { return GetPropertyValue<string>("FullName"); }
            set { SetPropertyValue<string>("FullName", value); }
        }
        #endregion
    }
}