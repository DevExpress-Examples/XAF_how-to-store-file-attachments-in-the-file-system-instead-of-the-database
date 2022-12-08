using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using FileSystemData.BusinessObjects;

namespace E965.Module.BusinessObjects {
    [DefaultClassOptions]
    [FileAttachment("File")]
    public class FileSystemLinkObjectDemo : BaseObject {
        [ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData]
        public virtual FileSystemLinkObject File { get; set; }
    }
}
