using System.Web.Mvc;

namespace CRM.Application.Interfaces.Services;

public interface ICodeService
{
	public byte[] GenerateCode(string payload);
}

public interface IFileService
{
	public FileContentResult GenerateFile(byte[] file);
}