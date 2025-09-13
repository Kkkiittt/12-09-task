using System.Web.Mvc;

using CRM.Application.Interfaces.Services;

using QRCoder;

namespace CRM.Application.Implementations.Services;

public class CodeService : ICodeService
{
	public byte[] GenerateCode(string payload)
	{
		QRCodeGenerator generator = new QRCodeGenerator();
		QRCodeData data = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L);
		var code = new PngByteQRCode(data);
		return code.GetGraphic(20);
	}
}

public class FileService : IFileService
{
	public FileContentResult GenerateFile(byte[] file)
	{
		return new FileContentResult(file, "image/png");
	}
}

