namespace PoroDev.Common.Models.UserModels.DeleteUser
{
    public class DeleteUserModel
    {
        public bool Deleted { get; set; }

        public DeleteUserModel(bool deleted)
        {
            Deleted = deleted;
        }

        public DeleteUserModel()
        {
        }
    }
}