/*----------------------------------------------------------------------------
| WAGO Kontakttechnik GmbH       |                                           |
| Hansastr. 27                   |  Technical Support                        |
| D-32423 Minden                 |                                           |
| Tel.: +49(0)571 / 887 - 0      |  Tel.: +49(0)571 / 887 - 555              |
| Fax.: +49(0)571 / 887 - 169    |  Fax.: +49(0)571 / 887 - 8555             |
| Mail: info@wago.com            |  Mail: support@wago.com                   |
| www : http://www.wago.com      |                                           |
|-----------------------------------------------------------------------------
|              Copyright (C) WAGO 2001 - All Rights Reserved                 |
|-----------------------------------------------------------------------------
|  Filename    :
|  Version     : 0.00
|  Date        : 20-DEC-2003
|-----------------------------------------------------------------------------
|  Description : Wrapperclass für WAGO Modbus_TCP Library "MBT.dll".
|
|-----------------------------------------------------------------------------
|  Required    : MBT.dll (in folder System or System32)
|-----------------------------------------------------------------------------
| |Date    |Who |Ver  |Changes
|-----------------------------------------------------------------------------
|  20.12.03 CM   0.00  Init
|---------------------------------------------------------------------------*/

using System.Runtime.InteropServices; 

namespace WAGO.IO.Modbus
{
	/// <summary>
	/// Wrapperclass für WAGO Modbus_TCP Library.
	/// </summary>
	public class MBTDLL
	{
		// Constant class members -------------------------------------------
		private enum MBTTableType :byte
		{
			MODBUSTCP_TABLE_OUTPUT_REGISTER	 =  0x04,
			MODBUSTCP_TABLE_INPUT_REGISTER	 =  0x03,
			MODBUSTCP_TABLE_OUTPUT_COIL		 =  0x00,
			MODBUSTCP_TABLE_INPUT_COIL       =  0x01,
			MODBUSTCP_TABLE_EXCEPTION_STATUS =  0x07
		}

