<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128593930/16.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E965)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [FileSystemLinkObject.cs](./CS/FileSystemData/BusinessObjects/FileSystemLinkObject.cs) (VB: [FileSystemLinkObject.vb](./VB/FileSystemData/BusinessObjects/FileSystemLinkObject.vb))
* **[FileSystemStoreObject.cs](./CS/FileSystemData/BusinessObjects/FileSystemStoreObject.cs) (VB: [FileSystemStoreObject.vb](./VB/FileSystemData/BusinessObjects/FileSystemStoreObject.vb))**
<!-- default file list end -->
# How to: Store file attachments in the file system instead of the database (XPO)


<p><strong>Scenario</strong><br>The FileSystemData module provides the FileSystemStoreObject and FileSystemLinkObject classes that implement the IFileData interface for the use with <a href="https://documentation.devexpress.com/#Xaf/CustomDocument2781">our File Attachments module</a>.<br><em>FileSystemStoreObject</em> - this class enables you to store uploaded files in a centralized file system location instead of the database. You can configure the file system store location via the static <em>FileSystemDataModule.FileSystemStoreLocation</em> property.<br><em>FileSystemLinkObject</em> - this class enables you to add soft links to real files instead of saving their contents to the database. Apparently, it is intended for use in Windows Forms applications only.</p>
<p>Refer to the following video to see this functionality in action: <u></u><u><a href="http://www.screencast.com/t/Xl1GMfxw">http://www.screencast.com/t/Xl1GMfxw</a></u><br><br><strong>Steps to implement<br></strong><strong>1.</strong> Copy and include the FileSystemData project into your solution and make sure it is built successfully.<strong><br></strong></p>
<p><strong>2.</strong> Invoke the Module Designer for the <em>YourSolutionName.Module/Module.xx</em> file by double-clicking it in Solution Explorer. Invoke the Toolbox (Alt+X+T) and then drag & drop the FileSystemDataModule component into the modules list on the left.<br><strong>3.</strong> Define a FileSystemStoreObject or FileSystemLinkObject type properties within your business class as described in the <a href="https://documentation.devexpress.com/#Xaf/CustomDocument2658">eXpressApp Framework > Task-Based Help > How to: Implement File Data Properties</a> article. Make sure to decorate the container business class with the <a href="https://documentation.devexpress.com/#eXpressAppFramework/clsDevExpressPersistentBaseFileAttachmentAttributetopic">FileAttachmentAttribute</a> (to provide additional commands for working with files) and also do not miss the Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never) and ImmediatePostData attributes for the new class properties. See the <em>E965.Module\BusinessObjects\FileSystemStoreObjectDemo.xx</em> and <em>E965.Module\BusinessObjects\FileSystemLinkObjectDemo.xx</em>  source files for examples. </p>
<p><strong>4.</strong> Make sure you do not override the <em>DevExpress.Persistent.BaseImpl.BaseObject.OidInitializationMode</em> property in your application and related modules, because the <em>OidInitializationMode.AfterConstruction</em> value is necessary for the correct operation of this module (in the example, the required default value is already set in the <em>FileSystemDataModule</em> class of this example module).<br><strong>5.</strong> Modify <em>YourSolutionName.Win/WinApplication.xx</em> file to handle the <em>CustomOpenFileWithDefaultProgram</em> event of the <em>DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule</em> class as shown in the <em>E965.Win\WinApplication.xx</em> file.</p>
<p> </p>
<p><strong> IMPORTANT NOTES<br> 1. </strong>The current version of this example does not support the middle-tier scenario. Refer to the <a href="http://www.devexpress.com/issue=Q476039"><u>Q476039</u></a> ticket for more details.<br><strong>2.</strong> If you plan to migrate existing FileData objects from the database to a file system, you can use the techniques described in the <a href="https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113708">eXpressApp Framework > Concepts > Data Manipulation and Business Logic</a> article to create read FileData objects and create new FileSystemStoreObject objects based on their content. Since both classes implement IFileData, you can call their LoadFromStream and SaveToStream methods to copy data. Even though we do not provide a ready example for this migration procedure, we hope that the example below will be helpful for getting started:</p>


```cs
            FileData fd = ObjectSpace.FindObject<FileData>(null); // Use any other IObjectSpace APIs to query required data.
            FileSystemStoreObject fss = ObjectSpace.CreateObject<FileSystemStoreObject>();
            Stream sourceStream = new MemoryStream();
            ((IFileData)fd).SaveToStream(sourceStream);
            sourceStream.Position = 0;
            ((IFileData)fss).LoadFromStream(fd.FileName, sourceStream);
            ObjectSpace.CommitChanges();
```


<p>Of course, you can rework this code to use UnitOfWork instead of IObjectSpace.</p>

## See Also
- <a href="http://documentation.devexpress.com/#Xaf/CustomDocument2781">File Attachments Module Overview</a>
- <a href="http://dennisgaravsky.blogspot.com/2012/10/working-with-links-to-files-instead-of.html">Working with links to files instead of storing their contents in the database</a>
- [XAF - Store file attachments in Dropbox instead of the database](https://github.com/egarim/FileDataDropBox) (XPO) ([videos](https://www.youtube.com/watch?v=lVfUeDj9T7U) and [additional information](https://www.bitframeworks.com/devexpress-xaf-cloud-filedata-module/) on XAF Cloud FileData Module by BitFrameworks
- <a href="http://technet.microsoft.com/en-us/library/bb933993(v=sql.105).aspx"><u>SQL Server FILESTREAM feature Overview</a>
