using System.Runtime.Serialization;

namespace RolesAuthorize.Contracts.Models.User
{
    [DataContract]
    public class Reader
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string UserName { get; set; }
        
        [DataMember]
        public string EmailAddress { get; set; }
    }
}