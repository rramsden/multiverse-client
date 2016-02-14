using System;
using System.Collections.Generic;
using System.Text;

namespace Multiverse.Utility
{
	public static class Misc
	{
		public static string HexBytes(byte[] data, int index, int length)
		{

			string sDump = (length > 0 ? BitConverter.ToString(data, index, length) : "");
			string[] sDumpHex = sDump.Split('-');
			List<string> lstDump = new List<string>();

			string sHex = "";
			string sAscii = "";
			char cByte = '\0';

			if (sDump.Length > 0)
			{
				for (Int32 iCount = 0; iCount < sDumpHex.Length; iCount++)
				{
					cByte = Convert.ToChar(data[index + iCount]);
					sHex += sDumpHex[iCount] + ' ';

					if (char.IsWhiteSpace(cByte) || char.IsControl(cByte))
					{
						cByte = '.';
					}

					sAscii += cByte.ToString();
					if ((iCount + 1) % 16 == 0)
					{
						lstDump.Add(sHex + " " + sAscii);
						sHex = "";
						sAscii = "";
					}
				}
				if (sHex.Length > 0)
				{
					if (sHex.Length < (16 * 3)) sHex += new string(' ', (16 * 3) - sHex.Length);
					lstDump.Add(sHex + " " + sAscii);
				}
			}
			string retval = "";
			for (Int32 iCount = 0; iCount < lstDump.Count; iCount++)
			{
				retval += lstDump[iCount] + "\n";

			}
			return retval;

		}

		public static string HexBytes(byte[] data)
		{
			int index = 0;
			int length = data.Length;

			string sDump = (length > 0 ? BitConverter.ToString(data, index, length) : "");
			string[] sDumpHex = sDump.Split('-');
			List<string> lstDump = new List<string>();

			string sHex = "";
			string sAscii = "";
			char cByte = '\0';

			if (sDump.Length > 0)
			{
				for (Int32 iCount = 0; iCount < sDumpHex.Length; iCount++)
				{
					cByte = Convert.ToChar(data[index + iCount]);
					sHex += sDumpHex[iCount] + ' ';

					if (char.IsWhiteSpace(cByte) || char.IsControl(cByte))
					{
						cByte = '.';
					}

					sAscii += cByte.ToString();
					if ((iCount + 1) % 16 == 0)
					{
						lstDump.Add(sHex + " " + sAscii);
						sHex = "";
						sAscii = "";
					}
				}
				if (sHex.Length > 0)
				{
					if (sHex.Length < (16 * 3)) sHex += new string(' ', (16 * 3) - sHex.Length);
					lstDump.Add(sHex + " " + sAscii);
				}
			}
			string retval = "";
			for (Int32 iCount = 0; iCount < lstDump.Count; iCount++)
			{
				retval += lstDump[iCount] + "\n";

			}
			return retval;

		}
	}
}