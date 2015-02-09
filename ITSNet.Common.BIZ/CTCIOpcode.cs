using System;
namespace ITSNet.Common.BIZ
{
	public enum CTCIOpcode
	{
		HELLO,
		HELLO_REPLY,
		CONFIRM,
		DATA,
		LEFTOVER,
		LEFTOVER_LAST,
		RETRAN_REQ,
		RETRAN_REPLY,
		ACK,
		NACK,
		FINISH,
		ACKFIN,
		ECHO,
		ECHO_REPLY,
		NULL
	}
}