		public enum MBTError :uint 
		{
			// Class-intern generierte Errorcodes
			MBTDLL_NOT_CONNECTED_TO_DEVICE	= 0x80080001, //Nicht verbunden mit Gerät
			MBTDLL_UNSUPPORTED_ADDRESS		= 0x80080002, //Angeforderte Modbus-Adresse wird nicht unterstützt
			// DLL-intern generierte Errorcodes
			MBT_THREAD_CREATION_ERROR		= 0xEF010000, //Der Abeitsthread der MBT Library konnte nicht erzeugt werden.
			MBT_EXIT_TIMEOUT_ERROR			= 0xEF010001, //Der Abeitsthread der MBT Library konnte nicht innerhalb des Timeouts beendet werden.
			MBT_UNKNOWN_THREAD_EXIT_ERROR	= 0xEF010002, //Der Abeitsthread der MBT Library hat sich mit einem Fehler-Code beendet
			MBT_NO_ENTRY_ADDABLE_ERROR		= 0xEF010004, //Fehler in der Socket-Verwaltung der Library
			MBT_NO_JOB_ADDABLE_ERROR		= 0xEF010005, //Fehler in der Job Verwaltung der Library
			MBT_HANDLE_INVALID_ERROR		= 0xEF010006, //Das übergebene Handle ist ungültig
			MBT_SOCKET_TIMEOUT_ERROR		= 0xEF010008, //Ein Modbus/TCP Telegramm für den I/O Auftrag wurde abschickt. Innerhalb des Reqeust-Timeouts wurde jedoch keine Antwort vom Modbus/TCP empfangen.
			MBT_EXIT_ERROR					= 0xEF01000B, //Der I/O Auftrag wurde durch ein MBTExit Call abgebrochen
			//Durch Modbus-Slave generierte Errorcodes
			MBT_ILLEGAL_FUNCTION			= 0xEF01F001, //Modbus FunctionsCode wird nicht unterstützt
			MBT_ILLEGAL_DATA_ADDRESS		= 0xEF01F002, //Zugriff auf ungültige Datenadresse
			MBT_ILLEGAL_DATA_VALUE			= 0xEF01F003, //Ungültiger Datenwert, schreiben nicht möglich
			MBT_ILLEGAL_RESPONSE_LENGTH		= 0xEF01F004, //Antworttelegramm hat nicht die erwartete Länge
			MBT_ACKNOWLEDGE					= 0xEF01F005, //
			MBT_SLAVE_DEVICE_BUSY			= 0xEF01F006,
			MBT_NEGATIVE_ACKNOWLEDGE		= 0xEF01F007,
			MBT_MEMORY_PARITY_ERROR			= 0xEF01F008,
			MBT_GATEWAY_PATH_UNAVAILABLE	= 0xEF01F00A,
			MBT_GATEWAY_TARGET_DEVICE_FAILED_TO_RESPOND = 0xEF01F00B,
			//Durch WinSock(2) generierte Errorcodes
			HR_WSAEFAULT			= 0x8007271E, //Der übergebe ReadBuffer zeigt auf keinen gültigen Speicherbereich
			HR_WSAEMFILE			= 0x80072728, //Das System hat keine weiteren Socket Descriptors zur Verfügung
			HR_WSAEWOULDBLOCK		= 0x80072733, //Zu viele I/O Operationen sind zur Zeit ausständig
			HR_WSAEINPROGRESS		= 0x80072734, //Eine blockierende Windows Socket 1.1 Operation ist am laufen.
			HR_WSAEPROTONOSUPPORT	= 0x8007273B, //Das TCP oder UPD Protokoll wird nicht unterstützt.
			HR_WSAEAFNOSUPPORT		= 0x8007273F, //
			HR_WSAEADDRNOTAVAIL		= 0x80072741, //Die Remote Adresse ist ungültig
			HR_WSAENETDOWN			= 0x80072742, //Fehler im Netzwerk-Subsystem
			HR_WSAENETRESET			= 0x80072744, //Die Verbindung zum Gerät wurde unterbrochen
			HR_WSAECONNABORTED		= 0x80072745, //Die Verbindung zum Gerät wurde unterbrochen
			HR_WSAEDISCON			= 0x80072746, //Die Verbindung zum Gerät wurde unterbrochen
			HR_WSAETIMEDOUT			= 0x8007274C, //Timeout während des Verbindungsaufbaus
			HR_WSAECONNREFUSED		= 0x8007274D, //Der Verbindungsversuch wurde abgewiesen
			//????					= 0x80072751, //
			HR_WSAEPROCLIM			= 0x80072753, //Die maximale Anzahl an Threads, die von der von der Socketimplementierung unterstützt wird ist erreicht
			HR_WSASYSNOTREADY		= 0x8007276B, //Das Netzwerk Subsystem ist nicht bereit für die Netzwerk-Kommunikation
			HR_WSAVERNOTSUPPORTED	= 0x8007276C, //Die benötigte Windows Socket Version 2 wird vom System nicht angeboten.
			HR_WSANOTINITIALISED	= 0x8007276D, //MBTInit wurde nicht aufgerufen
			HR_WSAHOST_NOT_FOUND	= 0x80072AF9, //Das Gerät für den übergebenen DNS Name wurde nicht gefunden oder ungültige IP Adresse
			HR_WSATRY_AGAIN			= 0x80072AFA, //Fehler im DNS Server
			HR_WSANO_RECOVERY		= 0x80072AFB  //Ein Fataler Fehler ist aufgetreten
		}

		// Static class members -------------------------------------------------------
		private static int ms_InstanceCount;

		// Member for eatch Instance --------------------------------------------------
		private int		m_hSocket;		
		private byte[]	m_readBuffer;
		private byte[]	m_writeBuffer;

		//*****************************************************************************
		//*  EVENTS         ***********************************************************
		//*****************************************************************************
		public event MBTErrorEventHandler OnMBTError;

		//*****************************************************************************
		//*  DELEGATE       ***********************************************************
		//*****************************************************************************
		// ErrorEventHandler ---------------------------------------------------------- 
		public delegate void MBTErrorEventHandler(int ErrorCode);

		// MBTReadCompleted -----------------------------------------------------------   
		public delegate void MBTReadCompleted(	int hSocket,
										ushort callbackContext,
										int errorCode,
										byte tableType,
										ushort dataStartAddress,
										ushort numRead,
										ushort numBytes,
										byte[] pReadBuffer);
					  
		// MBTWriteCompleted -----------------------------------------------------------   
		public delegate void MBTWriteCompleted(int hSocket,			// socket handle
										ushort callbackContext,		// callback context, handed over at the call
										int errorCode,				// result of the write operation
										byte tableType,				// type of MODBUS/TCP tables(MODBUSTCP_TABLE_xxx)	
										ushort dataStartAddress,	// start address of the registers or coils to be
										ushort numWrite,			// number of the registers or coils to be written
										byte[] pWriteBuffer);		// memory section with the data to be written	
		

