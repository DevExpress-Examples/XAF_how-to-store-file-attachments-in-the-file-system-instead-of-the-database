using System;
using System.IO;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;

namespace FileSystemData.BusinessObjects {
    /// <summary>
    /// This class enables you to add soft links to real files instead of saving their contents to the database. It is intended for use in Windows Forms applications only.
    /// </summary>
    [DefaultProperty(nameof(FileName))]
    public class FileSystemLinkObject : BaseObject, IFileData, IEmptyCheckable, ISupportFullName {
        #region IFileData Members
        [FieldSize(260)]
        [ReadOnly(true)]
        public virtual string FileName { get; set; }
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
        public virtual int Size { get; set; }
        #endregion
        #region IEmptyCheckable Members
        public bool IsEmpty {
            get { return !File.Exists(FullName); }
        }
        #endregion
        #region ISupportFullName Members
        [FieldSize(260)]
        [ReadOnly(true)]
        public virtual string FullName { get; set; }
        #endregion
    }
}