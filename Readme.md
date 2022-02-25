<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128593930/21.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E965)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to: Store file attachments in the file system instead of the database (XPO)  

## Scenario  

The *FileSystemData* module provides the *FileSystemStoreObject* and *FileSystemLinkObject* classes that implement the *IFileData* interface for the use with our [File Attachments module](https://docs.devexpress.com/eXpressAppFramework/112781/document-management/file-attachments-module).  
* *FileSystemStoreObject* - this class enables you to store uploaded files in a centralized file system location instead of the database. You can configure the file system store location via the static *FileSystemDataModule.FileSystemStoreLocation* property.  
* *FileSystemLinkObject* - this class enables you to add soft links to real files instead of saving their contents to the database. Apparently, it is intended for use in Windows Forms applications only.  

Refer to the following video to see this functionality in action: [http://www.screencast.com/t/Xl1GMfxw](http://www.screencast.com/t/Xl1GMfxw).

## Steps to implement  

1. Copy and include the ***FileSystemData*** project into your solution and make sure it is built successfully.  
2. Register the FileSystemDataModule in code in the *YourSolutionName.Module/Module.xx* file as described in the following help topic: [Ways to Register a Module > Add a Module in Code](https://docs.devexpress.com/eXpressAppFramework/118047/application-shell-and-base-infrasctructure/application-solution-components/ways-to-register-a-module#code).  
3. Define a *FileSystemStoreObject* or *FileSystemLinkObject* type properties within your business class as described in this help topic: [Implement File Data Properties](https://docs.devexpress.com/eXpressAppFramework/112658/business-model-design-orm/business-model-design-with-xpo/implement-file-data-properties). Make sure to decorate the container business class with the [FileAttachmentAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.FileAttachmentAttribute) (to provide additional commands for working with files) and also do not miss the Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never) and ImmediatePostData attributes for the new class properties. See the *E965.Module\BusinessObjects\FileSystemStoreObjectDemo.xx* and *E965.Module\BusinessObjects\FileSystemLinkObjectDemo.xx*  source files for examples.  
4. Make sure you do not override the *DevExpress.Persistent.BaseImpl.BaseObject.OidInitializationMode* property in your application and related modules, because the *OidInitializationMode.AfterConstruction* value is necessary for the correct operation of this module (in the example, the required default value is already set in the *FileSystemDataModule* class of this example module).  
5. Modify the *YourSolutionName.Win/WinApplication.xx* file to handle the *CustomOpenFileWithDefaultProgram* event of the *DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule* class as shown in the *E965.Win\WinApplication.xx* file.  

## IMPORTANT NOTES 

1. The *FileSystemLinkObject* class can be used in Winforms applications only.  
2. The current version of this example does not support the middle-tier scenario. For more details, refer to the following ticket: [A problem occurs when file attachments are stored in the file system with middle-tier configuration](https://supportcenter.devexpress.com/ticket/details/q476039).  
3. If you plan to migrate existing FileData objects from the database to a file system, you can use the techniques described in the [Data Manipulation and Business Logic](https://docs.devexpress.com/eXpressAppFramework/113708/data-manipulation-and-business-logic) help topic to create, read FileData objects and create new FileSystemStoreObject objects based on their content. Since both classes implement IFileData, you can call their LoadFromStream and SaveToStream methods to copy data. Even though we do not provide a ready example for this migration procedure, we hope that the example below will be helpful for getting started:  

```cs
FileData fd = ObjectSpace.FindObject<FileData>(null); // Use any other IObjectSpace APIs to query required data.
FileSystemStoreObject fss = ObjectSpace.CreateObject<FileSystemStoreObject>();
Stream sourceStream = new MemoryStream();
((IFileData)fd).SaveToStream(sourceStream);
sourceStream.Position = 0;
((IFileData)fss).LoadFromStream(fd.FileName, sourceStream);
ObjectSpace.CommitChanges();
```

Of course, you can rework this code to use UnitOfWork instead of IObjectSpace.  

<!-- default file list -->
## Files to look at  

* [FileSystemLinkObject.cs](./CS/FileSystemData/BusinessObjects/FileSystemLinkObject.cs) (VB: [FileSystemLinkObject.vb](./VB/FileSystemData/BusinessObjects/FileSystemLinkObject.vb))
* **[FileSystemStoreObject.cs](./CS/FileSystemData/BusinessObjects/FileSystemStoreObject.cs) (VB: [FileSystemStoreObject.vb](./VB/FileSystemData/BusinessObjects/FileSystemStoreObject.vb))**
<!-- default file list end -->

## See Also  
- [File Attachments (Store Custom Files)](https://docs.devexpress.com/eXpressAppFramework/112781/document-management/file-attachments-module)  
- [Working with links to files instead of storing their contents in the database](https://dennisgaravsky.blogspot.com/2012/10/working-with-links-to-files-instead-of.html)  
- [XAF - Store file attachments in Dropbox instead of the database](https://github.com/egarim/FileDataDropBox) (XPO) ([videos](https://www.youtube.com/watch?v=lVfUeDj9T7U) and [additional information](https://www.bitframeworks.com/devexpress-xaf-cloud-filedata-module/) on XAF Cloud FileData Module by BitFrameworks)  
- [FILESTREAM (SQL Server)](https://docs.microsoft.com/en-us/sql/relational-databases/blob/filestream-sql-server?view=sql-server-ver15)  