		//*****************************************************************************
		//*  DECLARE EXTERN ***********************************************************
		//*****************************************************************************
		// MBTInit --------------------------------------------------------------------                                                        
		[DllImport("MBT.dll")]
		internal static extern int MBTInit();

		// MBTExit --------------------------------------------------------------------	                                                          
		[DllImport("MBT.dll")]
		internal static extern int MBTExit();

		// MBTConnect -----------------------------------------------------------------	                                                          
		[DllImport("MBT.dll")]
		internal static extern int MBTConnect(	[In]  string	IpAddress,
												[In]  ushort	Port,
												[In]  bool		UseTCP,
												[In]  ushort	TimeOut,
												[Out] out int	hSocket);

		// MBTDisconnect --------------------------------------------------------------	                                                          
		[DllImport("MBT.dll")]
		internal static extern int MBTDisconnect([In] int hSocket);

		// MBTReadRegisters -----------------------------------------------------------                                                          
		[DllImport("MBT.dll")]
		internal static extern int MBTReadRegisters([In] int hSocket,
													[In] byte tableType,
													[In] ushort dataStartAddress,
													[In] ushort numWords,
													[In, Out] byte[] pReadBuffer,
													[In] int fpReadCompletedCallback,
													[In] int callbackContext);
		
		// MBTWriteRegisters ----------------------------------------------------------- 
		[DllImport("MBT.dll")]
		internal static extern int MBTWriteRegisters([In] int hSocket,
													[In] ushort dataStartAddress,
													[In] ushort numWords,
													[In] byte[] pWriteBuffer,
													[In] int fpWriteCompletedCallback,
													[In] int callbackContext);
		
		// MBTReadCoils ---------------------------------------------------------------
		[DllImport("MBT.dll")]
		internal static extern int MBTReadCoils([In] int hSocket,
												[In] byte tableType,
												[In] ushort dataStartAddress,
												[In] ushort numBits,
												[Out] byte[] pReadBuffer,
												[In] int fpReadCompletedCallback,
												[In] int callbackContext);

		// MBTWriteCoils ---------------------------------------------------------------
		[DllImport("MBT.dll")]
		internal static extern int MBTWriteCoils([In] int hSocket,
												[In] ushort dataStartAddress,
												[In] ushort numBits,
												[In] byte[] pWriteBuffer,
												[In] int fpWriteCompletedCallback,
												[In] int callbackContext);

		// MBTSwapWord ----------------------------------------------------------------
		[DllImport("MBT.dll")]
		public static extern ushort MBTSwapWord([In] ushort wData);

		// MBTSwapDWord ----------------------------------------------------------------
		[DllImport("MBT.dll")]
		public static extern int MBTSwapDWord([In] int dwData);

		//*****************************************************************************
		//****  PUBLIC AREA ***********************************************************
		//*****************************************************************************

		// Konstruktor ----------------------------------------------------------------                                                        
		public MBTDLL()
		{
			if (ms_InstanceCount == 0)MBTInit();
			ms_InstanceCount += 1;
			m_readBuffer = new byte[256];
			m_writeBuffer = new byte[256];
		}

		// Destruktor -----------------------------------------------------------------
		public void Destroy()
		{			
			m_writeBuffer = null;
			m_readBuffer = null;
			ms_InstanceCount -= 1;
			if (ms_InstanceCount == 0)MBTExit();	
		}
		
		
		// Connect --------------------------------------------------------------------                                                        
		public int Connect( string IpAddress,
							ushort Port,
							bool UseTCP,
							ushort TimeOut)
		{
			int result = MBTConnect(IpAddress,
									Port,
									UseTCP,
									TimeOut,
									out this.m_hSocket);
			if ((result != 0) & (OnMBTError != null))
				OnMBTError(result);
			return result;
		}

