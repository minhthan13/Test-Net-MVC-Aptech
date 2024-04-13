namespace TestNetMVC.Helpers
{
  public class FileHelper
  {
    public static string generateFileName(string fileName)
    {
      // Replace() xoa dau gach ngang trong chuoi GUID
      var name = Guid.NewGuid().ToString().Replace("-", "");
      var lastIndex = fileName.LastIndexOf('.');
      var ext = fileName.Substring(lastIndex);
      return name + ext;
    }
  }
}
