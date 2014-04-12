using System;
using System.IO;
using System.Text;
using RemoteTaskPlatform.Contract;

namespace RemoteTaskPlatform.DefaultEntities
{
	public class DefaultCloneFilesAndAssembles : ICloneFilesAndAssembles
	{
		private readonly string REMOTETASKSBASEPATH = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "REMOTETASKS");

		public string CloneFilesAndAssembles(IRemoteTaskRequest remoteTask)
		{
			var remoteTaskDirectoryPath = Path.Combine(this.REMOTETASKSBASEPATH, remoteTask.RemoteTaskIdentity.ToString());
			if (!Directory.Exists(remoteTaskDirectoryPath))
			{
				Directory.CreateDirectory(remoteTaskDirectoryPath);
			}
			this.SaveFile(remoteTask.CodeFile.CodeFileBytes, remoteTaskDirectoryPath, remoteTask.CodeFile.CodeFileName);
			if (remoteTask.ReferencedAssembles == null) return remoteTaskDirectoryPath;
			foreach (var referencedAssemble in remoteTask.ReferencedAssembles)
			{
				this.SaveFile(referencedAssemble.ReferencedAssembleBytes, remoteTaskDirectoryPath, referencedAssemble.ReferencedAssembleName);
			}
			return remoteTaskDirectoryPath;
		}

		private void SaveFile(byte[] bytes, string remoteTaskDirectoryPath, string fileName)
		{
			using (var fileStream = new FileStream(Path.Combine(remoteTaskDirectoryPath, fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				using (var streamWrite = new StreamWriter(fileStream))
				{
					streamWrite.Write(Encoding.Default.GetString(bytes).Trim());
				}
			}
		}
	}
}