		// DisConnect -----------------------------------------------------------------                                                         
		public int Disconnect()
		{
			int result = MBTDisconnect(this.m_hSocket);
			if ((result != 0) & (OnMBTError != null))
				OnMBTError(result);
			return result;
		}
		
		
		// Read (Coil's)--------------------------------------------------------------
		public int Read(ushort DataAddress, ref bool[] BoolData, bool Output)
		{
			byte tableType;
			if (Output) 
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_OUTPUT_COIL;
			else 
			{
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_INPUT_COIL ;
			}
			ushort numBits = (ushort)BoolData.Length;
			int result = MBTReadCoils(	this.m_hSocket,
										tableType,
										DataAddress,
										numBits,
										this.m_readBuffer,
										0,
										0);
			if (result == 0) 
			{ 

				for (int i = 0; i < (numBits) ; i++)
				{ 
					int byt = i/8;
					int bit = i%8;
					BoolData[i] = ((this.m_readBuffer[byt] & (0x01 << bit)) > 0x00); 
				}
			}
			else
			{
				if (OnMBTError != null)	OnMBTError(result);
			}
			return result;		
		}

		// Read (WORD's)--------------------------------------------------------------
		public int Read(ushort DataAddress, ref ushort[] WordData, bool Output)
		{
			byte tableType;
			if (Output) 
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_OUTPUT_REGISTER;
			else 
			{
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_INPUT_REGISTER;
			}
			ushort numWords = (ushort)WordData.Length;
			int result = MBTReadRegisters(	this.m_hSocket,
											tableType,
											DataAddress,
											numWords,
											this.m_readBuffer,
											0,
											0);
			if (result == 0) 
			{ 
				for (int i = 0; i < (numWords * 2) ; i++)
				{ 
					if (i%2 == 0) WordData[i/2] = (ushort)(((ushort)this.m_readBuffer[i] << 8) | ((ushort)this.m_readBuffer[i+1]));
				}
			}
			else
			{
				if (OnMBTError != null) OnMBTError(result);
			}
			return result;
		}

		// Read (String)-----------------------------------------------------------
		public int Read(ushort DataAddress, ushort Quantity, ref string TheString, bool Output)
		{
			byte tableType;
			if (Output) 
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_OUTPUT_REGISTER;
			else 
			{
				tableType = (byte)MBTTableType.MODBUSTCP_TABLE_INPUT_REGISTER;
			}
			ushort numWords = (ushort)((Quantity/2) + 1);
			int result = MBTReadRegisters(	this.m_hSocket,
											tableType,
											DataAddress,
											numWords,
											this.m_readBuffer,
											0,
											0);
			if (result == 0) 
			{ 
				byte[] MyByteArray = new byte[(int)Quantity +1];
				for (int i = 0; i < Quantity; i++)
				{ 
					if (i%2 == 0)
					{
						MyByteArray[i+1] = this.m_readBuffer[i];
						MyByteArray[i]   = this.m_readBuffer[i+1];
					}				
				}
				System.Text.ASCIIEncoding  myEncoder = new System.Text.ASCIIEncoding();			
				TheString = myEncoder.GetString(MyByteArray);
			}
			else
			{
				if (OnMBTError != null) OnMBTError(result);
			}
			return result;
		}

		
		// Write (Coil's)--------------------------------------------------------------
		public int Write(ushort DataAddress, bool[] BoolData)
		{
			ushort numBits = (ushort)BoolData.Length;
			//Clear writebuffer
			for (int i = 0; i < (numBits); i++)
			{
				this.m_writeBuffer[i] = 0x00;
			}
			//Preset writebuffer
			for (int i = 0; i < (numBits); i++)
			{ 
				int byt = i/8;
				int bit = i%8;
				if (BoolData[i])
				{
					this.m_writeBuffer[byt] = (byte)(this.m_writeBuffer[byt] | (0x01 << bit));
				}
			}
			int result = MBTWriteCoils(	this.m_hSocket,
										DataAddress,
										numBits,
										this.m_writeBuffer,
										0,
										0);

			if ((result != 0) & (OnMBTError != null))
				OnMBTError(result);
			return result;		
		}

		// Write (WORD's)--------------------------------------------------------------
		public int Write(ushort DataAddress, ushort[] WordData)
		{
			ushort numWords = (ushort)WordData.Length;
			for (int i = 0; i < (numWords * 2) ; i++)
			{ 
				if (i%2 == 0)
				{
					this.m_writeBuffer[i] = (byte)((WordData[i/2] & 0xFF00) >> 8);
					this.m_writeBuffer[i+1] = (byte)((WordData[i/2] & 0x00FF));
				}
			}

			int result = MBTWriteRegisters(	this.m_hSocket,
											DataAddress,
											numWords,
											this.m_writeBuffer,
											0,
											0);

			if ((result != 0) & (OnMBTError != null))
				OnMBTError(result);
			return result;
		}

