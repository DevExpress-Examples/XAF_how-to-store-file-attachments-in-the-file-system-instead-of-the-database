<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128593930/23.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E965)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to: Store file attachments in the file system instead of the database (XPO and EF Core)  

## Basics  

This example contains a **FileSystemDataModule** - an XAF module that you can reuse in your own applications. You will be interested in two types that allow you to attach a file to an object. Both types do not store the files in the database, but keep them in the file system:

* **FileSystemStoreObject** - stores files in a centralized file system location instead of the database. Use the module's static **FileSystemStoreLocation** property to specify the file location.  
* **FileSystemLinkObject** - stores soft links to files instead of saving their contents to the database. This type is intended for use in WinForms applications only.

Both types implement the **IFileData** interface and thus work with the DevExpress [File Attachments module](https://docs.devexpress.com/eXpressAppFramework/112781/document-management/file-attachments-module).

Refer to the following video to see this functionality in action: [http://www.screencast.com/t/Xl1GMfxw](http://www.screencast.com/t/Xl1GMfxw).

## Steps to implement  

1. Copy the "FileSystemData" project and include it into your solution. Rebuild the solution and make sure the new project was built successfully.  
2. Register the **FileSystemDataModule** in the *YourSolutionName.Module/Module.xx* file as described in the following help topic: [Ways to Register a Module > Add a Module in Code](https://docs.devexpress.com/eXpressAppFramework/118047/application-shell-and-base-infrasctructure/application-solution-components/ways-to-register-a-module#code).  
3. Extend a business class with a property that will hold file attachments. Choose the **FileSystemStoreObject** or **FileSystemLinkObject** type for the property. See descriptions above to compare the two types. 
4. Add the following attributes to the file attachment property: **Aggregated**, **ExpandObjectMembers(ExpandObjectMembers.Never)** and **ImmediatePostData**.  
5. Decorate the business class with the [FileAttachmentAttribute](https://docs.devexpress.com/eXpressAppFramework/DevExpress.Persistent.Base.FileAttachmentAttribute) that links to the new property. This attribute enables additional file management UI commands.
6. Handle the **CustomOpenFileWithDefaultProgram** event of the *DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule* class. Review the implementation in this example (*E965.Win\WinApplication.xx*). Move that code to the following file in your project: *YourSolutionName.Win/WinApplication.xx*.  
7. **XPO Only**. Make sure that your application and related modules do not override the *DevExpress.Persistent.BaseImpl.BaseObject.OidInitializationMode* property. The property must return *OidInitializationMode.AfterConstruction* to ensure the correct operation of this module. (**FileSystemDataModule** sets this value as needed.)  

## Important Notes 

1. The **FileSystemLinkObject** class can be used in WinForms applications only.  
2. The current version of this example does not support the middle-tier scenario. For more information, refer to the following ticket: [A problem occurs when file attachments are stored in the file system with middle-tier configuration](https://supportcenter.devexpress.com/ticket/details/q476039).  
3. If you plan to migrate existing **FileData** objects from the database to a file system, use the techniques described in the following topic: [Data Manipulation and Business Logic](https://docs.devexpress.com/eXpressAppFramework/113708/data-manipulation-and-business-logic). The article explains how you can read **FileData** objects and create new **FileSystemStoreObject** objects based on their content. Since both classes implement **IFileData**, you can call their **LoadFromStream()** and **SaveToStream()** methods to copy data. Even though we do not provide an example for this migration procedure, we hope that the snippet below help you get started:  

```cs
// Use any other IObjectSpace APIs to query required data.
FileData fd = ObjectSpace.FindObject<FileData>(null); 
FileSystemStoreObject fss = ObjectSpace.CreateObject<FileSystemStoreObject>();
Stream sourceStream = new MemoryStream();
((IFileData)fd).SaveToStream(sourceStream);
sourceStream.Position = 0;
((IFileData)fss).LoadFromStream(fd.FileName, sourceStream);
ObjectSpace.CommitChanges();
```

Note that you can rework this code to use **UnitOfWork** instead of **IObjectSpace**.  

## Files to Review (XPO)

* [FileSystemLinkObject.cs](./CS/XPO/FileSystemData/BusinessObjects/FileSystemLinkObject.cs) 
* [FileSystemStoreObject.cs](./CS/XPO/FileSystemData/BusinessObjects/FileSystemStoreObject.cs) 

## Files to Review (EF Core)

* [FileSystemLinkObject.cs](./CS/EFCore/FileSystemData/BusinessObjects/FileSystemLinkObject.cs) 
* [FileSystemStoreObject.cs](./CS/EFCore/FileSystemData/BusinessObjects/FileSystemStoreObject.cs)

## Documentation

- [File Attachments (Store Custom Files)](https://docs.devexpress.com/eXpressAppFramework/112781/document-management/file-attachments-module)  


## See Also  

- [Working with links to files instead of storing their contents in the database](https://dennisgaravsky.blogspot.com/2012/10/working-with-links-to-files-instead-of.html)  
- [XAF - Store file attachments in Dropbox instead of the database](https://github.com/egarim/FileDataDropBox) (XPO)  
- [FILESTREAM (SQL Server)](https://docs.microsoft.com/en-us/sql/relational-databases/blob/filestream-sql-server?view=sql-server-ver15)  
