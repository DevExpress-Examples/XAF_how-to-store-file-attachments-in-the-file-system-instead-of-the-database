using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using FileSystemData.BusinessObjects;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileSystemLinkObjectDemo : BaseObject {
        public FileSystemLinkObjectDemo(Session session) : base(session) { }
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public FileSystemLinkObject File {
            get { return GetPropertyValue<FileSystemLinkObject>("File"); }
            set { SetPropertyValue<FileSystemLinkObject>("File", value); }
        }
    }
}