		// Write (String)----------------------------------------------------------
		public int Write(ushort DataAddress, string TheString)
		{
			
			ushort numWords = (ushort)TheString.Length;
			char[] UniCodeCharArray = TheString.ToCharArray();
			for (int i = 0; i < (numWords); i++)
			{ 
				if (i%2 == 0)
				{
					this.m_writeBuffer[i+1] = (byte)UniCodeCharArray[i];
					try
					{
						this.m_writeBuffer[i]	= (byte)UniCodeCharArray[i+1];
					}
					catch
					{
						this.m_writeBuffer[i]	= 0x00;
					}
				}				
			}

			int result = MBTWriteRegisters(	this.m_hSocket,
											DataAddress,
											numWords,
											this.m_writeBuffer,
											0,
											0);

			if ((result != 0) & (OnMBTError != null))
				OnMBTError(result);
			return result;
		}

		
		// GetErrorMsg ------------------------------------------------------------
		public string GetErrorMsg(int ErrorCode)
		{
			return ((MBTError)ErrorCode).ToString();
		}

		// GetErrorDesc -----------------------------------------------------------
		public string GetErrorDesc(int ErrorCode)
		{
			string desc = "Desription not aviable for" + ErrorCode.ToString();
			switch ((MBTError)ErrorCode)
			{
				case MBTError.MBTDLL_NOT_CONNECTED_TO_DEVICE :
					desc = ErrorCode.ToString() + ": Nicht verbunden mit Gerät";
					break;
				case MBTError.MBTDLL_UNSUPPORTED_ADDRESS:
					desc = ErrorCode.ToString() + ": Angeforderte Modbus-Adresse wird nicht unterstützt";
					break;			
				case MBTError.MBT_THREAD_CREATION_ERROR :
					desc = ErrorCode.ToString() + ": Der Abeitsthread der MBT Library konnte nicht erzeugt werden";
					break;
				case MBTError.MBT_EXIT_TIMEOUT_ERROR :
					desc = ErrorCode.ToString() + ": Der Arbeitsthread der Library konnte nicht innerhalb des Timeouts beendet werden";
					break;
				case MBTError.MBT_UNKNOWN_THREAD_EXIT_ERROR :
					desc = ErrorCode.ToString() + ": Der Abeitsthread der MBT Library hat sich mit einem Fehler-Code beendet";
					break;
				case MBTError.MBT_NO_ENTRY_ADDABLE_ERROR:
					desc = ErrorCode.ToString() + ": Fehler in der Socket-Verwaltung der Library";
					break;
				case MBTError.MBT_NO_JOB_ADDABLE_ERROR :
					desc = ErrorCode.ToString() + ": Fehler in der Job Verwaltung der Library";
					break;
				case MBTError.MBT_HANDLE_INVALID_ERROR:
					desc = ErrorCode.ToString() + ": Das übergebene Handle ist ungültig";
					break;
				case MBTError.MBT_SOCKET_TIMEOUT_ERROR:
					desc = ErrorCode.ToString() + ": Ein Modbus/TCP Telegramm für den I/O Auftrag wurde abschickt. Innerhalb des Reqeust-Timeouts wurde jedoch keine Antwort vom Modbus/TCP empfangen";
					break;
				case MBTError.MBT_EXIT_ERROR:
					desc = ErrorCode.ToString() + ": Der I/O Auftrag wurde durch ein MBTExit Call abgebrochen";
					break;			
				case MBTError.MBT_ILLEGAL_FUNCTION:
					desc = ErrorCode.ToString() + ": Modbus FunctionsCode wird vom Modbus-Slave nicht unterstützt";
					break;
				case MBTError.MBT_ILLEGAL_DATA_ADDRESS:
					desc = ErrorCode.ToString() + ": Zugriff auf ungültige Datenadresse im Modbus-Slave";
					break;
				case MBTError.MBT_ILLEGAL_DATA_VALUE:
					desc = ErrorCode.ToString() + ": Ungültiger Datenwert, schreiben nicht möglich";
					break;
				case MBTError.MBT_ILLEGAL_RESPONSE_LENGTH:
					desc = ErrorCode.ToString() + ": Antworttelegramm hat nicht die erwartete Länge";
					break;
				case MBTError.MBT_ACKNOWLEDGE:
					desc = ErrorCode.ToString() + ": 0xEF01F005";
					break;
				case MBTError.MBT_SLAVE_DEVICE_BUSY:
					desc = ErrorCode.ToString() + ": 0xEF01F006";
					break;
				case MBTError.MBT_NEGATIVE_ACKNOWLEDGE:
					desc = ErrorCode.ToString() + ": 0xEF01F007";
					break;
				case MBTError.MBT_MEMORY_PARITY_ERROR:
					desc = ErrorCode.ToString() + ": 0xEF01F008";
					break;
				case MBTError.MBT_GATEWAY_PATH_UNAVAILABLE:
					desc = ErrorCode.ToString() + ": 0xEF01F00A";
					break;
				case MBTError.MBT_GATEWAY_TARGET_DEVICE_FAILED_TO_RESPOND:
					desc = ErrorCode.ToString() + ": 0xEF01F00B";
					break;
				case MBTError.HR_WSAEFAULT:
					desc = ErrorCode.ToString() + ": Der übergebene ReadBuffer zeigt auf keinen gültigen Speicherbereich";
					break;
				case MBTError.HR_WSAEMFILE:
					desc = ErrorCode.ToString() + ": Das System hat keine weiteren Socket Descriptor zur Verfügung";
					break;
				case MBTError.HR_WSAEWOULDBLOCK:
					desc = ErrorCode.ToString() + ": Zu viele I/O Operationen sind zur Zeit ausständig";
					break;
				case MBTError.HR_WSAEINPROGRESS:
					desc = ErrorCode.ToString() + ": Eine blockierende Windows Socket 1.1 Operation ist am laufen.";
					break;
				case MBTError.HR_WSAEPROTONOSUPPORT:
					desc = ErrorCode.ToString() + ": Das TCP oder UPD Protokoll wird nicht unterstützt.";
					break;
				case MBTError.HR_WSAEAFNOSUPPORT:
					desc = ErrorCode.ToString() + ": 0x8007273F";
					break;
				case MBTError.HR_WSAEADDRNOTAVAIL:
					desc = ErrorCode.ToString() + ": Die Remote Adresse ist ungültig";
					break;
				case MBTError.HR_WSAENETDOWN:
					desc = ErrorCode.ToString() + ": Fehler im Netzwerk-Subsystem";
					break;
				case MBTError.HR_WSAENETRESET:
					desc = ErrorCode.ToString() + ": Die Verbindung zum Gerät wurde unterbrochen";
					break;
				case MBTError.HR_WSAECONNABORTED:
					desc = ErrorCode.ToString() + ": Die Verbindung zum Gerät wurde unterbrochen";
					break;
				case MBTError.HR_WSAEDISCON:
					desc = ErrorCode.ToString() + ": Die Verbindung zum Gerät wurde unterbrochen";
					break;
				case MBTError.HR_WSAETIMEDOUT:
					desc = ErrorCode.ToString() + ": Timeout während des Verbindungsaufbaus";
					break;
				case MBTError.HR_WSAECONNREFUSED:
					desc = ErrorCode.ToString() + ": Der Verbindungsversuch wurde abgewiesen";
					break;
				case MBTError.HR_WSAEPROCLIM:
					desc = ErrorCode.ToString() + ": Die maximale Anzahl an Threads, die von der von der Socketimplementierung unterstützt wird ist erreicht";
					break;
				case MBTError.HR_WSASYSNOTREADY:
					desc = ErrorCode.ToString() + ": Das Netzwerk Subsystem ist nicht bereit für die Netzwerk-Kommunikation";
					break;
				case MBTError.HR_WSAVERNOTSUPPORTED:
					desc = ErrorCode.ToString() + ": Die benötigte Windows Socket Version 2 wird vom System nicht angeboten.";
					break;
				case MBTError.HR_WSANOTINITIALISED:
					desc = ErrorCode.ToString() + ": MBTInit wurde nicht aufgerufen";
					break;
				case MBTError.HR_WSAHOST_NOT_FOUND:
					desc = ErrorCode.ToString() + ": Das Gerät für den übergebenen DNS Name wurde nicht gefunden oder ungültige IP Adresse";
					break;
				case MBTError.HR_WSATRY_AGAIN:
					desc = ErrorCode.ToString() + ": Fehler im DNS Server";
					break;
				case MBTError.HR_WSANO_RECOVERY:
					desc = ErrorCode.ToString() + ": Ein Fataler Fehler ist aufgetreten";
					break;		
			}
			return desc ;
		}
	}
}
