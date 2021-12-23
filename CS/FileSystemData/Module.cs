using System;
using System.IO;
using System.ComponentModel;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.BaseImpl;

namespace FileSystemData {
    [Description("This module provides the FileSystemStoreObject and FileSystemLinkObject classes that enable you to store uploaded files in a file system instead of the database.")]
    public sealed partial class FileSystemDataModule : ModuleBase {
        public static int ReadBytesSize = 0x1000;
        public static string FileSystemStoreLocation = String.Format("{0}FileData", PathHelper.GetApplicationFolder());
        public FileSystemDataModule() {
            InitializeComponent();
            BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
        }
        public static void CopyFileToStream(string sourceFileName, Stream destination) {
            if (string.IsNullOrEmpty(sourceFileName) || destination == null) return;
            using (Stream source = File.OpenRead(sourceFileName))
                CopyStream(source, destination);
        }
        public static void OpenFileWithDefaultProgram(string sourceFileName) {
            Guard.ArgumentNotNullOrEmpty(sourceFileName, "sourceFileName");

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = sourceFileName;
            process.Start();
        }
        public static void CopyStream(Stream source, Stream destination) {
            if (source == null || destination == null) return;
            byte[] buffer = new byte[ReadBytesSize];
            int read = 0;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, read);
        }
    }
}